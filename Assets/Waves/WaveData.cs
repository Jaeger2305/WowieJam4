using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "Waves/Config", order = 1)]
public class WaveData : ScriptableObject
{
    public int robotDamage;
    public int friendlyRobotHealth;
    public int consumptionRate;
    public float durationSeconds;


    // stretches
    public int cityCount;
    public int enemyFactoryCount;
}
