using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatEditor : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _statNameTxt;
    [SerializeField] TextMeshProUGUI _statValueTxt;
    [SerializeField] TextMeshProUGUI _statUpgradeCostTxt;
    [SerializeField] Image _statUpgradeButton;

    //TODO: This approach doesn't make sense - need to reference values
    public void SetEditedStat(string statName, string statValue, string statUpgradeCost)
    {
        _statNameTxt.text = statName;
        _statValueTxt.text = statValue;
        _statUpgradeCostTxt.text = statUpgradeCost;
    }
}
