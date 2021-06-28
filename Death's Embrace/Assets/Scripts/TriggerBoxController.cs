using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoxController : MonoBehaviour
{
    public GameObject collToActivate;


    private void Awake()
    {
        collToActivate.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Orb"))
        {
            if (other.GetComponent<OrbMovement>().isCaught)
            {
                collToActivate.SetActive(true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Orb"))
        {
            if (other.GetComponent<OrbMovement>().isCaught)
            {
                collToActivate.SetActive(true);
            }
            else
            {
                collToActivate.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Orb"))
        {
            if (!other.GetComponent<OrbMovement>().isCaught)
            {
                collToActivate.SetActive(false);
            }
        } 
    }
}
