using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private AudioSource aS;
    [Range(0,1)]
    public float volume;
    public AudioClip UI_Click;
    private void Awake()
    {
        aS = GetComponent<AudioSource>();
        aS.volume = volume;
    }

    public void Continue()
    {
        aS.PlayOneShot(UI_Click);
        StartCoroutine(GoContinue());      
    }

    public void Quit()
    {
        aS.PlayOneShot(UI_Click);
        Application.Quit();
    }

    public void MainMenu()
    {
        aS.PlayOneShot(UI_Click);
        StartCoroutine(GoMenu());
    }

    IEnumerator GoContinue()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        yield return new WaitForSeconds(UI_Click.length);
    }

    IEnumerator GoMenu()
    {
        SceneManager.LoadScene("Lvl_MainMenu");
        yield return new WaitForSeconds(UI_Click.length);
    }
}
