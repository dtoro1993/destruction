using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPotion : MonoBehaviour {

	PlayerHealth playerHealth;
	Transform player;
	public bool pickedUp = false;
	float minDist = 5;
	float currDist;
	public float healthAmount = 50f;
    public int scoreval = 100;
    // Use this for initialization
    void Start () {
	
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate(0, 5, 0, Space.Self);
        currDist = Vector3.Distance (player.position, transform.position);
		if (!pickedUp && currDist <= minDist) {
			pickedUp = true;
            ScoreManager.score += scoreval;
            playerHealth.healthSlider.value += healthAmount;

            if (playerHealth.currentHealth <= 80) {
				playerHealth.currentHealth += healthAmount;
                DestroyObject (gameObject, 2f);
            } else {
				healthAmount = 100 - playerHealth.currentHealth;
				playerHealth.currentHealth += healthAmount;
                DestroyObject (gameObject, 0.5f);
            }
		}
	}
}
