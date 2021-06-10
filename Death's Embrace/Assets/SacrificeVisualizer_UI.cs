using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeVisualizer_UI : MonoBehaviour
{
    private PlayerMovement pM;
    public GameObject leftIm, rightIm, jumpIm, orbIm;

    private void Awake()
    {
        pM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        CheckScrifices();
    }

    void Update()
    {
        CheckScrifices();
    }

    void CheckScrifices()
    {
        if (!pM.canJump)
        {
            jumpIm.SetActive(false);
        }
        else jumpIm.SetActive(true);

        if (!pM.canWalkLeft)
        {
            leftIm.SetActive(false);
        }
        else leftIm.SetActive(true);

        if (!pM.canWalkRight)
        {
            rightIm.SetActive(false);
        }
        else rightIm.SetActive(true);

        if (!pM.canOrb)
        {
            orbIm.SetActive(false);
        }
        else orbIm.SetActive(true);
    }
}
