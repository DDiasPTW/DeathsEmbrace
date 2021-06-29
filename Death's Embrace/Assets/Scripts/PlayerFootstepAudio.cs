using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepAudio : MonoBehaviour
{
    private AudioSource aS;
    [Range(0, 1)]
    public float footstepSFXVolume;
    public AudioClip footstepSFX;

    private void Awake()
    {
        aS = GetComponent<AudioSource>();
    }

    void PlayStep()
    {
        aS.volume = footstepSFXVolume;
        aS.PlayOneShot(footstepSFX);
    }
}
