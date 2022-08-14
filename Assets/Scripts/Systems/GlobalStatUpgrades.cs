using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalStatUpgrades : MonoBehaviour
{
    //Lazy singleton
    public static GlobalStatUpgrades Instance;

    [SerializeField] StatEditor _statEditPrefab;
    [SerializeField] AnimationCurve UpgradeCostCurve;
    [SerializeField] Transform _statEditMenuContentParent;
    [SerializeField] Inventory _playerInventory;
    public List<StatBonus> StatBonuses = new List<StatBonus>();

    bool _statEditorPopulated;

    private void Awake()
    {
        Instance = this;

        StatBonus sbMaxHP = new StatBonus(
        "HP",
        0f,
        50f,
        0,
        10,
        50,
        500);

        StatBonus sbAttackDamage = new StatBonus(
        "Damage",
        0f,
        5f,
        0,
        10,
        100,
        1000);

        StatBonuses.Add(sbMaxHP);
        StatBonuses.Add(sbAttackDamage);

        sbMaxHP.OnUpgrade += TestStatUpgradeHP;
    }

    public bool AffordUpgrade(StatBonus editedStatBonus)
    {
        return _playerInventory.Scrap >= GetUpgradeCost(editedStatBonus);
    }

    public void BuyUpgrade(StatBonus editedStatBonus)
    {
        _playerInventory.SpendScrap(GetUpgradeCost(editedStatBonus));
        editedStatBonus.UpgradeStat();
    }

    public void PopulateStatEditorScreen()
    {
        if (_statEditorPopulated) return;

        for (int i = 0; i < StatBonuses.Count; i++) {
            var stat = Instantiate(_statEditPrefab, _statEditMenuContentParent);
            stat.SetEditedStat(StatBonuses[i]);
        }

        _statEditorPopulated = true;
    }

    public int GetUpgradeCost(StatBonus stat)
    {
        float t = stat.UpgradeCount / stat.UpgradeMaxCount;
        return Mathf.RoundToInt(stat.UpgradeBaseCost + (stat.UpgradeCurveMult * UpgradeCostCurve.Evaluate(t)));
    }

    void TestStatUpgradeHP()
    {
        print($"HP upgraded! New value is: {StatBonuses[0].CurrentBonusValue}| Upgrade count: {StatBonuses[0].UpgradeCount}");
    }
}

public class StatBonus
{
    public string Name;
    public float CurrentBonusValue;
    public float BonusPerUpgrade;
    public int UpgradeCount;
    public int UpgradeMaxCount;
    public int UpgradeBaseCost;
    public float UpgradeCurveMult;

    public event Action OnUpgrade;

    public void UpgradeStat()
    {
        CurrentBonusValue += BonusPerUpgrade;
        UpgradeCount++;
        OnUpgrade?.Invoke();
    }

    public StatBonus(
        string name,
        float currentBonusValue,
        float bonusPerUpgrade,
        int upgradeCount,
        int upgradeMaxCount,
        int upgradeBaseCost,
        float upgradeCurveMult)
    {
        Name = name;
        CurrentBonusValue = currentBonusValue;
        BonusPerUpgrade = bonusPerUpgrade;
        UpgradeCount = upgradeCount;
        UpgradeMaxCount = upgradeMaxCount;
        UpgradeBaseCost = upgradeBaseCost;
        UpgradeCurveMult = upgradeCurveMult;
    }
}
