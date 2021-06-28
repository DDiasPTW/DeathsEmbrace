using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sacrifice_UI : MonoBehaviour
{
    private PlayerMovement pM;
    public Button SacLeft, SacRight, SacJump, SacOrb;
    
    private AudioSource aS;
    
    [Range(0,1)]
    public float clickVolume;
    public AudioClip UI_Click;
    private void Awake()
    {
        pM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        aS = GetComponent<AudioSource>();
    }

    private void Update()
    {
        aS.volume = clickVolume;
        if (!pM.canWalkLeft)
        {
            SacLeft.interactable = false;
        }else SacLeft.interactable = true;

        if (!pM.canWalkRight)
        {
            SacRight.interactable = false;
        }
        else SacRight.interactable = true;

        if (!pM.canJump)
        {
            SacJump.interactable = false;
        }
        else SacJump.interactable = true;

        if (!pM.canOrb)
        {
            SacOrb.interactable = false;
        }
        else SacOrb.interactable = true;
    }

    public void SacrificeLeft()
    {
        pM.canWalkLeft = false;
        Time.timeScale = 1;
        SacLeft.interactable = false;
        aS.PlayOneShot(UI_Click);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var child = gameObject.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
        //gameObject.SetActive(false);
    }
    public void SacrificeRight()
    {
        pM.canWalkRight = false;
        Time.timeScale = 1;
        SacRight.interactable = false;
        aS.PlayOneShot(UI_Click);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var child = gameObject.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
        //gameObject.SetActive(false);
    }

    public void SacrificeJump()
    {
        pM.canJump = false;
        Time.timeScale = 1;
        SacJump.interactable = false;
        aS.PlayOneShot(UI_Click);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var child = gameObject.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
        //gameObject.SetActive(false);
    }

    public void SacrificeOrb()
    {
        pM.canOrb = false;
        Time.timeScale = 1;
        SacOrb.interactable = false;
        aS.PlayOneShot(UI_Click);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var child = gameObject.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
        //gameObject.SetActive(false);
    }
}
