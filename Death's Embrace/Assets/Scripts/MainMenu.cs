using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject levelSelectUI;
    private AudioSource aS;

    [Range(0, 1)]
    public float clickVolume = 0.5f;
    public AudioClip UI_Click;
    private void Awake()
    {
        aS = GetComponent<AudioSource>();
        aS.volume = clickVolume;
        levelSelectUI.SetActive(false);
    }
    public void PlayButton()
    {
        aS.PlayOneShot(UI_Click);
        levelSelectUI.SetActive(true);
    }

    public void QuitButton()
    {
        aS.PlayOneShot(UI_Click);
        Application.Quit();
    }
}
