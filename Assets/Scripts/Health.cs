using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent healthEmpty;
    public UnityEvent<int, int> healthChanged;
    [SerializeField] private int _health = 100;
    [SerializeField] private int _maxHealth = 100;

    public int health
    {
        get { return _health; }
        private set { _health = value; }
    }

    /** Consume a specific amount - useful for triggering on a specific event */
    public void LoseHealth(int amount)
    {
        var healthBefore = _health;
        _health = System.Math.Clamp(_health - amount, 0, _maxHealth);

        // assume all the correct events were already triggered, and as a result just exit early on this event.
        // We were seeing this when the projectiles were entering a corpse's collider, and attempting to remove its health further
        // Probably semantically better to never call lose health, and remove the collider, or disable damage or something, but this is just quick.
        if (healthBefore == health) return;

        healthChanged.Invoke(_health, amount); // careful here - amount will be positive in the event for both lose and gain health!
        if (_health <= 0) healthEmpty.Invoke();
    }

    public void GainHealth(int amount)
    {
        healthChanged.Invoke(_health, amount);
        _health = System.Math.Clamp(_health + amount, 0, _maxHealth);
    }
}
