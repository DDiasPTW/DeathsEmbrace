using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movement Stuff")]
    public float movementSpeed = 8.0f;
    private float movementInputDirection;
    private Rigidbody2D rb;
    private bool isFacingRight = true;
    public float jumpForce = 10.0f;
    [Header("Better Jumping")]
    public float gravidade;
    private float coyote_timer = 0;
    [SerializeField] private float coyote_seconds = 0.1f;
    private float JumpPressedRemember = 0;
    [SerializeField] private float JumpPressedRememberTimer = 0.1f;
    [Header("Extra Jumps")]
    public int howManyJumps;
    public int totalJumps = 0;
    [Header ("Ground Detection")]
    public bool isGrounded;
    public Transform groundCheck;
    private float groundCheckRadius = 0f;
    public LayerMask whatIsGround;
    public float xValue;
    public float yValue;
    [Header("Animation")]
    private Animator anim;
    private const string IdleAnim = "anim_Idle";
    private const string RunAnim = "anim_Run";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();   
    }

    void Update()
    {
        CheckGround();
        CheckJump();
        CheckInput();
        CheckMovementDirection();
        CheckAnimation();
        coyote_timer += Time.deltaTime;
        rb.gravityScale = gravidade;
    }
    
    //Detetar Input de movimento -1 para esquerda, 1 para direita
    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");  
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.BoxCast(groundCheck.position, new Vector2(xValue, yValue), 0, Vector2.down, groundCheckRadius, whatIsGround);
    }
    private void CheckJump()
    {
        //Reset Jumps
        if (isGrounded && totalJumps > 0)
        {
            totalJumps = 0;
        }

        //Coyote Timer
        if (isGrounded)
        {
            coyote_timer = 0;
        }else if (!isGrounded && coyote_timer > coyote_seconds && totalJumps == 0)
        {
            totalJumps = 1;
        }

        //JumpBuffer
        JumpPressedRemember -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpPressedRemember = JumpPressedRememberTimer;
        }
        //The Jumping
        if (((JumpPressedRemember > 0) && isGrounded && totalJumps < howManyJumps) /*Jump Buffer*/)
        {
            JumpPressedRemember = 0;
            Jump();
        }else if ((Input.GetKeyDown(KeyCode.Space) && coyote_timer <= coyote_seconds && totalJumps < howManyJumps) /*CoyoteTimer*/)
        {
            JumpPressedRemember = 0;
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isGrounded)
        {
            if (totalJumps < howManyJumps)
            {
                Jump();
            }
            else return;
        }
    }
    private void Jump()
    {
        totalJumps++;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void ApplyMovement()
    {
        rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
    }


    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }else if(!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

    }

    void CheckAnimation()
    {
        if (rb.velocity.x == 0 && isGrounded)
        {
            anim.Play(IdleAnim);
        }
        else if(rb.velocity.x != 0 && isGrounded)
        {
            anim.Play(RunAnim);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0,180,0);
    }

    private void OnDrawGizmos()
    {
        if (isGrounded)
        {
            Gizmos.color = new Color(0, 1, 0, 0.5f);
        }
        else
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
        }
        Gizmos.DrawCube(groundCheck.position, new Vector2(xValue, yValue));
    }
}
