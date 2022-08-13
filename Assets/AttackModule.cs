using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackModule : MonoBehaviour
{
    public GameObject projectilePrefab;
    [SerializeField] private float _cooldownBetweenShots = 0.5f;
    [SerializeField] private float _attackRange = 5f;
    [SerializeField] private int _projectileDamage = 15;
    [SerializeField] private int _projectileSpeed = 5;
    private Transform _attackTarget;
    private float cooldown = 0f;
    [SerializeField] List<EntityType> _targetEntityTypes;
    public List<EntityType> targetEntityTypes { get { return _targetEntityTypes; } private set { _targetEntityTypes = value; } }

    public void ConfigureModule(float range, float cooldownBetweenShots, int projectileSpeed, int projectileDamage, EntityType[] chaseTargets)
    {
        _attackRange = range;
        _cooldownBetweenShots = cooldownBetweenShots;
        _projectileSpeed = projectileSpeed;
        _projectileDamage = projectileDamage;
        _targetEntityTypes = new List<EntityType>(chaseTargets);
    }

    private void Update()
    {
        // Keeping a cooldown within an update loop here will be a performance killer
        cooldown = Mathf.Clamp(cooldown - Time.deltaTime, 0f, _cooldownBetweenShots);
    }
    public void FireAtTarget()
    {
        if (cooldown <= 0)
        {
            cooldown += _cooldownBetweenShots;
            Projectile projectile = Instantiate(projectilePrefab).GetComponent<Projectile>();
            projectile.Init(_attackTarget, transform, _projectileSpeed, _projectileDamage, _targetEntityTypes);
        }
    }

    public bool RefreshTarget()
    {
        _attackTarget = SearchForTarget();
        return _attackTarget != null;
    }

    public Transform SearchForTarget()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _attackRange);
        foreach (var collider in colliders)
        {
            collider.TryGetComponent(out EntityMetadata entity);
            if (entity == null) break;
            if (entity != null && targetEntityTypes.Contains(entity.entityType))
            {
                _attackTarget = collider.transform;
                return _attackTarget;
            }
        }
        _attackTarget = null;
        return null;
    }
}
