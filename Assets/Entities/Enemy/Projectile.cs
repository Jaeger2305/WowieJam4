using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private Transform source;
    private int speedFactor;
    private int damage = 5;
    [SerializeField] List<EntityType> _targetEntityTypes;
    [SerializeField] List<AudioClip> _sfx;
    [SerializeField] float _vol;

    private Rigidbody2D projectileBody;

    void Awake()
    {
        projectileBody = GetComponent<Rigidbody2D>();
        var sp = GetComponent<SoundPlayer>();
        sp.TryPlaySound(sp.GetVariant(_sfx), SoundType.World, _vol);
    }

    public void Init(Transform target, Transform source, int speedFactor, int damage, List<EntityType> targetEntityTypes)
    {
        GetComponentInChildren<SpriteRenderer>().color = new Color(Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), Random.Range(0.2f, 1f));
        this.transform.position = source.position;
        this.damage = damage;
        this.speedFactor = speedFactor;
        this._targetEntityTypes = targetEntityTypes;
        Vector2 moveDirection = (target.position - source.position).normalized;
        projectileBody.velocity = moveDirection * speedFactor;
        projectileBody.angularVelocity = Random.Range(-100f, 100f);
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        collider.TryGetComponent(out Health health);
        collider.TryGetComponent(out EntityMetadata entity);
        if (health != null && _targetEntityTypes.Contains(entity.entityType))
        {
            health.LoseHealth(damage);
            Destroy(gameObject);
        }
        // note: projectile won't ever be destroyed if it misses its target... perf issue
    }
}
