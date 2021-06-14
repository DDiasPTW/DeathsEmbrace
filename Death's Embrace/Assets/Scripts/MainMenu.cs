using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject levelSelectUI;

    private void Awake()
    {
        levelSelectUI.SetActive(false);
    }
    public void PlayButton()
    {
        levelSelectUI.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
