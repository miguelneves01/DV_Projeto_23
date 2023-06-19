using UnityEngine;

[CreateAssetMenu(fileName = "New Player Inventory", menuName = "PlayerInventory")]
public class PlayerInventorySO : ScriptableObject
{
    [field: SerializeField] public WeaponItemSO MeleeWeapon { set; get; }
    [field: SerializeField] public WeaponItemSO RangedWeapon { set; get; }
    [field: SerializeField] public ArmorItemSO Armor { set; get; }
}