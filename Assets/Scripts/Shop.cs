using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public void openShop() {
        Debug.Log("Shop opened");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Trigger entered");
        // 3 is the shop layer. Make sure it is set correctly in the inspector
        if (other.gameObject.layer == 3) {
            Debug.Log("Player entered shop");
        }
    }

}
