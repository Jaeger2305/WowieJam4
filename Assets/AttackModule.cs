using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



[System.Serializable] public struct AttackModuleConfig
{
    [SerializeField] public EntityType[] attackTargets;
    [SerializeField] public float attackRange;
    [SerializeField] public int projectileSpeed;
    [SerializeField] public int projectileDamage;
    [SerializeField] public float cooldownBetweenShots;
    public AttackModuleConfig(float attackRange, float cooldownBetweenShots, int projectileSpeed, int projectileDamage, EntityType[] attackTargets)
    {
        this.attackTargets = attackTargets;
        this.attackRange = attackRange;
        this.projectileSpeed = projectileSpeed;
        this.projectileDamage = projectileDamage;
        this.cooldownBetweenShots = cooldownBetweenShots;
    }
}
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

    public void ConfigureModule(AttackModuleConfig config)
    {
        _attackRange = config.attackRange;
        _cooldownBetweenShots = config.cooldownBetweenShots;
        _projectileSpeed = config.projectileSpeed;
        _projectileDamage = config.projectileDamage;
        _targetEntityTypes = new List<EntityType>(config.attackTargets);
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
            if (_attackTarget == null)
            {
                var target = SearchForTarget(); // If it's the player that triggered this, their target isn't set like the robotAI does.
                if (target == null) return;
            }
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
            if (entity == null) continue;
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
