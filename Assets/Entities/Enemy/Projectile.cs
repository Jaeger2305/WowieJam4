using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private Transform source;
    private int speedFactor;
    private int damage = 5;

    private Rigidbody2D projectileBody;

    void Awake()
    {
        projectileBody = GetComponent<Rigidbody2D>();
    }

    public void Init(Transform target, Transform source, int speedFactor, int damage)
    {
        GetComponentInChildren<SpriteRenderer>().color = new Color(Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), Random.Range(0.2f, 1f));
        this.transform.position = source.position;
        this.damage = damage;
        this.speedFactor = speedFactor;
        Vector2 moveDirection = (target.position - source.position).normalized;
        projectileBody.velocity = moveDirection * speedFactor;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Health>().LoseHealth(damage);
            Destroy(gameObject);
        }
    }
}
