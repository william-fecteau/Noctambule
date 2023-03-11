using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed = 5f;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 position = transform.position;

        if (Input.GetKey(KeyCode.W)) {
            position.y += speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A)) {
            position.x -= speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S)) {
            position.y -= speed * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.D)) {
            position.x += speed * Time.deltaTime;
        }

        transform.position = position;
    }
}
