using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEntityAnimatorEvents : MonoBehaviour
{
    public void DyingAnimationComplete()
    {
        LivingEntity entityComponent = GetComponentInParent<LivingEntity>();
        if (entityComponent) entityComponent.DestroyAfterDeath();
    }
}
