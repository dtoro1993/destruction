using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

	GameObject PauseMenu;
	bool paused;

	// Use this for initialization
	void Start () {
		paused = false;
		PauseMenu = GameObject.Find("PauseMenu");
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			paused = !paused;

		}
		if (paused) {
			PauseMenu.SetActive (true);
			Time.timeScale = 0;
			//Cursor.lockState = CursorLockMode.None;
		} 
		else if (!paused) {
			PauseMenu.SetActive (false);
			Time.timeScale = 1;
			//Cursor.lockState = CursorLockMode.Locked;
		}
	}

	public void Resume(){
		paused = false;
	}

	public void MainMenu(){
		SceneManager.LoadScene ("MainMenu");
	}

	public void Quit(){
		Application.Quit ();
	}
}
