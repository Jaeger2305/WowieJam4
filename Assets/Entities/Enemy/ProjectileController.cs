using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject projectilePrefab;
    [SerializeField] private float cooldownBetweenShots = 0.5f;
    private float cooldown = 0f;

    private void Update()
    {
        cooldown = Mathf.Clamp(cooldown - Time.deltaTime, 0f, cooldownBetweenShots);
    }
    public void FireAtTarget(Transform target)
    {
        if (cooldown <= 0)
        {
            cooldown += cooldownBetweenShots;
            Projectile projectile = Instantiate(projectilePrefab).GetComponent<Projectile>();
            projectile.Init(target, transform, 20, 15);
        }
    }
}
