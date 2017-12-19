using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointPotion : MonoBehaviour {

	GameObject timerObj;
	GameTimer currTime;
	PlayerHealth playerHealth;
	Transform player;
	bool pickedUp = false;
	float minDist = 5;
	float currDist;
	
    public int scoreval = 500;
    // Use this for initialization
    void Start ()
	{

		timerObj = GameObject.FindGameObjectWithTag ("Timer");
		currTime = timerObj.GetComponent <GameTimer> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		transform.Rotate (0, 5, 0, Space.Self);
		currDist = Vector3.Distance (player.position, transform.position);

		if (!pickedUp && currDist <= minDist) {
            ScoreManager.score += scoreval;
            pickedUp = true;
			
			DestroyObject (gameObject, 0.5f);
		} 
	}
}