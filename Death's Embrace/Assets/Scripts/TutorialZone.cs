using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialZone : MonoBehaviour
{
    public GameObject Tut_UI;
    public Text Tut_Text;
    public string textTut;
    private void Awake()
    {
        Tut_UI.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Tut_Text.text = textTut;
            Tut_UI.SetActive(true);
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Tut_Text.text = textTut;
            Tut_UI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Tut_UI.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
