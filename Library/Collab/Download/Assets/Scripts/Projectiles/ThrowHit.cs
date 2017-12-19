using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHit : MonoBehaviour {
	EnemyHealth enemHealth;
	buildingHealth buildHealth;
	houseHealth houseHealth;
	public float buildHitMultiplier;
	public float enemyHitMultiplier;
	private float hitTimer;
	public float timer;
	private bool hit2 = false;
	

	public bool canHit = false;
	
	public Rigidbody rb;
	

	// Use this for initialization
	public void hitSet(){
		canHit = true;
		hitTimer = timer;
	}
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (canHit){
			
			if (timer >= hitTimer + 1.5f){
				canHit = false;

			}
			
		}
	}
	void OnCollisionEnter(Collision col) {
		
		transform.parent = null;	
		float hitVal = rb.velocity.magnitude;
		//canHit = hitVal > 7;
		//Debug.Log(hitVal);
		/*without setting parent to null, object would deform on collision due to
		 * the road scale being not set to 1,1,1. This nee */
		//col.gameObject.GetComponent<enemyMovement>().canHit = false;
		//use this tim then set timer. dont do it on this object
		if (canHit && transform.tag !="Enemy"){
			
			//Debug.Log(rb.velocity.magnitude);
			if (col.gameObject.tag =="Enemy"){
				
				//if (hitVal >3f) {
					enemHealth = col.gameObject.GetComponent<EnemyHealth>();
					col.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
					col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
					col.gameObject.GetComponent<enemyMovement>().throwing();
					col.gameObject.GetComponent<Rigidbody>().AddForce (transform.forward * 320 + transform.up * 40 );
					int damage = (int)(enemyHitMultiplier*40);
					enemHealth.TakeDamage(damage);
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
					int damage = (int)(buildHitMultiplier*30);
					buildHealth.TakeDamage(damage);
					ScoreManager.score += 12;
					canHit = false;
					
			//	}
			}
			if (col.gameObject.tag =="House"){
			//	if (hitVal >1) {
					houseHealth = col.gameObject.GetComponent<houseHealth>();
					int damage = (int)(buildHitMultiplier*30);
					houseHealth.TakeDamage(damage);
					ScoreManager.score += 10;
					canHit = false;
					
			//	}
			}
		
			

			}
			
			
			//GetComponent<Rigidbody>().isKinematic = false;
	
		}
		void OnCollisionStay(Collision col) {
			if (col.gameObject.tag == "Player") {
				canHit = false;
			}
			
				//canHit = false;
			
		}
		
	
	
	//add new class functions here lol



}