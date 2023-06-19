using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private Dictionary<string, ShopGUI> _shops;
    [SerializeField] private GameObject _uiBlocker;
    public static ShopManager Instance { private set; get; }

    private void Awake()
    {
        if (Instance != null) return;

        Instance = this;

        _uiBlocker.SetActive(false);
    }

    private void Start()
    {
        _shops = new Dictionary<string, ShopGUI>();

        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            _shops.Add(child.name, child.GetComponent<ShopGUI>());
        }
    }

    public void ShowShop(string shopToShow)
    {
        var curShop = _shops.GetValueOrDefault(shopToShow);

        foreach (var shop in _shops.Values)
            if (!shop.Equals(curShop))
            {
                shop.ShowShop(false);
            }
            else
            {
                curShop.ShowShop(!curShop.Show);
                _uiBlocker.SetActive(curShop.Show);
            }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (var shop in _shops.Values) shop.ShowShop(false);
            _uiBlocker.SetActive(false);
        }
    }
}