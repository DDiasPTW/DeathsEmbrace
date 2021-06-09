using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sacrifice_Orb : MonoBehaviour
{
    public GameObject Sacrifice_UI;
    public bool isCaught = false;

    private void Awake()
    {
       Sacrifice_UI = GameObject.FindGameObjectWithTag("Sacrifice");
    }
    void Start()
    {
        //Sacrifice_UI.SetActive(false);
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
            for (int i = 0; i < Sacrifice_UI.transform.childCount; i++)
            {
                var child = Sacrifice_UI.transform.GetChild(i).gameObject;
                child.SetActive(true);
            }
            Time.timeScale = 0;
            isCaught = true;
            gameObject.SetActive(false);
        }
    }
}
