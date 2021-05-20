using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class OrbMovement : MonoBehaviour
{
    private CircleCollider2D cc; //Has to be trigger -> Detects collision with "TriggerBoxes"
    private SpriteRenderer sr; //Is disabled when it gets "caught"
    private Rigidbody2D rb; 
    private Transform playerTarget; //Has to have a tag in order to get set automatically in Awake()
    Vector2 caughtTarget; //Gets set when orb hits a trigger box
    Vector2 mouseTarget; //Gets set when player leftClicks with mouse
    Vector2 position; //Gets set every frame, uses the playerTarget
    public float speed; //Speed at which the orb follows the player
    public float launchSpeed; //Speed at which the orb travels to the mouseTarget
    public float caughtSpeed; //Speed at which the orb moves to the TriggerBox (set to 1 so it goes instantly)
    public bool isLaunched = false; //is true when player leftClicks, false when rightClicks/by default
    public bool isCaught = false; //is true when orb hits the "TriggerBoxes"
    private Vector3 mousePos;
    private Vector2 mousePos2D;
    private Vector2 clickPos = Vector2.zero;

    #region UnityMethods
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        cc.isTrigger = true;
        position = new Vector2(transform.position.x,transform.position.y);
        playerTarget = GameObject.FindGameObjectWithTag("OrbTarget").transform;
    }
    void Update()
    {
        CheckTargets();
        SetVisuals();
        
    }
    private void FixedUpdate()
    {
        CheckStatus();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TB") && isLaunched)
        {
            isCaught = true;
            caughtTarget = Vector2.Lerp(transform.position, other.transform.position, caughtSpeed);
        }
    }

    #endregion


    #region CustomMethods
    void CheckTargets()
    {
        //Sets orb target position to the playerTarget GO
        position = Vector2.Lerp(transform.position, playerTarget.position, speed);

        //Detect mouse click
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos2D = new Vector2(mousePos.x, mousePos.y);
        mouseTarget = Vector2.Lerp(transform.position, clickPos, launchSpeed);
        //Sends the orb to mousePos
        if (Input.GetMouseButtonDown(0) && !isLaunched)
        {
            isLaunched = true;
            clickPos = mousePos2D;
        }
        //Recalls the orb
        if (Input.GetMouseButtonDown(1) && isLaunched)
        {
            isLaunched = false;
            if (isCaught)
            {
                isCaught = false;
            }
        }
    }

    void CheckStatus()
    {
        //Applies the movement to the orb
        if (!isLaunched)
        {
            rb.MovePosition(position);
        }
        if (isLaunched)
        {
            rb.MovePosition(mouseTarget);
        }
        if (isCaught)
        {
            rb.MovePosition(caughtTarget);
        }
    }

    void SetVisuals()
    {
        if (isCaught)
        {
            sr.enabled = false;
        }
        else sr.enabled = true;
    }

    #endregion


}
