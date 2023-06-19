using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor SO", menuName = "ShopItems/Armor")]

public class ArmorItemSO : ShopItemSO
{
    [field: SerializeField] public int Armor { private set; get; }
    [field: SerializeField] public int Health { private set; get; }
}
