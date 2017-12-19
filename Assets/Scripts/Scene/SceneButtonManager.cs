using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonManager : MonoBehaviour{
	public void NewGameButton(string MainGame){
		SceneManager.LoadScene("MainGame");
	}

	public void ExitButton(){
		Application.Quit();
	}

	public void OptionsButton(string Options){
		SceneManager.LoadScene ("OptionsMenu");
	}
}
