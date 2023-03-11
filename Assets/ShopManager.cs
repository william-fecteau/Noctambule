using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private static ShopManager instance;
    private Shop shop;
    private Inventory inventory;

    [SerializeField] private GameObject shopUI;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }


        shopUI.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    public void ToggleShop() {
        shopUI.SetActive(!shopUI.activeSelf);
    }
    public static ShopManager GetInstance() {
        return instance;
    }
}

public class Inventory {
    private List<Item> items = new List<Item>();
    private int money = 0;

    public void BuyItem(Item item) {
        if (money >= item.GetPrice()) {
            money -= item.GetPrice();
            items.Add(item);
        }
    }
}

public class Shop {
    private List<Item> items = new List<Item>();
}

public class Item {
    private string name;
    private int price;
    private bool isBought = false;

    public Item(string name, int price) {
        this.name = name;
        this.price = price;
    }

    public int GetPrice() {
        return price;
    }
}

public class SaintRowSword : Item {
    public SaintRowSword() : base("Saint Row Sword", 100) {}
}
