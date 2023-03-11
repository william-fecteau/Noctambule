using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rigidbody2d;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 inputVector = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) inputVector.y = 1;
        if (Input.GetKey(KeyCode.S)) inputVector.y = -1;
        if (Input.GetKey(KeyCode.A)) inputVector.x = -1;
        if (Input.GetKey(KeyCode.D)) inputVector.x = 1;

        inputVector = inputVector.normalized;


        Vector2 movementVector = Vector2.zero;
        movementVector.x = inputVector.x;
        movementVector.y = inputVector.y;
        movementVector = movementVector * speed * Time.deltaTime;

        rigidbody2d.MovePosition(rigidbody2d.position + movementVector);
    }
}
