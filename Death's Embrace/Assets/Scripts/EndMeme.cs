using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMeme : MonoBehaviour
{
    public void EndMemeWoop()
    {
        SceneManager.LoadScene("Lvl_MainMenu");
    }
}
