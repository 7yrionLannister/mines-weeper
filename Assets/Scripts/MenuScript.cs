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
        string line;
        try
        {
            StreamReader sr = new StreamReader(@"Assets/Data/Scores.txt");

            line = "";

            while ((line = sr.ReadLine()) != null)
            {
                Debug.Log(line);
            }

            sr.Close();
            //Console.ReadLine();
        }
        catch (System.Exception e)
        {
            Debug.Log("Exception: " + e.Message);
        }
    }
}
