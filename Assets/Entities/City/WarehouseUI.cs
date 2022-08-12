using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarehouseUI : MonoBehaviour
{
    private Slider _suppliesSlider;

    void Start()
    {
        _suppliesSlider = GetComponentInChildren<Slider>();
    }

    public void SetSupplies(int supplies)
    {
        _suppliesSlider.value = supplies;
    }
}
