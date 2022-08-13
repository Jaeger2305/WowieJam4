using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFactory : MonoBehaviour
{
    public GameObject robot;
    [SerializeField] private Transform robotDestination;

    public void SpawnRobot()
    {
        Instantiate(robot);
        robot.GetComponent<WaypointModule>().SetWaypoint(robotDestination);
    }
}
