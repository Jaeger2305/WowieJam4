using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDestroyer : MonoBehaviour
{
    private Robot _robot;

    private void Start()
    {
        _robot = GetComponent<Robot>();
    }

    public void SearchForTarget()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 10);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Robot"))
            {
                _robot.HuntTarget(collider.transform);
            }
        }
    }
}
