using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private PlayerWallet playerWallet;
    private static ShopManager instance;

    public Upgrade[] upgrades;
    public TMPro.TMP_Text coinText;
    [SerializeField] private GameObject shopUI;
    public Transform shopContent;
    public GameObject itemPrefab;


    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        GameObject gameManagerObject = GameObject.Find("Player");
        if (gameManagerObject != null) {
            playerWallet = gameManagerObject.GetComponent<PlayerWallet>();
        }

        shopUI.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        foreach (Upgrade upgrade in upgrades) {
            GameObject item = Instantiate(itemPrefab, shopContent);

            upgrade.itemRef = item;
            upgrade.isOwned = false;

            foreach (Transform child in item.transform) {
                if (child.gameObject.name == "Name") {
                    child.gameObject.GetComponent<TMPro.TMP_Text>().text = upgrade.name.ToString();
                } else if (child.gameObject.name == "Description") {
                    child.gameObject.GetComponent<TMPro.TMP_Text>().text = upgrade.description.ToString();
                } else if (child.gameObject.name == "Cost") {
                    child.gameObject.GetComponent<TMPro.TMP_Text>().text = "$" + upgrade.cost.ToString();
                } else if (child.gameObject.name == "Image") {
                    child.gameObject.GetComponent<Image>().sprite = upgrade.image;
                }
            }

            item.GetComponentInChildren<Button>().onClick.AddListener(() => {
                BuyUpgrade(upgrade);
            });
        } 
    }

    public void BuyUpgrade(Upgrade upgrade) {
        if (playerWallet.currentMoney >= upgrade.cost && !upgrade.isOwned) {
            playerWallet.SubtractMoney(upgrade.cost);
            upgrade.isOwned = true;

            this.UpdateLimitedItem(upgrade.itemRef, upgrade);
        }
    }

    private void UpdateLimitedItem(GameObject upgradeRef, Upgrade upgrade) {
        foreach (Transform child in upgradeRef.transform) {
            if (child.gameObject.name == "Name") {
                child.gameObject.GetComponent<TMPro.TMP_Text>().text = upgrade.name.ToString();
            } else if (child.gameObject.name == "Description") {
                child.gameObject.GetComponent<TMPro.TMP_Text>().text = upgrade.description.ToString();
            } else if (child.gameObject.name == "Cost") {
                child.gameObject.GetComponent<TMPro.TMP_Text>().text = "Owned";
            } else if (child.gameObject.name == "Image") {
                child.gameObject.GetComponent<Image>().sprite = upgrade.image;
            }
        }
    }

    public void ToggleShop() {
        shopUI.SetActive(!shopUI.activeSelf);
    }

    private void OnGUI() {
        coinText.text = "Coins: " + playerWallet.currentMoney;
    }

    public static ShopManager GetInstance() {
        return instance;
    }
}

[System.Serializable]
public class Upgrade {
    public string name;
    public string description;
    public int cost;
    public Sprite image;
    [HideInInspector] public bool isOwned;
    [HideInInspector] public GameObject itemRef;
}