using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("Music");
        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
