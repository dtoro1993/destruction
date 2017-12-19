using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeUp : MonoBehaviour {

//	playMove playerMovement;
//	GameTimer gameTimer;
	// Use this for initialization
	void Start () {
//		playerMovement = GetComponent <playMove> ();
//		gameTimer = GetComponent <GameTimer> ();
	}

	// Update is called once per frame
	void Update () {
//		if(gameTimer.time <= 0){
//			playerMovement.enabled = false;
//		}
	}

	public void StartGame()
	{
		SceneManager.LoadScene ("MainMenu");
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
