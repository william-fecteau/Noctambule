using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Triggered");
        // 3 is the shop layer. Make sure it is set correctly in the inspector
        if (other.CompareTag("Player")) {
            ShopManager.GetInstance().ToggleShop();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        // 3 is the shop layer. Make sure it is set correctly in the inspector
        if (other.CompareTag("Player")) {
            ShopManager.GetInstance().ToggleShop();
        }
    }

}