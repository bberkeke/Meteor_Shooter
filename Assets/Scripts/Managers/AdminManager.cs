using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminManager : MonoBehaviour
{
    public GameDirector GameDirector;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayerPrefs.SetInt("HighestLevelReached", 1);
            GameDirector.RestartLevel();
        }
    }
}
