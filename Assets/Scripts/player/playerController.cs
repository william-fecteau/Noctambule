using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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

        rigidbody.MovePosition(rigidbody.position + movementVector);
    }
}
