using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{
    public Button[] LevelButtons;
    public GameObject levelSelect_UI;

    private void Awake()
    {
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
        SceneManager.LoadScene("Lvl_1");
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Lvl_2");
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene("Lvl_3");
    }
    public void LoadLevel4()
    {
        SceneManager.LoadScene("Lvl_4");
    }
    public void LoadLevel5()
    {
        SceneManager.LoadScene("Lvl_5");
    }
    public void LoadLevel6()
    {
        SceneManager.LoadScene("Lvl_6");
    }
    public void LoadLevel7()
    {
        SceneManager.LoadScene("Lvl_7");
    }
    public void LoadLevel8()
    {
        SceneManager.LoadScene("Lvl_8");
    }
    public void LoadLevel9()
    {
        SceneManager.LoadScene("Lvl_9");
    }
    public void LoadLevel10()
    {
        SceneManager.LoadScene("Lvl_10");
    }
    public void LoadLevelLair()
    {
        SceneManager.LoadScene("Lvl_Lair");
    }

    public void BackButton()
    {
        levelSelect_UI.SetActive(false);
    }
}
