using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CircleCollider2D))]
public class OrbMovement : MonoBehaviour
{
    private CircleCollider2D cc; //Has to be trigger -> Detects collision with "TriggerBoxes"
    private SpriteRenderer sr; //Is disabled when it gets "caught"
    private Rigidbody2D rb;
    private FinalDialogue fD;
    private Transform playerTarget; //Has to have a tag in order to get set automatically in Awake()
    private PlayerMovement playerM;
    Vector2 caughtTarget; //Gets set when orb hits a trigger box
    Vector2 mouseTarget; //Gets set when player leftClicks with mouse
    public Vector2 position; //Gets set every frame, uses the playerTarget
    public float speed; //Speed at which the orb follows the player
    public float launchSpeed; //Speed at which the orb travels to the mouseTarget
    public float caughtSpeed; //Speed at which the orb moves to the TriggerBox (set to 1 so it goes instantly)
    public bool isLaunched = false; //is true when player leftClicks, false when rightClicks/by default
    public bool isCaught = false; //is true when orb hits the "TriggerBoxes"
    public bool isDead = false; //is true in the final level
    private Vector3 mousePos;
    private Vector2 mousePos2D;
    private Vector2 clickPos = Vector2.zero;
    private bool isLastRoom = false;

    private GameObject sceneTransition;
    public GameObject Death;

    private AudioSource aS;
    [Range(0, 1)]
    public float volumeInSFX;
    public AudioClip InSFX;
    [Range(0, 1)]
    public float volumeOutSFX;
    public AudioClip OutSFX;
    [Range(0, 1)]
    public float volumeExplodeSFX;
    public AudioClip ExplodeSFX;
    public GameObject particles;

    #region UnityMethods
    private void Awake()
    {  
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        aS = GetComponent<AudioSource>();
        playerM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        fD = GameObject.FindGameObjectWithTag("Player").GetComponent<FinalDialogue>();
        cc.isTrigger = true; //in case i forget to change in inspector
        position = new Vector2(transform.position.x,transform.position.y);
        playerTarget = GameObject.FindGameObjectWithTag("OrbTarget").transform;
        sceneTransition = GameObject.FindGameObjectWithTag("Scene");
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
            aS.volume = volumeInSFX;
            aS.PlayOneShot(InSFX);
            caughtTarget = other.transform.position;
        }

        if (other.CompareTag("Death"))
        {
            isLastRoom = true;
        }

        if (other.CompareTag("Cut"))
        {
            isCaught = true;
            aS.PlayOneShot(ExplodeSFX);
            particles.SetActive(true);
            StartCoroutine(Die());
            StartCoroutine(End());           
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(.5f);
        playerM.isDead = true;
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(1.5f);
        sceneTransition.GetComponent<Animator>().Play("anim_SceneOut");
        StartCoroutine(Credits());
    }

    IEnumerator Credits()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Lvl_Credits");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("TB") && isLaunched)
        {
            isCaught = true;
            caughtTarget = other.transform.position;
        }
    }

    #endregion


    #region CustomMethods
    void CheckTargets()
    {
        //Sets orb target position to the playerTarget GO
        position = Vector2.Lerp(transform.position, playerTarget.position, speed);

        if (playerM.canOrb && Time.timeScale != 0 && !isLastRoom)
        {
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
                    aS.volume = volumeOutSFX;
                    aS.PlayOneShot(OutSFX);
                }
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
            gameObject.transform.position = clickPos;
        }
        if (isCaught && !isDead)
        {
            gameObject.transform.position = caughtTarget;
        }
        if (isDead)
        {
            rb.MovePosition(Vector2.Lerp(transform.position, fD.newPositionOrb.position,speed));
            Death.GetComponent<Animator>().Play("Death_Smash");
        }
    }

    void SetVisuals()
    {
        if (isCaught || !playerM.canOrb)
        {
            sr.enabled = false;
        }
        else sr.enabled = true;
    }

    #endregion


}
