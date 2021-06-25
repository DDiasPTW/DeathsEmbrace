using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restore_Orb : MonoBehaviour
{
    private AudioSource aS;
    public AudioClip pickUp;
    private void Awake()
    {
        aS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovement>().canJump == false)
            {
                aS.PlayOneShot(pickUp);
                other.GetComponent<PlayerMovement>().canJump = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                StartCoroutine(Disable());
            }
        }
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(pickUp.length);
        gameObject.SetActive(false);
    }
}
