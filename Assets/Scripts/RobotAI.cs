using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAI : MonoBehaviour
{
    private State _state;

    private enum State
    {
        Roaming,
        ChaseTarget,
        GoingBackToStart,
        Waypoint,
        AttackingTarget,
    }

    private ChaseModule _chaseModule;
    private PatrolModule  _patrolModule;
    private WaypointModule _waypointModule;
    private AttackModule _attackModule;

    private void Start()
    {
        _chaseModule = GetComponent<ChaseModule>();
        _patrolModule = GetComponent<PatrolModule>();
        _waypointModule = GetComponent<WaypointModule>();
        _attackModule = GetComponent<AttackModule>();
    }

    private void Update()
    {
        switch (_state)
        {
            default:
            case State.Roaming:
                if (_chaseModule.SearchForTarget() != null) {
                    _state = State.ChaseTarget;
                } else if (_waypointModule.CheckActiveWaypoint()) {
                    _state = State.Waypoint;
                } else {
                    _patrolModule.AdvancePatrol();
                }

                break;
            case State.Waypoint:
                if (_chaseModule.SearchForTarget() != null)
                {
                    _state = State.ChaseTarget;
                }
                if (!_waypointModule.CheckActiveWaypoint())
                {
                    _waypointModule.CompleteWaypoint();
                    _patrolModule.StartPatrol();
                    _state = State.Roaming;
                }
                
                break;
            case State.AttackingTarget:
                if (_attackModule.RefreshTarget())
                {
                    _attackModule.FireAtTarget();
                }
                else
                {
                    _state = State.ChaseTarget;
                }
                break;
            case State.ChaseTarget:
                if(!_chaseModule.RefreshTarget())
                {
                    _state = State.Roaming;
                } else if (_attackModule.RefreshTarget())
                {
                    _state = State.AttackingTarget;
                }
                break;
        }
    }
}
