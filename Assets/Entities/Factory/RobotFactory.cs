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
    [SerializeField] public EntityType entityType;
    [SerializeField] public int health;
    [SerializeField] public int scrap;
    [SerializeField] public TickSpeed spawnSpeed;

    [SerializeField] public ChaseModuleConfig chaseConfig;
    [SerializeField] public AttackModuleConfig attackConfig;
    public RobotFactoryConfig(string label, GameObject robot, EntityType entityType, int health, int scrap, TickSpeed spawnSpeed, ChaseModuleConfig chaseConfig, AttackModuleConfig attackConfig)
    {
        this.label = label;
        this.robot = robot;
        this.entityType = entityType;
        this.health = health;
        this.scrap = scrap;
        this.spawnSpeed = spawnSpeed;
        this.chaseConfig = chaseConfig;
        this.attackConfig = attackConfig;
    }
}



public class RobotFactory : MonoBehaviour
{
    public GameObject robot;
    public string label { get; private set; }
    [SerializeField] private Transform robotDestination;

    public List<GameObject> FriendlyRobots { get; private set; } = new List<GameObject>();

    public void ConfigureFactory(RobotFactoryConfig config)
    {
        label = config.label;
        robot = config.robot;
        robot.GetComponent<Health>().SetMaxHealth(config.health);
        robot.GetComponent<Inventory>().AddScrap(config.scrap);
        robot.GetComponent<EntityMetadata>().SetEntityType(config.entityType);
        robot.GetComponent<ChaseModule>().ConfigureModule(config.chaseConfig);
        robot.GetComponent<AttackModule>().ConfigureModule(config.attackConfig);
        robot.GetComponent<PatrolModule>().ConfigureModule(5f, 10f);
    }

    public void SpawnRobot()
    {
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
