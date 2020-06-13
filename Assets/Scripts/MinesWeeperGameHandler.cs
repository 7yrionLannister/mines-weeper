using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinesWeeperGameHandler : MonoBehaviour
{
    public static Sprite[] spritesArray;
    public static readonly int MINE_INDEX = 9;
    public static readonly int FLAG_INDEX = 10;
    public static readonly int COVERED_INDEX = 11;

    public Sprite empty;
    public Sprite covered;
    public Sprite flag;
    public Sprite mine;
    public Sprite mine1;
    public Sprite mine2;
    public Sprite mine3;
    public Sprite mine4;
    public Sprite mine5;
    public Sprite mine6;
    public Sprite mine7;
    public Sprite mine8;

    private Map map;
    private TMP_Text timer;

    private void Start()
    {
        spritesArray = new Sprite[] {
            empty,
            mine1,
            mine2,
            mine3,
            mine4,
            mine5,
            mine6,
            mine7,
            mine8,
            mine,
            flag,
            covered,
        };

        switch (MenuScript.level)
        {
            case 2:
                map = new Map(15, 20, transform.position);
                break;
            case 3:
                map = new Map(30, 21, transform.position);
                break;
            default:
                map = new Map(10, 10, transform.position);
                break;
        }
        
        transform.Find("GameOverScreen").gameObject.SetActive(false);
        transform.Find("WinnerScreen").gameObject.SetActive(false);

        timer = transform.Find("CanvasTimer").Find("SecondsText").GetComponent<TMP_Text>();
        int secs = Convert.ToInt32(timer.text);
        FunctionPeriodic.Create(() => {
            if (transform.Find("GameOverScreen").gameObject.activeSelf || transform.Find("WinnerScreen").gameObject.activeSelf)
            {
                return;
            }
            secs++;
            timer.text = secs+"";
        }, 1f);
    }

    private void Update()
    {
        if (transform.Find("GameOverScreen").gameObject.activeSelf || transform.Find("WinnerScreen").gameObject.activeSelf)
        {
            return;
        }



        if (Input.GetMouseButtonDown(0))
        {
            if (map.Reveal(UtilsClass.GetMouseWorldPosition()) == MapTileGridObject.Type.Mine)
            {
                map.Uncover();
                transform.Find("GameOverScreen").gameObject.SetActive(true);
            }
            else if(map.RevealedTiles == (map.Width*map.Height - map.Mines)) {
                //Add functionality for saving scores before prompting to play again
                transform.Find("WinnerScreen").gameObject.SetActive(true);
                SaveScore();
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            map.Flag(UtilsClass.GetMouseWorldPosition());
        }
    }

    public void PlayAgainButtonPressed()
    {
        SceneManager.LoadScene(0); //Load the Main Scene again so the game starts from zero (Start is called)
    }

    public void SaveScore()
    {
        try
        {
            if (!Directory.Exists(@"Assets/Data"))
            {
                Directory.CreateDirectory(@"Assets/Data");
            }

            WriteScore("NULL", Convert.ToInt32(timer.text));
        }
        catch (System.Exception e)
        {
            Debug.Log("Exception: " + e.Message);
        }
    }

    private void WriteScore(string name, int score)
    {

        string[] scores = LoadScores(MenuScript.level);
        ScoreComparer scoreComparer = new ScoreComparer();
        string newScore = name + ":" + score;
        for (int i = 0; i < scores.Length; i++)
        {
            if (scoreComparer.Compare(newScore, scores[i]) > 0 || scores[i] == null)
            {
                scores[scores.Length - 1] = newScore;
                break;
            }
        }

        Array.Sort(scores, scoreComparer);
        StreamWriter sw = new StreamWriter(@"Assets/Data/Scores" + MenuScript.level + ".txt", false);
        Debug.Log("******");
        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] != null)
            {
                sw.WriteLine(scores[i]);
                Debug.Log(scores[i]);
            }
        }
        sw.Close();
    }

    public static string[] LoadScores(int level)
    {
        string[] scores = new string[10];

        try
        {
            string record;
            StreamReader sr = new StreamReader(@"Assets/Data/Scores" + level + ".txt");

            record = "";
            int i = 0;
            while ((record = sr.ReadLine()) != null && i < scores.Length)
            {
                scores[i] = record;
                i++;
            }

            sr.Close();
            //Console.ReadLine();
        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e.Message);
        }

        return scores;
    }

    public class ScoreComparer : IComparer<string>
    { 
        public int Compare(string x, string y)
        {
            int comp = 0;
            if (x != null && y == null)
            {
                comp = -1;
            }
            else if (x == null && y != null)
            {
                comp = 1;
            }
            else if(x != null && y != null){
                comp = Convert.ToInt32(x.Split(':')[1]) - Convert.ToInt32(y.Split(':')[1]);
            }
            return comp;
        }
    }
}
