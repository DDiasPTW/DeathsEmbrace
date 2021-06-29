using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sacrifice_Orb : MonoBehaviour
{
    public GameObject Sacrifice_UI;
    public bool isCaught = false;

    private AudioSource aS;
    [Range(0,1)]
    public float volume;
    public AudioClip pickUp_SacrificeSFX;
    private void Awake()
    {
       Sacrifice_UI = GameObject.FindGameObjectWithTag("Sacrifice");
        aS = GetComponent<AudioSource>();
    }
    void Start()
    {
        //Sacrifice_UI.SetActive(false);
        aS.volume = volume;
        for (int i = 0; i < Sacrifice_UI.transform.childCount; i++)
        {
            var child = Sacrifice_UI.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Sacrifice_UI.SetActive(true);
            aS.PlayOneShot(pickUp_SacrificeSFX);
            for (int i = 0; i < Sacrifice_UI.transform.childCount; i++)
            {
                var child = Sacrifice_UI.transform.GetChild(i).gameObject;
                child.SetActive(true);
            }
            Time.timeScale = 0;
            isCaught = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine(DisableOrb());
            
        }
    }

    IEnumerator DisableOrb()
    {
        yield return new WaitForSeconds(pickUp_SacrificeSFX.length);
        gameObject.SetActive(false);
    }
}
