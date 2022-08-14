using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    public void DestroyAfterDeath()
    {
        Destroy(gameObject);
    }
}
