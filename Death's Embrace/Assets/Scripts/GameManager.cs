using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int whatLevelUnlocked;

    private void Start()
    {
        if (PlayerPrefs.GetInt("LevelUnlocked") == 0)
        {
            PlayerPrefs.SetInt("LevelUnlocked", 1);
        }
        
        whatLevelUnlocked = PlayerPrefs.GetInt("LevelUnlocked");
    }
    void Update()
    {
        whatLevelUnlocked = PlayerPrefs.GetInt("LevelUnlocked");
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("Lvl_MainMenu");
        }
    }
}
