using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopGUI : MonoBehaviour
{
    [SerializeField] private Transform _buttonTemplate;
    [SerializeField] public GameObject _shopContentUI;
    [SerializeField] public List<ShopItemSO> _shopItemList;
    [SerializeField] public GameObject _shopUI;

    public event EventHandler NotEnoughCurrency;
    public bool Show { private set; get; }

    public void Buy(int itemOrder)
    {
        var item = _shopItemList[itemOrder];

        if (item.Cost > CurrencySystem.Instance.GetCurrency())
        {
            NotEnoughCurrency?.Invoke(this, EventArgs.Empty);
            return;
        }

        CurrencySystem.Instance.RemoveCurrency(item.Cost);      

        switch (item.Type)
        {
            case ShopItemSO.ItemType.Building:
                GridBuildingSystem.Instance.SetSelectedBuilding((PlacedBuildingSO)item);
                break;
            case ShopItemSO.ItemType.Armor:
                break;
            case ShopItemSO.ItemType.MeleeWeapon:
                break;
            case ShopItemSO.ItemType.RangedWeapon:
                break;
        }
    }

    public void ShowShop()
    {
        ShowShop(!Show);
    }

    public void ShowShop(bool val)
    {
        if (Show == val) return;

        Show = val;

        if (Show)
        {
            _shopUI.SetActive(Show);
            LeanTween.scale(_shopUI.gameObject, Vector3.one, 0.1f).setEaseLinear();
        }
        else
        {
            LeanTween.scale(_shopUI.gameObject, Vector3.zero, 0.1f).setEaseLinear().setOnComplete(() =>
                _shopUI.SetActive(Show));
        }

        if (GridBuildingSystem.Instance.SelectedPlacedBuildingSo != null)
            GridBuildingSystem.Instance.SetBuildMode(!Show);
    }

    private void ImportShop()
    {
        for (var i = 0; i < _shopItemList.Count; i++)
            ShopItem.Create(_buttonTemplate, _shopItemList[i], i, _shopContentUI.transform, this);
    }

    private void Start()
    {
        ImportShop();
    }

}