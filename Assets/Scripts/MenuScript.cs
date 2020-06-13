using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public static int level;
    public void Play()
    {
        level = GameObject.Find("LevelSelector").GetComponent<TMP_Dropdown>().value + 1;
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowHighScores()
    {
        string[][] scores = new string[3][];
        scores[0] = MinesWeeperGameHandler.LoadScores(1);
        scores[1] = MinesWeeperGameHandler.LoadScores(2);
        scores[2] = MinesWeeperGameHandler.LoadScores(3);
    }
}
