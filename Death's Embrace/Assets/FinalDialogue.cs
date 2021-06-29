using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalDialogue : MonoBehaviour
{
    public GameObject Popup;
    private PlayerMovement pM;
    private OrbMovement oM;
    private bool canTalk = false;
    public GameObject DialogueBox;
    public GameObject continueButton;
    public Text continueButtonText;
    public Transform newPositionOrb;

    private AudioSource aS;
    [Range(0, 1)]
    public float volume;
    public AudioClip dialogueSFX;

    [Header("Text")]
    public Text DialogueText;
    [TextArea(1, 5)]
    public string[] frases;
    public string[] continueButtonTexts;
    [SerializeField] private int index;
    public float typingSpeed;

    private void Awake()
    {
        DialogueBox.SetActive(false);
        DialogueText.text = "";
        continueButton.SetActive(false);
        aS = GetComponent<AudioSource>();
        pM = GetComponent<PlayerMovement>();
        oM = GameObject.FindGameObjectWithTag("Orb").GetComponent<OrbMovement>();
    }

    private void Update()
    {
        DialogueBox.transform.rotation = Quaternion.identity;
        if (Input.GetKeyDown(KeyCode.E) && canTalk)
        {
            Popup.GetComponent<SpriteRenderer>().enabled = false;
            DialogueBox.SetActive(true);
            pM.canWalkRight = false;
            StartCoroutine(Type());
        }


        if (DialogueText.text == frases[index])
        {
            continueButton.SetActive(true);
            continueButtonText.text = continueButtonTexts[index];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Final"))
        {
            canTalk = true;
            pM.canWalkRight = false;
        }
    }



    IEnumerator Type()
    {
        foreach (char letter in frases[index].ToCharArray())
        {
            DialogueText.text += letter;
            aS.volume = volume;
            aS.PlayOneShot(dialogueSFX);
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextFrase()
    {
        continueButton.SetActive(false);
        if (index < frases.Length - 1)
        {
            index++;
            DialogueText.text = "";
            StartCoroutine(Type());
        }
        else
        {
            DialogueText.text = "";
            Popup.SetActive(false);
            oM.isDead = true;
        }
    }
}
