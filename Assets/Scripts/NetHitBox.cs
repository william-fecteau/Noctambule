using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetHitBox : MonoBehaviour
{
    [SerializeField] private float netCooldown = 1f;
    private float netTimer = 0f;
    private bool canNet = true;
    private bool netActivated = false;

    private void Update() {
        if (canNet == false) {
            netTimer += Time.deltaTime;
            if (netCooldown >= netTimer) {
                canNet = true;
                netActivated = false;
                netTimer = 0f;
            }
        } else if (Input.GetKeyDown(KeyCode.Q)) {
            netActivated = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 3 && netActivated) {
            Debug.Log("Moth hit");
            canNet = false;
        }
    }
}
