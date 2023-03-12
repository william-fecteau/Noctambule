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
    private Vector2 movementInput;

    public List<string> items;

    void Start()
    {
        items = new List<string>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // movementInput direction
        movementInput = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W)) movementInput.y = 1;
        if (Input.GetKey(KeyCode.S)) movementInput.y = -1;
        if (Input.GetKey(KeyCode.A)) movementInput.x = -1;
        if (Input.GetKey(KeyCode.D)) movementInput.x = 1;
        movementInput = movementInput.normalized;

        // dash
        if (Input.GetKey(KeyCode.Space) && canDash)
        {
            isDashing = true;
            canDash = false;
        }
        if (isDashing) {
            movementInput *= dashingPower;
            dashingTimer += Time.fixedDeltaTime;
            if (dashingTimer >= dashingTime) {
                isDashing = false;
                dashingTimer = 0f;
            }
        } else if (!canDash) {
            dashingCooldownTimer += Time.fixedDeltaTime;
            if (dashingCooldownTimer >= dashingCooldown) {
                canDash = true;
                dashingCooldownTimer = 0f;
            }
        }
        // Move
        movementInput = movementInput * speed * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(rigidbody2d.position + movementInput);


        // Rotate towards mouse
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseScreenPosition  - (Vector2) transform.position).normalized;
        transform.up = direction;
    }

    public Vector2 GetNormalizedDirection() => movementInput;
}