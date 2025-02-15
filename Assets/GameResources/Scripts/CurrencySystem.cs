using System;
using UnityEngine;

public class CurrencySystem : MonoBehaviour
{
    public static CurrencySystem Instance;
    [SerializeField] private int _currencyAmount = 1000;

    [SerializeField] private Sprite _sprite;

    public event EventHandler OnCurrencyChange;

    private void Awake()
    {
        if (Instance != null) return;

        Instance = this;
    }

    public void AddCurrency(int value)
    {
        _currencyAmount += value;
        OnCurrencyChange?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveCurrency(int value)
    {
        if (_currencyAmount < value)
        {
            Debug.Log("Can't Buy");
            return;
        }

        _currencyAmount -= value;
        OnCurrencyChange?.Invoke(this, EventArgs.Empty);
    }

    public int GetCurrency()
    {
        Debug.Log("Currency: " + _currencyAmount);
        return _currencyAmount;
    }

    public Sprite GetSprite()
    {
        return _sprite;
    }
}