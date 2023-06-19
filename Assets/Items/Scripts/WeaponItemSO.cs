using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon SO", menuName = "ShopItems/Weapon")]

public class WeaponItemSO : ShopItemSO
{
    [field: SerializeField] public int Damage { private set; get; }
    [field: SerializeField] public float AttackSpeed { private set; get; }
}
