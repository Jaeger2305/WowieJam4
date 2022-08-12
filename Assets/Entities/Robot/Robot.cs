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
        Seeking,
        ShootingTarget,
    }

    public AIPath _aiPath;
    private Vector3 _startingPosition;
    private State _state;
    private AIDestinationSetter _aiDestinationSetter;
    private GameObject _fabricatedDestination;
    [SerializeField] private Vector2 _patrolRange = new Vector2(6f, 4f);
    [SerializeField] private float _visionRange = 5f;
    [SerializeField] private float _stopChaseDistance = 8f;
    [SerializeField] private float _attackRange = 4f;

    [SerializeField] private Transform _chaseTarget;
    [SerializeField] private Transform _seekTarget;
    [SerializeField] private bool _isArmed;

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
        _fabricatedDestination.transform.position = _seekTarget == null ? GetRoamingPosition() : _seekTarget.position;
        _aiDestinationSetter.target = _fabricatedDestination.transform;
        _isArmed = TryGetComponent(out ProjectileController pc);
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
                if (_chaseTarget != null)
                {
                    FindTarget(); // this might be causing performance issues - i think there's an easier way of doing this
                }
                break;
            case State.Seeking:
                if (_aiPath.reachedDestination)
                {
                    _startingPosition = transform.position;
                    _state = State.Roaming;
                }
                break;
            case State.ShootingTarget:
                if (Vector3.Distance(transform.position, _chaseTarget.position) < _attackRange)
                {
                    Debug.Log("Target in range, firing");
                    GetComponent<ProjectileController>().FireAtTarget(_chaseTarget);
                }
                else
                {
                    _state = State.ChaseTarget;
                }
                break;
            case State.ChaseTarget:
                if (Vector3.Distance(transform.position, _chaseTarget.position) > _stopChaseDistance)
                {
                    Debug.Log("Target out of chase range, going back to start");
                    _state = State.GoingBackToStart;
                } else if (_isArmed && Vector3.Distance(transform.position, _chaseTarget.position) < _attackRange)
                {
                    _state = State.ShootingTarget;
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
        if (Vector3.Distance(transform.position, _chaseTarget.position) < _visionRange)
        {
            Debug.Log("Found target");
            // Target within target range
            _state = State.ChaseTarget;
            _aiDestinationSetter.target = _chaseTarget;
        }
    }

    public void SeekTarget(Transform target)
    {
        _seekTarget = target;
    }
    public void HuntTarget(Transform target)
    {
        _chaseTarget = target;
    }
}