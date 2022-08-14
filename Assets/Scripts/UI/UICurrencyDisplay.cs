using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICurrencyDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _currencyAmountText;
    [SerializeField] Inventory _invTracked;
    [SerializeField] float _updateDuration;

    float _currencyUpdateStartTime;
    float _currencyDisplayAmount;
    float _currencyUpdatingAmount;

    bool _currencyUpdating;

    private void Start()
    {
        _currencyDisplayAmount = _invTracked.Scrap;
        _currencyAmountText.text = "" + _currencyDisplayAmount;
        _currencyUpdating = false;
    }

    public void StartUpdateScrap()
    {
        _currencyUpdating = true;
        _currencyUpdatingAmount = _currencyDisplayAmount;
        _currencyUpdateStartTime = Time.time;
    }

    private void Update()
    {
        if (!_currencyUpdating) return;
        float t = (Time.time - _currencyUpdateStartTime) / _updateDuration;
        _currencyUpdatingAmount = Mathf.Lerp(_currencyDisplayAmount, _invTracked.Scrap, t);
        _currencyAmountText.text = _currencyUpdatingAmount.ToString("0");

        if (t >= 1f) {
            _currencyUpdating = false;
            _currencyDisplayAmount = _invTracked.Scrap;
        }
    }


}
