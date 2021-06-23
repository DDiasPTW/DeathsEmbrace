using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int whatLevelUnlocked;

    private void Start()
    {       
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
