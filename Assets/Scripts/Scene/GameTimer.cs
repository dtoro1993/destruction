using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour {
    //float timeAmt;
	public float time;
	public Text Timer;
   	public float timeLength;
	playMove playerMovement;

	// Use this for initialization
	void Start () {
        time = timeLength;
		playerMovement = GetComponent <playMove> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (time >= 167) {
			Timer.color = Color.green;
			Timer.text = time.ToString ("f1") + " TIME";
			time -= Time.deltaTime;
		} else if (time <= 166 && time >= 84) {
			Timer.color = Color.yellow;
			Timer.text = time.ToString ("f1") + " TIME";
			time -= Time.deltaTime;
		} else if (time <= 83 && time >= 0){
			Timer.color = Color.red;
			Timer.text = time.ToString ("f1") + " TIME";
			time -= Time.deltaTime;
		} else{
			time -= Time.deltaTime;
		}
	}
}