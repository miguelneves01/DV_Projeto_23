using UnityEngine;

[CreateAssetMenu(fileName = "New Armor SO", menuName = "ShopItems/Armor")]
public class ArmorItemSO : ShopItemSO
{
    [field: SerializeField] public int Armor { get; }
    [field: SerializeField] public int Health { get; }
}