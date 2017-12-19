﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
		
	public void StartGame()
	{
		SceneManager.LoadScene ("MainGame");
	}

	public void Quit()
	{
		Application.Quit ();
	}

	/*public void Main_Menu(){
		SceneManager.LoadScene (1);
	}*/
}
