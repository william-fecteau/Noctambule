using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepiScript : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private float angle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(0, 0);
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

        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 direction = (mouseScreenPosition  - (Vector2) transform.position).normalized;

        transform.up = direction;
        transform.position += (Vector3)movement * speed * Time.deltaTime;
    }
}
