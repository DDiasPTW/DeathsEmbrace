using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    private PlayerMovement pM;
    private BoxCollider2D bCol;
    private bool canShow = false;
    public GameObject DialogueUI;
    public GameObject continueButton;
    public GameObject NoButton;
    public Text continueButtonText;
    [Header("Text")]
    public Text DialogueText;
    [TextArea (1,5)]
    public string[] frases;
    public string[] continueButtonTexts;
    public int NoButton_Index;
    [SerializeField]private int index;
    public float typingSpeed;


    private void Awake()
    {
        DialogueText.text = "";
        DialogueUI.SetActive(false);
        continueButton.SetActive(false);
        NoButton.SetActive(false);
        pM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        bCol = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (canShow && Input.GetKeyDown(KeyCode.E))
        {
            bCol.enabled = false;
            canShow = false;
            DialogueUI.SetActive(true);
            StartCoroutine(Type());
            pM.canJump = false;
            pM.canOrb = false;
            pM.canWalkLeft = false;
            pM.canWalkRight = false;
        }

        if (DialogueText.text == frases[index])
        {
            continueButton.SetActive(true);
            if (NoButton_Index == index)
            {
                NoButton.SetActive(true);
            }
            else { NoButton.SetActive(false); }
            continueButtonText.text = continueButtonTexts[index];
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in frases[index].ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextFrase()
    {
        continueButton.SetActive(false);
        NoButton.SetActive(false);
        if (index < frases.Length - 1)
        {
            index++;
            DialogueText.text = "";
            StartCoroutine(Type());
        }
        else
        {
            DialogueText.text = "";
            DialogueUI.SetActive(false);
            pM.canJump = true;
            pM.canOrb = true;
            pM.canWalkLeft = true;
            pM.canWalkRight = true;
        }
    }

    public void NoMeme()
    {
        SceneManager.LoadScene("Lvl_Credits_Meme");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canShow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canShow = false;
            DialogueUI.SetActive(false);
            DialogueText.text = "";
        }
    }
}
