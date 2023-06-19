using UnityEngine;

public class ShopItemSO : ScriptableObject
{
    public enum ItemType
    {
        Building,
        Armor,
        RangedWeapon,
        MeleeWeapon
    }

    [field: SerializeField] public Sprite Sprite { get; }
    [field: SerializeField] public string Name { get; }
    [field: SerializeField] public string Description { get; }
    [field: SerializeField] public int Cost { get; }
    [field: SerializeField] public int Level { get; }
    [field: SerializeField] public ItemType Type { get; }
}