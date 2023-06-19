using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    private TMP_Text _uiText;
    // Start is called before the first frame update
    void Start()
    {
        _uiText = transform.Find("Value").GetComponent<TMP_Text>();
        transform.Find("Image").GetComponent<Image>().sprite = CurrencySystem.Instance.GetSprite();

        CurrencySystem.Instance.OnCurrencyChange += OnCurrencyChange_UpdateUI;
        _uiText.text = CurrencySystem.Instance.GetCurrency().ToString();
    }

    private void OnCurrencyChange_UpdateUI(object sender, EventArgs e)
    {
        _uiText.text = CurrencySystem.Instance.GetCurrency().ToString();
    }
}
