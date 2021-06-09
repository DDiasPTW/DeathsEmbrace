using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sacrifice_UI : MonoBehaviour
{
    private PlayerMovement pM;
    public Button SacLeft, SacRight, SacJump, SacOrb;
    private void Awake()
    {
        pM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {

    }

    public void SacrificeLeft()
    {
        pM.canWalkLeft = false;
        Time.timeScale = 1;
        SacLeft.interactable = false;

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
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var child = gameObject.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
        //gameObject.SetActive(false);
    }
}
