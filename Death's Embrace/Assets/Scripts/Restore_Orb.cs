using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restore_Orb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovement>().canJump == false)
            {
                other.GetComponent<PlayerMovement>().canJump = true;
                gameObject.SetActive(false);
            }
        }
    }
}
