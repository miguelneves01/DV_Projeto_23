using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [SerializeField] private PlayerInventorySO _playerInventory;

    private void Awake()
    {
        if (Instance != null) return;

        Instance = this;

        var objs = GameObject.FindGameObjectsWithTag("GameController");

        if (objs.Length > 1) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void SetMelee(WeaponItemSO item)
    {
        Debug.Log("New MeleeWeapon");
        _playerInventory.MeleeWeapon = item;
    }

    public void SetRanged(WeaponItemSO item)
    {
        Debug.Log("New RangedWeapon");
        _playerInventory.RangedWeapon = item;
    }

    public void SetArmor(ArmorItemSO item)
    {
        Debug.Log("New Armor");
        _playerInventory.Armor = item;
    }

    public WeaponItemSO GetMelee()
    {
        return _playerInventory.MeleeWeapon;
    }

    public WeaponItemSO GetRanged()
    {
        return _playerInventory.RangedWeapon;
    }

    public ArmorItemSO GetArmor()
    {
        return _playerInventory.Armor;
    }
}