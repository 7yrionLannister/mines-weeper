using TMPro;
using UnityEngine;

public class HighScoreScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void Setup()
    {
        Debug.Log("ping-pong");
        string[] scores = MinesWeeperGameHandler.LoadScores(MenuScript.level);
        for (int i = 0; i < scores.Length; i++)
        {
            transform.Find("slot" + (i + 1)).GetComponent<TMP_Text>().text = (i+1) + ". " + scores[i];
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    
}
