using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Color _friendColor;
    [SerializeField] Color _enemyColor;
    [SerializeField] Image _fill;
    private Slider _healthSlider;

    void Start()
    {

        _healthSlider = GetComponentInChildren<Slider>();
        EntityMetadata edata = GetComponent<EntityMetadata>();
        if (edata == null) return;
        _fill.color = edata.entityType == EntityType.AlliedRobot ? _friendColor : _enemyColor;
    }

    public void SetHealth(int health, int difference)
    {
        _healthSlider.value = health;
    }
}
