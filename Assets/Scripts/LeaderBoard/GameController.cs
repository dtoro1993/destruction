using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	private ScoreManager score;
    public InputField playerName;

    // Use this for initialization
    void Start()
	{

    }

    public void InitialsEntered()
    {
        GetComponent<LeaderBoard>().CheckForHighScore(ScoreManager.score, playerName.text);
    }


    // Update is called once per frame
    void Update()
    {
 
    }
}
