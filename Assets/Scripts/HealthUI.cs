using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Slider _healthSlider;

    void Start()
    {
        _healthSlider = GetComponentInChildren<Slider>();
    }

    public void SetHealth(int health, int difference)
    {
        _healthSlider.value = health;
    }
}
