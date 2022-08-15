using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Linq;

public class WaveRunner : MonoBehaviour
{
    [SerializeField] private List<WaveData> _waves = new List<WaveData>();
    [SerializeField] private Spawner _enemySpawn;
    [SerializeField] private Spawner _enemyFactorySpawn;
    [SerializeField] private Spawner _alliedFactorySpawn;
    public UnityEvent waveComplete;
    public UnityEvent allWavesComplete;

    public GameObject factoryPrefab;

    private List<RobotFactory> _factories = new List<RobotFactory>();

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _alliedPrefab;

    private Timer _timer;

    private void Start()
    {
        _timer = GetComponent<Timer>();
    }

    public void StartWave(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        Debug.Log("Starting wave");
        if (_waves.Count == 0)
        {
            allWavesComplete.Invoke();
            return;
        }
        var config = _waves.ElementAt(0);
        _waves.RemoveAt(0);

        for (int i = 0; i < config.enemySpawnCount; i++)
        {
            _enemySpawn.SpawnInCollider(_enemyPrefab);
        }

        foreach (var factoryConfig in config.factoryConfigs)
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

        // Automatically start the next wave with no pause.
        // If we want to use the "chill" music assets, we could add in an artificial delay here
        // Maybe we check the scene if there are no enemies, and swap the music then?
        Invoke("StartWave", config.waveDurationSeconds);
    }
}
