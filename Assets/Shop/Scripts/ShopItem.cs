using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public static void Create(Transform buttonTemplate, ShopItemSO itemSO, int shopOrder, Transform shop,
        ShopGUI shopGui)
    {
        var itemTransform = Instantiate(buttonTemplate, shop.transform);

        var offset = 70;
        var xInitialOffset = (10 + offset) * (shopOrder + 1);
        var xOffset = shopOrder == 0 ? xInitialOffset : xInitialOffset + 50;
        var yOffset = -offset - 30;

        itemTransform.Find("Name").GetComponent<TMP_Text>().text = itemSO.Name;
        itemTransform.Find("Image").GetComponent<Image>().sprite = itemSO.Sprite;
        itemTransform.Find("Cost").GetComponent<TMP_Text>().text = itemSO.Cost.ToString();
        itemTransform.Find("Description").GetComponent<TMP_Text>().text = itemSO.Description;
        itemTransform.Find("Currency").GetComponent<Image>().sprite = CurrencySystem.Instance.GetSprite();

        itemTransform.Find("Button").GetComponent<Button>().onClick.AddListener(delegate { shopGui.Buy(shopOrder); });
    }
}