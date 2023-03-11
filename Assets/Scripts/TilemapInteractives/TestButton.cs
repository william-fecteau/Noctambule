using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    bool isPlayerOnButton;

    void Start() {
        isPlayerOnButton = false;
    }

    void Update() {
        this.buttonPress(isPlayerOnButton);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            isPlayerOnButton = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            isPlayerOnButton = false;
        }
    }

    private void buttonPress(bool isPlayerOnBtn) {
        if (Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("buttonPress");
        }
    }
}
