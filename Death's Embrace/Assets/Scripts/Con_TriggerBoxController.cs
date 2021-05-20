using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Con_TriggerBoxController : MonoBehaviour
{
    public GameObject collToActivate;
    public GameObject otherBox;
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
                otherBox.GetComponent<Con_TriggerBoxController>().collToActivate.SetActive(true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<OrbMovement>().isCaught)
        {
            collToActivate.SetActive(true);
            otherBox.GetComponent<Con_TriggerBoxController>().collToActivate.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.GetComponent<OrbMovement>().isCaught)
        {
            collToActivate.SetActive(false);
            otherBox.GetComponent<Con_TriggerBoxController>().collToActivate.SetActive(false);

        }
    }
}
