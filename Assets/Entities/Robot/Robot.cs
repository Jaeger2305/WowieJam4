using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Robot : MonoBehaviour
{
    private enum State
    {
        Roaming,
        ChaseTarget,
        GoingBackToStart,
    }

    public AIPath _aiPath;
    private Vector3 _startingPosition;
    private State _state;
    private AIDestinationSetter _aiDestinationSetter;
    private GameObject _fabricatedDestination;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector2 _patrolRange = new Vector2(6f, 4f);
    [SerializeField] private float _visionRange = 5f;
    [SerializeField] private float _stopChaseDistance = 8f;

    private void Awake()
    {
        _aiPath = GetComponent<AIPath>();
        _aiDestinationSetter = GetComponent<AIDestinationSetter>();
        _state = State.Roaming;
    }

    private void Start()
    {
        _startingPosition = transform.position;
        _fabricatedDestination = new GameObject();
        _fabricatedDestination.transform.position = GetRoamingPosition();
        _aiDestinationSetter.target = _fabricatedDestination.transform;
    }

    private void Update()
    {
        switch (_state)
        {
            default:
            case State.Roaming:
                if (_aiPath.reachedDestination)
                {
                    Debug.Log("Reached patrol path, getting new patrol spot");
                    _fabricatedDestination.transform.position = GetRoamingPosition();
                }

                FindTarget();
                break;
            case State.ChaseTarget:
                if (Vector3.Distance(transform.position, _target.position) > _stopChaseDistance)
                {
                    Debug.Log("Target out of chase range, going back to start");
                    _state = State.GoingBackToStart;
                }
                break;
            case State.GoingBackToStart:
                _fabricatedDestination.transform.position = _startingPosition;
                _aiDestinationSetter.target = _fabricatedDestination.transform;
                _state = State.Roaming;
                break;
        }
    }

    private Vector3 GetRoamingPosition()
    {
        return _startingPosition + new Vector3(Random.Range(-_patrolRange.x, _patrolRange.x), Random.Range(-_patrolRange.y, _patrolRange.y), 1);
    }

    private void FindTarget()
    {
        if (Vector3.Distance(transform.position, _target.position) < _visionRange)
        {
            Debug.Log("Found target");
            // Target within target range
            _state = State.ChaseTarget;
            _aiDestinationSetter.target = _target.transform;
        }
    }
}