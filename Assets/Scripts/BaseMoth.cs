using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMoth : MonoBehaviour
{
    [SerializeField] private float minSecBeforeDirectionChange = 0.5f;
    [SerializeField] private float maxSecBeforeDirectionChange = 2.0f;
    [SerializeField] private float minSpeed = 0.5f;
    [SerializeField] private float maxSpeed = 1.5f;

    private Vector2 curDirection;
    private float directionChangeTimer;
    private float nextDirectionChangeTimeSec;
    private Rigidbody2D rigidbody2d;
    private Vector2 curSpeedVector;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        DecideNewDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (directionChangeTimer > nextDirectionChangeTimeSec)
            DecideNewDirection();

        MoveInCurrentDirection();
        directionChangeTimer += Time.deltaTime;
    }

    private void MoveInCurrentDirection() 
    {
        Vector2 moveVector = curDirection;
        moveVector.x *= Time.deltaTime;
        moveVector.y *= Time.deltaTime;
        moveVector.Scale(curSpeedVector);

        rigidbody2d.MovePosition(rigidbody2d.position + moveVector);
    }

    private void DecideNewDirection()
    {
        curSpeedVector = Vector2.zero;
        curSpeedVector.x = Random.Range(minSpeed, maxSpeed);
        curSpeedVector.y = Random.Range(minSpeed, maxSpeed);

        curDirection = Vector2.zero;
        curDirection.x = RollForDirection();
        curDirection.y = RollForDirection();
        curDirection.Normalize();

        nextDirectionChangeTimeSec = Random.Range(minSecBeforeDirectionChange, maxSecBeforeDirectionChange);
        directionChangeTimer = 0.0f;
    }

    private float RollForDirection() 
    {
        float random = Random.Range(0.0f, 1.0f);

        if (random < 0.33f) return -1.0f;
        if (random < 0.66f) return 0.0f;
        else return 1.0f;
    }
}
