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
    public static int level = 1;
    [SerializeField]
    private Canvas highScoreScreen;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void OnValueChangedLevelSelector(int index)
    {
        level = index + 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowHighScores()
    {
        highScoreScreen.gameObject.SetActive(true);
        highScoreScreen.GetComponent<HighScoreScreen>().Setup();
    }
}
