using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    [SerializeField] private float dashingPower = 24f;

    private bool canDash = true;
    private bool isDashing;
    private float dashingTimer = 0f;
    private float dashingCooldownTimer = 0f;

    void Update()
    {
        Vector2 movement = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.Space) && canDash)
        {
            isDashing = true;
            canDash = false;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movement.y = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1;
        }
        movement = movement.normalized;

        if (isDashing) {
            movement *= dashingPower;
            dashingTimer += Time.deltaTime;
            if (dashingTimer >= dashingTime) {
                isDashing = false;
                dashingTimer = 0f;
            }
        } else if (!canDash) {
            dashingCooldownTimer += Time.deltaTime;
            if (dashingCooldownTimer >= dashingCooldown) {
                canDash = true;
                dashingCooldownTimer = 0f;
            }
        }

        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 direction = (mouseScreenPosition  - (Vector2) transform.position).normalized;

        transform.up = direction;
        transform.position += (Vector3)movement * speed * Time.deltaTime;
    }
}
