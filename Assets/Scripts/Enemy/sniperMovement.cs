using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sniperMovement : MonoBehaviour {

	Transform barrel;
	Transform player;               // Reference to the player's position.
	PlayerHealth playerHealth;      // Reference to the player's health.
	EnemyHealth enemyHealth;        // Reference to this enemy's health.
	public float attackDamage = 10f;
	float timer;
	float rayCastTimer;
	public float rayCastLineDelta = 1f;
	public float timeBetweenAttacks = 10f;
	float rotationSpeed;
	RaycastHit hit;
	Ray barrelRay;
	LineRenderer line;

	// Use this for initialization
	void Start () {
		
	}

	void Awake () {

		//Init player, enemies, and raycast objects
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		barrel = GameObject.Find ("firepoint").transform;
//		barrelRay = new Ray (barrel.position, player.position);
		line = barrel.GetComponent<LineRenderer>();

		rayCastTimer += 0.0f;

	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		rayCastTimer += Time.deltaTime;
		/* Lock x and z rotation to 0 so enem only rotates on y axis*/
		//Look at player
		transform.LookAt(player);
		Vector3 newRotation = Quaternion.Slerp(transform.rotation,
 		Quaternion.LookRotation(player.position - transform.position), rotationSpeed*Time.deltaTime).eulerAngles;
		transform.eulerAngles = new Vector3(90, newRotation.y, 0);
  		newRotation.x = 0;
   		newRotation.z = 0;
   		//transform.rotation = Quaternion.Euler(newRotation);

		//Attack in time intervals
		if (timer >= timeBetweenAttacks) {
			Attack ();
//			print ("Sniper Attacking");
		}
//			Debug.Log (rayCastTimer);
		//Disable line renderer
		if (rayCastTimer >= rayCastLineDelta){
//			Debug.Log ("Sniper line disabled");
			line.enabled = false;
			rayCastTimer = 0f;
		}
	}

	void Attack ()
	{
		
		timer = 0f;
		barrelRay = new Ray (barrel.transform.position, player.transform.position - barrel.transform.position);

		//Check if player is in range

		//Fire raycast
		if (Physics.Raycast (barrelRay, out hit, 3000)) {
//			Debug.Log (barrel.transform.position + "\t" + player.transform.position);
//			Debug.DrawLine (barrel.transform.position, hit.transform.position, Color.green, 2f, false);
			//Enable line renderer
			line.SetPosition(0, barrel.position);
			line.SetPosition (1, hit.point);
			line.enabled = true;


//			Debug.Log (rayCastTimer);
//			if (rayCastTimer >= rayCastLineDelta){
//				Debug.Log ("Line renderer disabled");
//				line.enabled = false;
//				rayCastTimer = 0f;
//			}

			//If raycast hits player, deal damage
			if (hit.collider.tag == "Player") {
				print ("Sniper hit player");
				if (playerHealth.currentHealth >= 0) {
					playerHealth.TakeDamage (attackDamage);
				}
			}
		}
	}

}
