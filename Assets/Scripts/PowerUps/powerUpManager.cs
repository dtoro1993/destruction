using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpManager : MonoBehaviour {

	Transform player;
	PlayerHealth playerHealth;
	public float spawnTime = 20f;
	public Transform[] spawnPoints;
	public GameObject powerUp;

	// Use this for initialization
	void Start () {

		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Spawn ()
	{
		if (playerHealth.currentHealth <= 0f) {
			return;
		} else {

			int spawnPointIndex = Random.Range (0, spawnPoints.Length);

			Instantiate (powerUp, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
		}
	}

}
