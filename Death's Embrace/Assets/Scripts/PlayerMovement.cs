using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    private Rigidbody2D rb;
    public bool canWalkRight = true, canWalkLeft = true, canJump = true, canOrb = true;
    public bool canNextLevel = false;
    public GameObject popUp;
    public GameObject sceneTransition;
    private RoomManager rM;
    [Header ("Movement Stuff")]
    public float movementSpeed = 8.0f;
    private float movementInputDirection;
    float velocityXSmooth;
    public float smoothTime;
    private bool isFacingRight = true;
    public bool isDead = false;
    [Header("Jumping")]
    public float jumpForce = 10.0f;
    public float gravidade = 4.5f;
    [SerializeField] private float coyote_timer;
    [SerializeField] private float coyote_seconds = 0.1f;
    private float JumpBuffer_Timer;
    [SerializeField] private float JumpBuffer_Seconds = 0.1f;
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
    public Animator anim;
    private const string IdleAnim = "anim_Idle";
    private const string RunAnim = "anim_Run";
    private const string Jump0Up = "anim_Jump0Up";
    private const string Jump0Down = "anim_Jump0Down";
    private const string Jump1Up = "anim_Jump1Up";
    private const string Jump1Down = "anim_Jump1Down";
    private const string Dead = "anim_Dead";
    [Header("PopUp_Anim")]
    public Animator pop_Anim;
    private const string In = "Pop_In";
    private const string Out = "Pop_Out";
    [Header("Audio")]
    private AudioSource aS;
    [Range(0,1)]
    public float jumpSFXVolume;
    public AudioClip jumpSFX;
    public AudioClip elevatorSFX;
    [Range(0, 1)]
    public float jumpPadSFXVolume;
    public AudioClip jumpPadSFX;
    [Range(0, 1)]
    public float bodyThumpSFXVolume;
    public AudioClip bodyThumpSFX;


    #endregion

    #region UnityMethods
    private void Awake()
    {
        aS = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sceneTransition = GameObject.FindGameObjectWithTag("Scene");
        rM = GameObject.FindGameObjectWithTag("Room").GetComponent<RoomManager>(); 
        popUp.SetActive(false);
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
        CheckNextLevel();        
        rb.gravityScale = gravidade;
        popUp.transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Jump pads
        if (other.CompareTag("Jump"))
        {
            float padForce = other.GetComponent<Jumppad>().Force;
            aS.volume = jumpPadSFXVolume;
            aS.PlayOneShot(jumpPadSFX);
            rb.velocity = new Vector2(rb.velocity.x, padForce);
        }

        //Next level
        if (other.CompareTag("Next"))
        {
            if (rM.howManyOrbs == rM.orbsNeeded)
            {
                canNextLevel = true;
                popUp.transform.rotation = Quaternion.identity;
                popUp.SetActive(true);
                pop_Anim.Play(In);
            }
        }
        //Rececao
        if (other.CompareTag("Recept"))
        {
            popUp.transform.rotation = Quaternion.identity;
            popUp.SetActive(true);
            pop_Anim.Play(In);
        }

        //Death
        if (other.CompareTag("Final"))
        {
            popUp.transform.rotation = Quaternion.identity;
            popUp.SetActive(true);
            pop_Anim.Play(In);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Next"))
        {
            canNextLevel = false;
            pop_Anim.Play(Out);
        }
        if (other.CompareTag("Recept"))
        {
            pop_Anim.Play(Out);
        }
    }

    #endregion


    #region BasicMovementMethods
    //Detetar Input de movimento -1 para esquerda, 1 para direita
    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");
    }
    private void CheckGround()
    {
        //isGrounded = Physics2D.BoxCast(groundCheck.position, new Vector2(xValue, yValue), 0, Vector2.down, groundCheckRadius, whatIsGround);
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(xValue, yValue),0,whatIsGround);
    }
    private void CheckJump()
    {
        //Reset Jumps
        if (isGrounded && totalJumps > 0 && (coyote_timer > coyote_seconds))
        {
            totalJumps = 0;
        }
        else if (!isGrounded && (coyote_timer > coyote_seconds) && totalJumps == 0)
        {
            totalJumps = 1;
        }


        coyote_timer += Time.deltaTime;
        if (canJump)
        {
            //Coyote Timer
            if (isGrounded)
            {
                coyote_timer = 0;
            }
            

            //JumpBuffer
            JumpBuffer_Timer -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) && !isGrounded)
            {
                JumpBuffer_Timer = JumpBuffer_Seconds;
            }


            //The Jumping
            if (((JumpBuffer_Timer > 0) && isGrounded && totalJumps < howManyJumps)) //Jumps automatically
            {
                Jump();
            }

            //--------------
            if (((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && coyote_timer <= coyote_seconds && totalJumps < howManyJumps))
            {
                Jump();
            }
        }

    }
    private void Jump()
    {
        totalJumps++;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        JumpBuffer_Timer = 0;
        aS.volume = jumpSFXVolume;
        aS.PlayOneShot(jumpSFX);
    }
    private void ApplyMovement()
    {
        if ((movementInputDirection == 1 && canWalkRight) || (movementInputDirection == -1 && canWalkLeft))
        {
            float targetVelocityX = movementSpeed * movementInputDirection;
            rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, targetVelocityX, ref velocityXSmooth, smoothTime), rb.velocity.y);
        }else if ((movementInputDirection == 1 && !canWalkRight) || (movementInputDirection == -1 && !canWalkLeft))
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
        }
        else 
        {
            float targetVelocityX = movementSpeed * movementInputDirection;
            rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, targetVelocityX, ref velocityXSmooth, smoothTime), rb.velocity.y);
        }
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

    #endregion

    #region Other
    void CheckNextLevel()
    {
        if (Input.GetKeyDown(KeyCode.E) && canNextLevel)
        {
            sceneTransition.GetComponent<Animator>().Play("anim_SceneOut");
            sceneTransition.GetComponent<AudioSource>().PlayOneShot(elevatorSFX);
            StartCoroutine(LoadLevel());
            canNextLevel = false;
        }
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion

    #region Visuals

    void CheckAnimation()
    {
        if (!isDead)
        {
            if (/*rb.velocity.x == 0 &&*/ isGrounded && movementInputDirection == 0) //Is stopped
            {
                anim.Play(IdleAnim);
            }
            else if (rb.velocity.x != 0 && isGrounded && movementInputDirection != 0) //Is walking
            {
                anim.Play(RunAnim);

            }
            else if (rb.velocity.x == 0 && !isGrounded && rb.velocity.y > 0) //Is jumping with no x movement
            {
                anim.Play(Jump0Up);
            }
            else if (rb.velocity.x == 0 && !isGrounded && rb.velocity.y < 0) //Is falling with no x movement
            {
                anim.Play(Jump0Down);
            }
            else if (rb.velocity.x != 0 && !isGrounded && rb.velocity.y > 0) //Is jumping with x movement
            {
                anim.Play(Jump1Up);
            }
            else if (rb.velocity.x != 0 && !isGrounded && rb.velocity.y < 0) //is falling with x movement
            {
                anim.Play(Jump1Down);
            }
        }
        if (isDead)
        {
            anim.Play(Dead);
        }
    }

    private void Flip()
    {
        if (Time.timeScale != 0)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
        }
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
        //Gizmos.DrawCube(groundCheck.position, new Vector2(xValue, yValue));
        Gizmos.DrawWireCube(groundCheck.position, new Vector2(xValue, yValue));
    }

    #endregion
}
