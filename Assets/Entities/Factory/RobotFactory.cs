using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TickSpeed {
    Fast,
    Medium,
    Slow
}

[System.Serializable]
public struct RobotFactoryConfig
{
    [SerializeField] public string label;
    [SerializeField] public GameObject robot;
    [SerializeField] public int capacity;
    [SerializeField] public TickSpeed spawnSpeed;
    [SerializeField] public RobotConfig robotConfig;

    public RobotFactoryConfig(string label, GameObject robot, int capacity, TickSpeed spawnSpeed, RobotConfig robotConfig)
    {
        this.label = label;
        this.robot = robot;
        this.capacity = capacity;
        this.spawnSpeed = spawnSpeed;
        this.robotConfig = robotConfig;
    }
}

[System.Serializable]
public struct RobotConfig
{
    [SerializeField] public EntityType entityType;
    [SerializeField] public int health;
    [SerializeField] public int scrap;

    [SerializeField] public ChaseModuleConfig chaseConfig;
    [SerializeField] public AttackModuleConfig attackConfig;
    public RobotConfig(EntityType entityType, int health, int scrap, ChaseModuleConfig chaseConfig, AttackModuleConfig attackConfig)
    {
        this.entityType = entityType;
        this.health = health;
        this.scrap = scrap;
        this.chaseConfig = chaseConfig;
        this.attackConfig = attackConfig;
    }
}



public class RobotFactory : MonoBehaviour
{
    public GameObject robot;
    public string label { get; private set; }
    [SerializeField] private Transform robotDestination;
    [SerializeField] private int capacity;

    public List<GameObject> FriendlyRobots { get; private set; } = new List<GameObject>();

    /** There's no dedicated robot script, so just collect this as a static function here for convenience, for use in the wave runner */
    public static void ConfigureRobot(GameObject gameObject, RobotConfig robotConfig)
    {
        gameObject.GetComponent<Health>().SetMaxHealth(robotConfig.health);
        gameObject.GetComponent<Inventory>().AddScrap(robotConfig.scrap);
        gameObject.GetComponent<EntityMetadata>().SetEntityType(robotConfig.entityType);
        gameObject.GetComponent<ChaseModule>().ConfigureModule(robotConfig.chaseConfig);
        gameObject.GetComponent<AttackModule>().ConfigureModule(robotConfig.attackConfig);
        gameObject.GetComponent<PatrolModule>().ConfigureModule(5f, 10f);
    }

    public void ConfigureFactory(RobotFactoryConfig factoryConfig)
    {
        label = factoryConfig.label;
        robot = factoryConfig.robot;
        capacity = factoryConfig.capacity;
        ConfigureRobot(robot, factoryConfig.robotConfig);
    }

    public void SpawnRobot()
    {
        if (capacity <= 0) return;
        capacity -= 1;
        Instantiate(robot, transform.position, Quaternion.identity);
        robot.GetComponent<WaypointModule>().SetWaypoint(robotDestination);
        FriendlyRobots.Add(robot);
    }

    //Call when robot dies
    public void DeListRobot(GameObject robot)
    {
        FriendlyRobots.Remove(robot);
    }
}
