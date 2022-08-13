using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Linq;

public class ChaseModule : MonoBehaviour
{
    private IAstarAI _ai;
    private AIDestinationSetter _aiDestinationSetter;
    private Transform _chaseTarget;
    [SerializeField] private float _visionRange = 5f;
    [SerializeField] private float _chaseSpeed = 1f;
    [SerializeField] List<EntityType> _targetEntityTypes;
    public List<EntityType> targetEntityTypes { get { return _targetEntityTypes; } private set { _targetEntityTypes = value; } }

    private void Awake()
    {
        _ai = GetComponent<IAstarAI>();
        _aiDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    public void ConfigureModule(float visionRange, float chaseSpeed, EntityType[] chaseTargets)
    {
        _visionRange = visionRange;
        _chaseSpeed = chaseSpeed;
        _targetEntityTypes = new List<EntityType>(chaseTargets);
    }

    public bool RefreshTarget()
    {
        _aiDestinationSetter.target = SearchForTarget();
        return _chaseTarget != null;
    }

    public Transform SearchForTarget()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _visionRange);
        foreach (var collider in colliders)
        {
            collider.TryGetComponent(out EntityMetadata entity);
            if (entity == null) break;
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
