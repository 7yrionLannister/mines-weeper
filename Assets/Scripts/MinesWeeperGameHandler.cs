using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

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

        map = new Map(20, 15, transform.position);
        transform.Find("GameOverScreen").gameObject.SetActive(false);
        transform.Find("WinnerScreen").gameObject.SetActive(false);
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
}
