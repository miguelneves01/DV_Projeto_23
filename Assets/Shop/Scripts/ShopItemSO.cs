using System.Collections;
using System.Collections.Generic;
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

    [field: SerializeField] public Sprite Sprite { private set; get; }
    [field: SerializeField] public string Name { private set; get; }
    [field: SerializeField] public string Description { private set; get; }
    [field: SerializeField] public int Cost { private set; get; }
    [field: SerializeField] public int Level { private set; get; }
    [field: SerializeField] public ItemType Type { private set; get; }

}
