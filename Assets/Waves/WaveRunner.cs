using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Linq;

public class WaveRunner : MonoBehaviour
{
    [SerializeField] float _baseWaveDelay;
    [SerializeField] AnimationCurve _waveDelayCurve;

    [SerializeField] private List<WaveData> _waves = new List<WaveData>();
    [SerializeField] private Spawner _alliedSpawn;
    [SerializeField] private Spawner _enemySpawn;
    [SerializeField] private Spawner _enemyFactorySpawn;
    [SerializeField] private Spawner _alliedFactorySpawn;
    [SerializeField] private Warehouse _city;
    public UnityEvent waveComplete;
    public UnityEvent allWavesComplete;
    public UnityEvent OnNewWaveStart;
    public UnityEvent<List<Transform>> friendlyFactoryLocationsChange;

    public GameObject factoryPrefab;

    public WaveData _activeWaveData;

    float _currentWaveDuration;
    float _lastWaveTime;
    int _startingWaveCount;

    private List<RobotFactory> _factories = new List<RobotFactory>();

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _alliedPrefab;

    private Timer _timer;

    private void Start()
    {
        _timer = GetComponent<Timer>();
        _startingWaveCount = _waves.Count;
        //StartWave();
    }

    private void Update()
    {
        if (_waves.Count <= 0) return;
        int nextWaveIndex = _startingWaveCount - _waves.Count;
        float nextWaveTime = _waveDelayCurve.Evaluate(nextWaveIndex / _startingWaveCount) * _baseWaveDelay + _lastWaveTime + _currentWaveDuration;
        if (Time.time >= nextWaveTime) StartWave();
    }

    public void StartWave(InputAction.CallbackContext ctx)
    {
        return;
        //if (!ctx.performed) return;
        //StartWave();
    }

    [ContextMenu("DEBUG START NEXT WAVE")]
    public void StartWave()
    {
        Debug.Log("Starting wave");
        _lastWaveTime = Time.time;
        if (_waves.Count == 0)
        {
            allWavesComplete.Invoke();
            return;
        }
        _activeWaveData = _waves.ElementAt(0);
        _waves.RemoveAt(0);
        _waves.TrimExcess();

        _currentWaveDuration = _activeWaveData.waveDurationSeconds;

        _city.ConfigureWarehouse(_activeWaveData.startingSupplies, _activeWaveData.maxSupplies, _activeWaveData.consumptionRate, _activeWaveData.requiredBeetles);

        foreach (var robotConfig in _activeWaveData.alliedSpawns)
        {
            RobotFactory.ConfigureRobot(_enemySpawn.SpawnInCollider(_alliedPrefab), robotConfig);
        }
        foreach (var robotConfig in _activeWaveData.enemySpawns)
        {
            RobotFactory.ConfigureRobot(_enemySpawn.SpawnInCollider(_enemyPrefab), robotConfig);
        }

        foreach (var factoryConfig in _activeWaveData.factoryConfigs)
        {
            RobotFactory robotFactory;
            // If the factory doesn't already exist, create it.
            if (_factories.FirstOrDefault(f => f.label == factoryConfig.label) == null)
            {
                GameObject factoryGO = _enemyFactorySpawn.SpawnInCollider(factoryPrefab);
                robotFactory = factoryGO.GetComponent<RobotFactory>();
                _factories.Add(robotFactory);
            } else
            {
                robotFactory = _factories.First(f => f.label == factoryConfig.label);
            }


            // reconfigure all properties according to the config
            robotFactory.ConfigureFactory(factoryConfig);

            // adjust the timer
            _timer.DeregisterAllListenersForFunction(robotFactory.SpawnRobot);
            if (factoryConfig.spawnSpeed == TickSpeed.Fast)
            {
                _timer.quickTick.AddListener(robotFactory.SpawnRobot);
            } else if (factoryConfig.spawnSpeed == TickSpeed.Medium)
            {
                _timer.normalTick.AddListener(robotFactory.SpawnRobot);
            } else
            {
                _timer.slowTick.AddListener(robotFactory.SpawnRobot);
            }
        }

        OnNewWaveStart?.Invoke();
        var friendlyFactories = _factories.Where(f => f.robot.GetComponent<EntityMetadata>().entityType == EntityType.AlliedRobot).Select(f => f.transform).ToList();
        friendlyFactoryLocationsChange.Invoke(friendlyFactories);
    }

    public WaveData GetCurrentWaveData()
    {
        if (_activeWaveData == null) print($"wave data null lol. {_waves.Count} waves remaining");
        return _activeWaveData;
    }

    public void StartWaveAfterSeconds(int delay)
    {
        // Automatically start the next wave with no pause.
        // If we want to use the "chill" music assets, we could add in an artificial delay here
        // Maybe we check the scene if there are no enemies, and swap the music then?
        Invoke("StartWave", delay);
    }
}
