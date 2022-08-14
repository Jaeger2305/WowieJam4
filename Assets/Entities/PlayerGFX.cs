using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Mostly just a wrapper around the animator, but is extendable for other events/sounds/effects outside of just the animator.
// The reason we have a wrapper, is because the state machine is on the root object, but the graphics have multiple layers (e.g. a basic entity death, and then there's another child object dealing with the baked animations from blender)
public class PlayerGFX : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetWalking(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    public void TriggerDamage(int currentHealth, int difference)
    {
        if (difference > 0) animator.SetTrigger("TakeDamage");
    }

    public void TriggerDeath()
    {
        animator.SetTrigger("Die");
        animator.SetBool("IsDead", true);
    }
}
