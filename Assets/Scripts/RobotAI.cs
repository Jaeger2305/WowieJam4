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

    SoundPlayer _sp;
    //This should definitely have been added to another class but time is short :(
    [SerializeField] List<AudioClip> _footstepsSfx = new List<AudioClip>();
    [SerializeField] float _stepVol;
    [SerializeField] float _sfxStepDelayRoam;
    [SerializeField] float _sfxStepDelayChase;

    float _sfxTimeRoam;
    float _sfxTimeChase;

    private void Awake()
    {
        _sp = GetComponent<SoundPlayer>();
    }

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
                    PlayStepRoam();
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
                PlayStepChase();
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

    void PlayStepRoam()
    {
        if (Time.time < _sfxTimeRoam + _sfxStepDelayRoam) return;

        var sfx = _footstepsSfx[Random.Range(0, _footstepsSfx.Count-1)];

        _sp.TryPlaySound(sfx, SoundType.World, _stepVol);

        _sfxTimeRoam = Time.time;
    }

    void PlayStepChase()
    {
        if (Time.time < _sfxTimeChase + _sfxStepDelayChase) return;
        var sfx = _footstepsSfx[Random.Range(0, _footstepsSfx.Count-1)];
        _sp.TryPlaySound(sfx, SoundType.World, _stepVol);

        _sfxTimeChase = Time.time;
    }
}
