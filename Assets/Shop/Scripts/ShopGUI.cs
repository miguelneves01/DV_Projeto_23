using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopGUI : MonoBehaviour
{
    [SerializeField] private Transform _buttonTemplate;
    [SerializeField] public GameObject _shopContentUI;
    [SerializeField] public List<ShopItemSO> _shopItemList;
    [SerializeField] public GameObject _shopUI;
    [SerializeField] private GameObject _uiBlocker;
    public bool Show { private set; get; }

    public event EventHandler NotEnoughCurrency;

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
                Inventory.Instance.AddArmorStats((ArmorItemSO)item);
                break;
            case ShopItemSO.ItemType.MeleeWeapon:
            case ShopItemSO.ItemType.RangedWeapon:
                Inventory.Instance.AddWeaponStats((WeaponItemSO)item);
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
                _shopUI.SetActive(Show)).setIgnoreTimeScale(true);
        }

        if (GridBuildingSystem.Instance.SelectedPlacedBuildingSo != null)
            GridBuildingSystem.Instance.SetBuildMode(!Show);
    }

    private void ImportShop()
    {
        for (var i = 0; i < _shopItemList.Count; i++)
        {
            if (_shopItemList[i].Level > ExperienceSystem.Instance.CurrentLevel) continue;

            ShopItem.Create(_buttonTemplate, _shopItemList[i], i, _shopContentUI.transform, this);
        }
    }

    private void Start()
    {
        _shopItemList.Sort(new ItemLevelComparer());

        ImportShop();

        ExperienceSystem.Instance.OnLevelChange += ImportShop_OnLevelChange;
    }

    private void ImportShop_OnLevelChange(object sender, EventArgs e)
    {
        ClearShop();
        ImportShop();
    }

    private void ClearShop()
    {
        for (var i = 0; i < _shopContentUI.transform.childCount; i++)
        {
            var child = _shopContentUI.transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    public void CloseShop()
    {
        ShowShop(false);
        _uiBlocker.SetActive(false);
    }

    public class ItemLevelComparer : IComparer<ShopItemSO>
    {
        public int Compare(ShopItemSO _x, ShopItemSO _y)
        {
            return _x.Level.CompareTo(_y.Level);
        }
    }
}