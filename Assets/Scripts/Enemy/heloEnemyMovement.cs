using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;


public class heloEnemyMovement : MonoBehaviour
{
	Transform barrel;
	Transform player;
	PlayerHealth playerHealth;
	// Reference to the player's health.
	//EnemyHealth enemyHealth
	// Reference to the player's position.
	//	PlayerHealth playerHealth;      // Reference to the player's health.
	//	EnemyHealth enemyHealth;        // Reference to this enemy's health.
	UnityEngine.AI.NavMeshAgent nav;
	// Reference to the nav mesh agent.
	float rayCastTimer;
	public float rayCastLineDelta = 1f;
	float rotationSpeed;
	float minDist = 70;
	float currDist;
	float timer;
	public float attackDamage = 1f;
	public float timeBetweenAttacks = 1f;
	RaycastHit hit;
	Ray barrelRay; 
	bool isMoving = false;
	Transform helo;
	LineRenderer line;
	Vector3 playerPos;

	void Awake ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		//		playerHealth = player.GetComponent <PlayerHealth> ();
		//		enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
		barrel = GameObject.Find ("launchpoint").transform;
//		barrelRay = new Ray (barrel.position, player.position);
		line = barrel.GetComponent<LineRenderer>();
		rayCastTimer += 0.0f;
		helo = GameObject.Find ("heloGazelle").transform;
		playerPos = player.position;
	}


	void Update ()
	{
		// If the enemy and the player have health left...
		//		if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
		//		{
		// ... set the destination of the nav mesh agent to the player.
		timer += Time.deltaTime;
		rayCastTimer += Time.deltaTime;

		transform.LookAt (player);


		//Lerp isn't working properly
//		Quaternion toRotation = Quaternion.FromToRotation(transform.position, playerPos);
//		transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 5 * Time.time);

		/* Lock x and z rotation to 0 so enem only rotates on y axis*/
		Vector3 newRotation = Quaternion.Slerp (transform.rotation,
		Quaternion.LookRotation (player.position - transform.position), rotationSpeed * Time.deltaTime).eulerAngles;
		newRotation.x = 0;
		newRotation.z = 0;
		transform.rotation = Quaternion.Euler (newRotation);

//		Debug.Log (transform.position.y);
		if (helo.transform.position.y <= player.position.y + 40) {
//			Debug.Log (gameObject.name + "\tMy pos: " + helo.transform.position.ToString () + "\tPlayer Pos: " + player.transform.position.ToString ());
//			Debug.Log ("TOO LOW");
//			Vector3 temp = transform.position;
//			transform.Translate (-Vector3.back * 5 * Time.deltaTime);
//			temp.y += 10;
//			Debug.Log (temp.y);
			//transform.Translate (temp);
			//transform.position = temp;
//			transform.Translate ((-Vector3.back + 20) * 5 * Time.deltasTime);
//			helo.transform.position = new Vector3(transform.position.x, player.position.y + 10, transform.position.z);
//			transform.position = new Vector3(transform.position.x, player.position.y + 10, transform.position.z);
//			Vector3 newPos = new Vector3(transform.position.x, player.position.y + 10, transform.position.z);
//			if (!isMoving) {
//				isMoving = true;
//				transform.Translate (Vector3.up * Time.deltaTime, Space.Self);
			helo.transform.Translate (Vector3.up * 7 * Time.deltaTime);
//
//			Invoke ("moveHelo", 5);
//			}
		} else {
			helo.transform.Translate (Vector3.down * 20 * Time.deltaTime);
		}

		currDist = Vector3.Distance (player.position, transform.position);
//		print (currDist);

		if (playerHealth.currentHealth >= 0) {
			if (timer >= timeBetweenAttacks && currDist <= minDist) {
				Attack ();
//			print("Chopper Attacking");
			} else {
				nav.height = player.transform.position.y;
				nav.SetDestination (player.position);
//			if(transform.position.y < playerYOffset){
//				transform.position.y += playerYOffset;
//			}
//			Vector3 vect = player.position  - transform.position;
//			vect = vect.normalized;
//			vect *= (currDist-minDist);
//			transform.position += vect;
			}
		}

		if (rayCastTimer >= rayCastLineDelta){
//			Debug.Log ("Chopper line disabled");
			line.enabled = false;
			rayCastTimer = 0f;
		}
		//		}
		// Otherwise...
		//		else
		//		{
		// ... disable the nav mesh agent.
		//			nav.enabled = false;
		//		}
	}

	void moveHelo () {
	
		isMoving = false;
//		helo.transform.position = new Vector3(transform.position.x, player.position.y + 10, transform.position.z);
//		Vector3 newPos = new Vector3(transform.position.x, player.position.y + 10, transform.position.z);
		helo.transform.Translate (-Vector3.down * 5 * Time.deltaTime);	
	}

	void Attack ()
	{

		timer = 0f;
		barrelRay = new Ray (barrel.transform.position, player.transform.position - barrel.transform.position);
		

		if (Physics.Raycast (barrelRay, out hit, 1200)) {
//			Debug.Log (barrel.transform.position + "\t" + player.transform.position);
//			Debug.DrawLine (barrel.transform.position, hit.transform.position, Color.green, 2f, false);
			line.SetPosition(0, barrel.position);
			line.SetPosition (1, hit.point);
			line.enabled = true;
			if (hit.collider.tag == "Player") {
//				print ("Chopper hit player");
				if (playerHealth.currentHealth >= 0) {
					playerHealth.TakeDamage (attackDamage);
				}
			}
		}
	}
}

