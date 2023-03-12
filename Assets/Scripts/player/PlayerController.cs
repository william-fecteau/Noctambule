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

    private Animator animator;
    private Vector2 movementInput;
    
    // animation state
    private const string PLAYER_IDLE  = "Player_idle";
    private const string PLAYER_RUN_L = "Player_run_l";
    private const string PLAYER_RUN_R = "Player_run_r";
    private const string PLAYER_RUN_U = "Player_run_u";
    private const string PLAYER_RUN_D = "Player_run_d";

    private string currentAnimaton;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
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
        movementInput = movementInput * speed * Time.deltaTime;
        rigidbody2d.MovePosition(rigidbody2d.position + movementInput);


        // Rotate towards mouse
        float camOffsetX = Camera.main.pixelWidth/2;
        float camOffsetY = Camera.main.pixelHeight/2;
        Vector2 mouseScreenPosition = new Vector2(Input.mousePosition.x - camOffsetX,Input.mousePosition.y - camOffsetY );
        Vector2 playerScreenPosition = Camera.main.WorldToScreenPoint(this.transform.position);

        playerScreenPosition.x -= camOffsetX;
        playerScreenPosition.y -= camOffsetY;

        Vector2 vectRes = mouseScreenPosition - playerScreenPosition;

        // check which direction to face, then if character is idle or not
        if(Mathf.Abs(vectRes.x) > Mathf.Abs(vectRes.y)) {
            if(vectRes.x>0) {
                if(movementInput.magnitude > 0) ChangeAnimationState(PLAYER_RUN_R);
                else ChangeAnimationState(PLAYER_IDLE);
            } 
            else {
                if(movementInput.magnitude > 0) ChangeAnimationState(PLAYER_RUN_L);
                else ChangeAnimationState(PLAYER_IDLE);
            }
        }
        else {
            if(vectRes.y>0) {
                if(movementInput.magnitude > 0) ChangeAnimationState(PLAYER_RUN_U);
                else ChangeAnimationState(PLAYER_IDLE);
            } 
            else {
                if(movementInput.magnitude > 0) ChangeAnimationState(PLAYER_RUN_D);
                else ChangeAnimationState(PLAYER_IDLE);
            }
        }

        if(movementInput.magnitude > 0)
        {

        }
        else {

        }
        //Vector2 direction = (mouseScreenPosition  - (Vector2) transform.position).normalized;
        //transform.up = direction;
    }

    public Vector2 GetNormalizedDirection() => movementInput;
    
    //=====================================================
    // mini animation manager (shamelessly stolen)
    //=====================================================
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
}
