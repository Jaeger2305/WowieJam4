using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Robot : MonoBehaviour
{

    private AIDestinationSetter destinationSetter;
    private AIPath path;
    private IAstarAI ai;
    public float radius = 20;

    private void Awake()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        path = GetComponent<AIPath>();
        ai = GetComponent<IAstarAI>();
    }
    Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitCircle * radius;

        point += (Vector2)ai.position;
        return point;
    }

    void Update()
    {
        // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all
        if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        {
            ai.destination = PickRandomPoint();
            ai.SearchPath();
        }
    }
}