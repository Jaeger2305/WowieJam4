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
    [SerializeField] Color _buttonDefaultColor;
    [SerializeField] Color _buttonHoverColor;

    public StatBonus EditedStatBonus;

    public void SetEditedStat(StatBonus editedStat)
    {
        _statNameTxt.text = editedStat.Name;
        _statValueTxt.text = "+" + editedStat.CurrentBonusValue;
        _statUpgradeCostTxt.text = GlobalStatUpgrades.Instance.GetUpgradeCost(editedStat).ToString();
        EditedStatBonus = editedStat;
    }

    public void ButtonHover()
    {
        _statUpgradeButton.color = _buttonHoverColor;
    }
    public void ButtonHoverExit()
    {
        _statUpgradeButton.color = _buttonDefaultColor;
    }

    public void UpgradeClicked()
    {
        if (!GlobalStatUpgrades.Instance.AffordUpgrade(EditedStatBonus)) return;
        GlobalStatUpgrades.Instance.BuyUpgrade(EditedStatBonus);
        SetEditedStat(EditedStatBonus);
    }
}
