using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Linq;



[System.Serializable] public struct ChaseModuleConfig
{
    [SerializeField] public EntityType[] chaseTargets;
    [SerializeField] public float visionRange;
    [SerializeField] public float chaseSpeed;
    public ChaseModuleConfig(EntityType[] chaseTargets, float visionRange, float chaseSpeed)
    {
        this.chaseTargets = chaseTargets;
        this.visionRange = visionRange;
        this.chaseSpeed = chaseSpeed;
    }
}

public class ChaseModule : MonoBehaviour
{
    private IAstarAI _ai;
    private AIDestinationSetter _aiDestinationSetter;
    private Transform _chaseTarget;
    [SerializeField] private float _visionRange = 5f;
    [SerializeField] private float _chaseSpeed = 1f;
    [SerializeField] List<EntityType> _targetEntityTypes;
    bool pathToTargetInitialized = false;
    public List<EntityType> targetEntityTypes { get { return _targetEntityTypes; } private set { _targetEntityTypes = value; } }

    private void Awake()
    {
        _ai = GetComponent<IAstarAI>();
        _aiDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    public void ConfigureModule(ChaseModuleConfig config)
    {
        _visionRange = config.visionRange;
        _chaseSpeed = config.chaseSpeed;
        _targetEntityTypes = new List<EntityType>(config.chaseTargets);
    }

    public bool RefreshTarget()
    {
        bool hadTargetLastTime = pathToTargetInitialized;
        var target = SearchForTarget();
        pathToTargetInitialized = target != null && !_ai.reachedEndOfPath;
        if (target != null && !hadTargetLastTime) _ai.destination = PickRandomPointWithDistanceFromTarget(3f, target.position);

        return _chaseTarget != null;
    }


    private Vector3 PickRandomPointWithDistanceFromTarget(float distance, Vector3 target)
    {
        var point = Random.insideUnitCircle * distance;

        point += (Vector2)target;
        return point;
    }

    public Transform SearchForTarget()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _visionRange);
        foreach (var collider in colliders)
        {
            collider.TryGetComponent(out EntityMetadata entity);
            if (entity == null) continue;
            if (targetEntityTypes.Contains(entity.entityType))
            {
                _chaseTarget = collider.transform;
                _ai.maxSpeed = _chaseSpeed;
                return _chaseTarget;
            }
        }
        _chaseTarget = null;
        return null;
    }
}
