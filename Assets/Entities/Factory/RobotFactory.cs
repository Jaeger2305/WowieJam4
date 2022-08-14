using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFactory : MonoBehaviour
{
    public GameObject robot;
    [SerializeField] private Transform robotDestination;

    public List<GameObject> FriendlyRobots { get; private set; } = new List<GameObject>();

    public void SpawnRobot()
    {
        Instantiate(robot);
        robot.GetComponent<WaypointModule>().SetWaypoint(robotDestination);
        robot.GetComponent<Inventory>().AddScrap(40);
        FriendlyRobots.Add(robot);
    }

    //Call when robot dies
    public void DeListRobot(GameObject robot)
    {
        FriendlyRobots.Remove(robot);
    }
}
