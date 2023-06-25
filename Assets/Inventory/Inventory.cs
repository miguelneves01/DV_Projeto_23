using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [SerializeField] private PlayerInventorySO _playerInventory;

    [SerializeField] private WeaponItemSO _initialMelee;
    [SerializeField] private WeaponItemSO _initialRanged;
    [SerializeField] private ArmorItemSO _initialArmor;

    private void Awake()
    {
        if (Instance != null) return;

        Instance = this;

        var objs = GameObject.FindGameObjectsWithTag("GameController");

        if (objs.Length > 1) Destroy(gameObject);

        InitItems();

        DontDestroyOnLoad(gameObject);
    }

    public void AddWeaponStats(WeaponItemSO item)
    {
        switch (item.Type)
        {
            case ShopItemSO.ItemType.MeleeWeapon:
                _playerInventory.MeleeWeapon.Damage += item.Damage * ExperienceSystem.Instance.CurrentLevel;
                _playerInventory.MeleeWeapon.AttackSpeed += item.AttackSpeed * ExperienceSystem.Instance.CurrentLevel;
                break;
            case ShopItemSO.ItemType.RangedWeapon:
                _playerInventory.RangedWeapon.Damage += item.Damage * ExperienceSystem.Instance.CurrentLevel;
                _playerInventory.RangedWeapon.AttackSpeed += item.AttackSpeed * ExperienceSystem.Instance.CurrentLevel;
                break;
            default: break;
        }
    }

    public void AddArmorStats(ArmorItemSO item)
    {
        _playerInventory.Armor.Armor += item.Armor * ExperienceSystem.Instance.CurrentLevel;
        _playerInventory.Armor.Health += item.Health * ExperienceSystem.Instance.CurrentLevel;
    }

    private void InitItems()
    {
        _playerInventory.Armor.Armor = _initialArmor.Armor;
        _playerInventory.Armor.Health = _initialArmor.Health;
        _playerInventory.RangedWeapon.Damage = _initialRanged.Damage;
        _playerInventory.RangedWeapon.AttackSpeed = _initialRanged.AttackSpeed;
        _playerInventory.MeleeWeapon.Damage = _initialMelee.Damage;
        _playerInventory.MeleeWeapon.AttackSpeed = _initialMelee.AttackSpeed;
    }

    public WeaponItemSO GetMelee()
    {
        return _playerInventory.MeleeWeapon;
    }

    public WeaponItemSO GetRanged()
    {
        return _playerInventory.MeleeWeapon;
    }
}