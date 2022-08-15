using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "Waves/Config", order = 1)]
public class WaveData : ScriptableObject
{
    public int consumptionRate = 5;
    public int startingSupplies = 100;
    public int maxSupplies = 100;
    public int requiredBeetles = 5;
    public float waveDurationSeconds = 20;
    public string tutorialText;
    public string flavourText;


    // stretches
    public int cityCount = 1;


    public RobotFactoryConfig[] factoryConfigs;
    public RobotConfig[] alliedSpawns;
    public RobotConfig[] enemySpawns;

}
