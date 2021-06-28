using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{
    public Button[] LevelButtons;
    public GameObject levelSelect_UI;
    private AudioSource aS;

    [Range(0, 1)]
    public float clickVolume;
    public AudioClip UI_Click;

    private void Awake()
    {
        aS = GetComponent<AudioSource>();
        for (int i = 0; i < LevelButtons.Length; i++)
        {
            if (PlayerPrefs.GetInt("LevelUnlocked") < i)
            {
                LevelButtons[i].interactable = false;
            }
            else
            {
                LevelButtons[i].interactable = true;
            }
        }
    }

    private void Update()
    {
        aS.volume = clickVolume;
        for (int i = 0; i < LevelButtons.Length; i++)
        {
            if (PlayerPrefs.GetInt("LevelUnlocked") < i)
            {
                LevelButtons[i].interactable = false;
            }
            else
            {
                LevelButtons[i].interactable = true;
            }
        }
    }

    public void LoadLevel1()
    {
        aS.PlayOneShot(UI_Click);
        StartCoroutine(Load1());
    }

    IEnumerator Load1()
    {
        yield return new WaitForSeconds(UI_Click.length);
        SceneManager.LoadScene("Lvl_1");
    }
    public void LoadLevel2()
    {
        aS.PlayOneShot(UI_Click);
        StartCoroutine(Load2());
    }

    IEnumerator Load2()
    {
        yield return new WaitForSeconds(UI_Click.length);
        SceneManager.LoadScene("Lvl_2");
    }
    public void LoadLevel3()
    {
        aS.PlayOneShot(UI_Click);
        StartCoroutine(Load3());
    }

    IEnumerator Load3()
    {
        yield return new WaitForSeconds(UI_Click.length);
        SceneManager.LoadScene("Lvl_3");
    }

    public void LoadLevel4()
    {
        aS.PlayOneShot(UI_Click);
        StartCoroutine(Load4());
    }

    IEnumerator Load4()
    {
        yield return new WaitForSeconds(UI_Click.length);
        SceneManager.LoadScene("Lvl_4");
    }
    public void LoadLevel5()
    {
        aS.PlayOneShot(UI_Click);
        StartCoroutine(Load5());
    }

    IEnumerator Load5()
    {
        yield return new WaitForSeconds(UI_Click.length);
        SceneManager.LoadScene("Lvl_5");
    }
    public void LoadLevel6()
    {
        aS.PlayOneShot(UI_Click);
        StartCoroutine(Load6());
    }

    IEnumerator Load6()
    {
        yield return new WaitForSeconds(UI_Click.length);
        SceneManager.LoadScene("Lvl_6");
    }
    public void LoadLevel7()
    {
        aS.PlayOneShot(UI_Click);
        StartCoroutine(Load7());
    }

    IEnumerator Load7()
    {
        yield return new WaitForSeconds(UI_Click.length);
        SceneManager.LoadScene("Lvl_7");
    }
    public void LoadLevel8()
    {
        aS.PlayOneShot(UI_Click);
        StartCoroutine(Load8());
    }

    IEnumerator Load8()
    {
        yield return new WaitForSeconds(UI_Click.length);
        SceneManager.LoadScene("Lvl_8");
    }
    public void LoadLevel9()
    {
        aS.PlayOneShot(UI_Click);
        StartCoroutine(Load9());
    }

    IEnumerator Load9()
    {
        yield return new WaitForSeconds(UI_Click.length);
        SceneManager.LoadScene("Lvl_9");
    }
    public void LoadLevel10()
    {
        aS.PlayOneShot(UI_Click);
        StartCoroutine(Load10());
    }

    IEnumerator Load10()
    {
        yield return new WaitForSeconds(UI_Click.length);
        SceneManager.LoadScene("Lvl_10");
    }
    public void LoadLevelLair()
    {
        aS.PlayOneShot(UI_Click);
        StartCoroutine(LoadLair());
    }

    IEnumerator LoadLair()
    {
        yield return new WaitForSeconds(UI_Click.length);
        SceneManager.LoadScene("Lvl_Lair");
    }

    public void BackButton()
    {
        aS.PlayOneShot(UI_Click);
        levelSelect_UI.SetActive(false);
    }
}
