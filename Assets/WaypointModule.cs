using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WaypointModule : MonoBehaviour
{
    private IAstarAI _ai;
    [SerializeField] private Transform _waypointTarget;
    [SerializeField] private float _waypointSpeed = 1f;

    private void Awake()
    {
        _ai = GetComponent<IAstarAI>();
    }

    public void ConfigureModule (float waypointSpeed)
    {
        _waypointSpeed = waypointSpeed;
    }

    public void SetWaypoint(Transform waypointTarget)
    {
        _waypointTarget = waypointTarget;
    }

    public void CompleteWaypoint()
    {
        _waypointTarget = null;
    }

    public bool CheckActiveWaypoint()
    {
        bool isActive = _waypointTarget != null && Vector3.Distance(transform.position, _waypointTarget.position) > 0.5f;
        if (isActive)
        {
            _ai.destination = _waypointTarget.position;
            _ai.maxSpeed = _waypointSpeed;
        }
        return isActive;
    }
}
