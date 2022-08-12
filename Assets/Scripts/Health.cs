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
        _health = System.Math.Clamp(_health - amount, 0, _maxHealth);
        healthChanged.Invoke(_health, amount);
        if (_health <= 0) healthEmpty.Invoke();
    }

    public void GainHealth(int amount)
    {
        healthChanged.Invoke(_health, amount);
        _health = System.Math.Clamp(_health + amount, 0, _maxHealth);
    }
}
