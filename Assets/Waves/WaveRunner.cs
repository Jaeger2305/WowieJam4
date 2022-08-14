using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class WaveRunner : MonoBehaviour
{
    [SerializeField] private List<WaveData> _waves = new List<WaveData>();
    public UnityEvent waveComplete;
    public UnityEvent allWavesComplete;

    public GameObject factoryPrefab;

    private List<GameObject> _enemyFactories = new List<GameObject>();

    public void StartWave()
    {
        if (_waves.Count == 0)
        {
            allWavesComplete.Invoke();
            return;
        }
        var config = _waves.ElementAt(0);
        _waves.RemoveAt(0);

        // spawn enemy factories

        if (_enemyFactories.Count < config.enemyFactoryCount)
        {
            var factory = Instantiate(factoryPrefab);

            // Position the factory randomly, this is a risk of spawning in an unreachable zone
            // Better would be to check the pathfinding and validate before placing, but, time constraints.
            factory.transform.position = Random.insideUnitCircle * 20 + new Vector2(20, 20);
            factory.GetComponent<RobotFactory>().ConfigureFactory(config.friendlyRobotHealth);
        }

        // Automatically start the next wave with no pause.
        // If we want to use the "chill" music assets, we could add in an artificial delay here
        // Maybe we check the scene if there are no enemies, and swap the music then?
        Invoke("StartWave", config.waveDurationSeconds);
    }

}
