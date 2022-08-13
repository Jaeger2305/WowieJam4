using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PatrolModule : MonoBehaviour
{
    private IAstarAI _ai;
    private Vector3 _startingPosition;
    [SerializeField] private float _patrolRadius = 5f;
    [SerializeField] private float _patrolSpeed = 1f;

    private void Awake()
    {
        _ai = GetComponent<IAstarAI>();
    }

    public void ConfigureModule(float patrolRadius, float patrolSpeed)
    {
        _patrolRadius = patrolRadius;
        _patrolSpeed = patrolSpeed;
    }

    public void StartPatrol()
    {
        _startingPosition = transform.position;
        _ai.maxSpeed = _patrolSpeed;
    }

    public void AdvancePatrol()
    {
        // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all
        if (!_ai.pathPending && (_ai.reachedEndOfPath || !_ai.hasPath))
        {
            _ai.destination = PickRandomPoint();
            _ai.SearchPath();
        }
    }

    Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitCircle * _patrolRadius;

        point += (Vector2)_startingPosition;
        return point;
    }
}
