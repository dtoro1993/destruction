using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;


public class enemyMovement : MonoBehaviour
{
	Rigidbody rb;
	Transform player;               // Reference to the player's position.
	PlayerHealth playerHealth;      // Reference to the player's health.
	EnemyHealth enemyHealth;        // Reference to this enemy's health.
	EnemyHealth otherenemyHealth;  //reference to enemyhealth that collides with this object
	UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
	float minDist = 45;
	float currDist;
	float attackDamage = 5f;
	float timer;
	public float timeBetweenAttacks = 3f;
	float rotationSpeed;
	Animator anim;
	public bool thrown;
	private float throwTimer = 2f;
	public int throwDamage = 40;
	bool canGetHurt;
	buildingHealth buildHealth;
	houseHealth houseHealth;
	public bool held;
	public bool canSlideHit=true;

    /*private AudioSource a_source;
    public AudioClip[] sounds;*/

	

	void Awake ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		thrown = false;
		timer = 0;
		held = false;
		
	}
	public void throwing(){
		throwTimer = timer;
		thrown = true;
		canGetHurt = true;
	}
	public void punching(){
		
		throwTimer = timer;
		thrown = true;
		canSlideHit = false;
		
		
	}


	void Update ()
	{
		// If the enemy and the player have health left...
		//		if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
		//		{
		// ... set the destination of the nav mesh agent to the player.

		timer += Time.deltaTime;

		
		if (thrown && (timer >= throwTimer + 1.7)) {
			//enemyHealth.TakeDamage(throwDamage);
				if (rb.velocity.magnitude <= 25){

					GetComponent<Rigidbody>().isKinematic = true;
			    	rb.velocity = new Vector3(0,0,0);
					nav.enabled = true;
					thrown = false;
				}

				
		}  //logic for throwing enemies

		if (!canSlideHit) {
			if (timer >=throwTimer + 1f) {
				canSlideHit=true;
			}
		} // probly shouldnt be using throw timer for this. but it shouldnt matter
if (!thrown && !held ){
		transform.LookAt(player);
		/* Lock x and z rotation to 0 so enem only rotates on y axis*/
		Vector3 newRotation = Quaternion.Slerp(transform.rotation,
 		Quaternion.LookRotation(player.position - transform.position), rotationSpeed*Time.deltaTime).eulerAngles;
  		newRotation.x = 0;
   		newRotation.z = 0;
   		transform.rotation = Quaternion.Euler(newRotation);
		
		currDist = Vector3.Distance (player.position, transform.position);

		//		Debug.Log ("currDist is: %d", currDist);

		if (timer >= timeBetweenAttacks && currDist <= minDist) {
			Attack ();
			
			anim.SetTrigger("Attack");
		}
		else if (timer < timeBetweenAttacks && currDist <= minDist) {
			anim.SetBool("Running",false);
			//not attacking but close enough to player. set to idle
		}

		 else if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0){
			nav.SetDestination (player.position);
			anim.SetBool("Running",true);
		}
		
			
		}
	}

		//		}
		// Otherwise...
//else
//{
	//		 ... disable the nav mesh agent.
//	nav.enabled = false;
//}
	

	void Attack ()
	{
		timer = 0f;


	if(playerHealth.currentHealth > 0)
		{
			playerHealth.TakeDamage (attackDamage);
		}
	}
	void OnCollisionEnter(Collision col){
		//id like to move this into its own dedicated script eventually. not part of movement directly
		if (thrown){
		//held = false;
		//can leave this commented out since they die on impact anyway. not good practice	
			if (col.gameObject.tag!="Player"){
			if (canGetHurt) {
			enemyHealth.TakeDamage(throwDamage);
			Debug.Log("Taking throw damage");
			throwTimer = timer;
			canGetHurt = false;
			}
			if (col.gameObject.tag =="Enemy"){
				
				//if (hitVal >3f) {
					otherenemyHealth = col.gameObject.GetComponent<EnemyHealth>();
					col.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
					col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
					col.gameObject.GetComponent<enemyMovement>().throwing();
					col.gameObject.GetComponent<Rigidbody>().AddForce (transform.forward * 320 + transform.up * 40 );
					
					otherenemyHealth.TakeDamage(40);
					ScoreManager.score += 20;
                    //}
				}
			if (col.gameObject.tag == "Throw") {
				col.gameObject.GetComponent<ThrowHit>().canHit = true;
				//allows hit object to hit other objects
			
			}
			if (col.gameObject.tag =="Building"){
			//	if (hitVal >1) {
					buildHealth = col.gameObject.GetComponent<buildingHealth>();
					
					buildHealth.TakeDamage(60);
					ScoreManager.score += 12;
					//canHit = false;
					
			//	}
			}
			if (col.gameObject.tag =="House"){
			//	if (hitVal >1) {
					houseHealth = col.gameObject.GetComponent<houseHealth>();
					
					houseHealth.TakeDamage(60);
					ScoreManager.score += 10;
					//canHit = false;
					
			//	}
			}
		}
			
		} //can get hurt makes sure enemy only hurts itself once when thrown
	} //script where thrown enemies take damage when hitting object
}