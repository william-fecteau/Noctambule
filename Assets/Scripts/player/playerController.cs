using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    [SerializeField] private float dashingPower = 5f;

    private bool canDash = true;
    private bool isDashing;
    private float dashingTimer = 0f;
    private float dashingCooldownTimer = 0f;
    private Rigidbody2D rigidbody2d;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // movement direction
        Vector2 movement = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W)) movement.y = 1;
        if (Input.GetKey(KeyCode.S)) movement.y = -1;
        if (Input.GetKey(KeyCode.A)) movement.x = -1;
        if (Input.GetKey(KeyCode.D)) movement.x = 1;
        movement = movement.normalized;

        // dash
        if (Input.GetKey(KeyCode.Space) && canDash)
        {
            isDashing = true;
            canDash = false;
        }
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

        // Move
        movement = movement * speed * Time.deltaTime;
        rigidbody2d.MovePosition(rigidbody2d.position + movement);


        // Rotate towards mouse
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseScreenPosition  - (Vector2) transform.position).normalized;
        transform.up = direction;
    }
}
