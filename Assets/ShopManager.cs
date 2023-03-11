using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private static ShopManager instance;

    public static ShopManager GetInstance() {
        return instance;
    }
    [SerializeField] private GameObject shopUI;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void ToggleShop() {
        shopUI.SetActive(!shopUI.activeSelf);
    }
}
