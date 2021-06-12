using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Auto")]
    public List<GameObject> Sacrifice_Orbs;
    public int howManyOrbs = 0;
    public int orbsNeeded;
    [Header("Manual")]
    public int currentLevel;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("LevelUnlocked") < currentLevel)
        {
            PlayerPrefs.SetInt("LevelUnlocked",currentLevel);
        }
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var child = gameObject.transform.GetChild(i);
            if (child.CompareTag("Sacrifice_Orb"))
            {
                Sacrifice_Orbs.Add(child.gameObject);
            }
        }
    }

    void Start()
    {
        howManyOrbs = 0;
        orbsNeeded = Sacrifice_Orbs.Count;
    }

    
    void Update()
    {
        for (int i = 0; i < Sacrifice_Orbs.Count; i++)
        {
            if (Sacrifice_Orbs[i].GetComponent<Sacrifice_Orb>().isCaught)
            {
                howManyOrbs++;
                Sacrifice_Orbs.RemoveAt(i);
            }
        }
    }
}
