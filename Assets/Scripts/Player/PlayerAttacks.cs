using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttacks : MonoBehaviour {

	//PlayerHealth playerHealth;
	//Enemyhealth enemHealth
	public Transform enemy;
	public Vector3 endPos;
	public float hitDistance;
	public float radius;
	public float expandSpeed;
	private float length;
	private float maxLength;
	private Rigidbody rb;
	public float strength;
	public bool holding = false;
	public GameObject carry;
	EnemyHealth enemHealth;
	buildingHealth buildingHealth;
	private Vector3 pickUpAngles;
	public LayerMask HitMask = new LayerMask();
	public LayerMask PickUpMask = new LayerMask();
	Animator anim;
	float timer;
	float attackTimer;
	float slideTimer;
	houseHealth houseHealth;
	private bool canPunch = true;
	private float punchDelay = 0.7f;
	private float slideDelay = 1.5f;
	public bool canSlide=true;
    public AudioClip EnemyThrow1;
    public AudioClip EnemyThrow2;
    public AudioClip EnemyThrow3;
    public AudioClip EnemyThrow4;
    //public AudioClip EnemyThrow5;



    void Start() {
		length = 20;
		maxLength = hitDistance;
		anim = GetComponentInParent<Animator>();
		rb = GetComponentInParent<Rigidbody>();
    }


	

	// Use this for initialization
	//void Start () {
	//playerHealth 
	//enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
	/*can add reference to health when created*/


	//}

	void FixedUpdate(){
		if (!canPunch){
			if (timer >= (attackTimer+ punchDelay) ) {
				canPunch = true;
				
			}
		}
		if (!canSlide){
		if (timer >= (slideTimer+ slideDelay) ) {
			canSlide = true;

		}
	}
		
		
		if (!holding){
			anim.SetBool("Holding",false);
			if (canPunch){
				Punch();
				
			}
			if (canSlide){
				slide();
			}
		
	}
}
	void Update () {
		
		
		timer += Time.deltaTime;
		PickUp();
		if (holding) {
			if (carry == null) {
				holding = false;
				return;
			}
			if (carry.tag == "Enemy") {
				carry.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
				//carry.GetComponent<enemyMovement>().throwing();
			}
			anim.SetBool("Holding",true);
			if (carry.tag == "Crate"){
				carry.transform.position = transform.position+transform.forward*7 + transform.up*15;
				carry.transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
			}
			else {
			carry.transform.position = transform.position+transform.forward*5 + transform.up*11;
			carry.transform.eulerAngles = new Vector3(0,transform.eulerAngles.y+90,0); //lock its previous angles while nonkinematic
			//orient held object in front of player constantly
			}
		}
		
		
    }
	void PickUp(){
		if (Input.GetKeyDown ("e")) {
			if (!holding){
				carry = LiftScan();
				//will return pickup object. else returns null
				if (carry) {
					holding = true;
					if (carry.gameObject.tag =="Enemy") {
					carry.gameObject.GetComponent<enemyMovement>().held = true;
				}
				}
				//if returned an object, holding bool is true
				else {
					holding = false;
					
				}
			} //if not currently holding, check for object to pickup

			//else if we are carrying an object. THROW IT 
			else if (holding) {

				//if holding and we click..we throw
				
				if (carry.tag == "Enemy") {
					//carry.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
					carry.GetComponent<enemyMovement>().throwing();
                    /*after enemy is thrown, set a timer. reset their velocity, 
					then reset nav mesh */
                    SoundManager.instance.RandomizeSfx(EnemyThrow1, EnemyThrow2, EnemyThrow3, EnemyThrow4);
                } //if throwing an enemy we need to turn off their nav mesh or else weird behavior
				
				ScoreManager.score += 10;
				//no more parent. throw the object
				carry.transform.parent = null;
				carry.GetComponent<Rigidbody>().isKinematic = true;
				carry.GetComponent<Rigidbody>().isKinematic = false;
				/*need to set this twice or else the force glitches and f***s it all up -TIM */
				carry.GetComponent<Rigidbody>().AddForce(transform.forward * strength*4);
				carry.GetComponent<ThrowHit>().hitSet();
				//only set it to true after thrown. not in collision script
				
				Debug.Log("throwing object");
                //carry = null; //object doesnt exist. cant drop throw it twice etc..

                //SoundManager.instance.RandomizeSfx(EnemyThrow1, EnemyThrow2, EnemyThrow3, EnemyThrow4, EnemyThrow5);

				
				holding = false;
				carry = null;
				




			}
			else  {
				carry.transform.parent = null;
				Debug.Log("ELSE CONDITION??"); //never happens
			}
		}
	}

	void slide(){
		if (Input.GetMouseButtonDown (1)){
			
			slideTimer = timer;
			canSlide = false;
			anim.SetTrigger("Slide");
			if (rb.velocity.magnitude >= 45) {
				return;	
			}  //limit max speed
			rb.AddForce(transform.forward*400,ForceMode.Impulse);

		
			}
	}
	GameObject LiftScan(){

		Ray ray = new Ray(transform.position,transform.forward);

		RaycastHit hit;  



		if (Physics.SphereCast (ray, radius, out hit, length,PickUpMask)) {

			Lift (hit.collider.gameObject); //set found object as child
			//endPos.z = transform.position.z;

			if (hit.collider.gameObject.tag == "Enemy" || hit.collider.gameObject.tag == "Throw" || hit.collider.gameObject.tag == "Crate" ) {
				if (hit.collider.gameObject.tag == "Enemy") {
					if (hit.collider.gameObject.GetComponent<EnemyHealth>().isDead){
						return null;  //check to not pickup an object that will be destroyed
					}
				}
				return hit.collider.gameObject;
			}


		}

		return null;
	} //if found a pickupable object, return it. else return null

	void Lift(GameObject hit){
		hit.GetComponent<Rigidbody>().isKinematic = false;
		//if kinematic is true, colliders will push player
		hit.transform.SetParent(transform,true);
		//can adjust local position of object picking up
		//pickUpAngles = hit.transform.eulerAngles;  //dont really need


	}

	void Punch() {  
		if (Input.GetMouseButtonDown (0)) { //left click
		attackTimer = timer;
		canPunch = false;
		anim.SetTrigger("Attack");


			length = length + Time.deltaTime * expandSpeed;
			if (length > maxLength)
				length = maxLength;

			/*ray object for origin of spherecast. start at object pos*/
			Ray ray = new Ray(transform.position,transform.forward);

			RaycastHit hit;  



			if (Physics.SphereCast (ray, radius, out hit, length,HitMask)) {

				HitObject (hit.collider.gameObject);
				//endPos.z = transform.position.z;

				Debug.DrawRay (transform.position, transform.forward*hitDistance, Color.green);


			}  //shoots sphere hit detector forward from object
		}
	}
	void HitObject(GameObject hit){
		Debug.Log ("I hit a '" + hit.name + "'!");
		if (hit.tag == "Throw") {
			hit.GetComponent<Rigidbody> ().isKinematic = false;
			hit.GetComponent<Rigidbody> ().AddForce (transform.forward * strength*2 );
			ScoreManager.score += 10;

		}
		if (hit.tag == "Enemy") {
			//hit.GetComponent<Rigidbody> ().isKinematic = false;

			//hit.GetComponent<Rigidbody> ().AddForce (transform.forward * strength);
			enemHealth = hit.GetComponent<EnemyHealth> ();
			hit.GetComponent<Rigidbody> ().isKinematic = false;
			hit.GetComponent<enemyMovement>().punching();
			hit.GetComponent<Rigidbody> ().AddForce (transform.forward * strength*2 + transform.up*200 );
			enemHealth.TakeDamage (40);
			//take damage is delayed in throwing script. no time to fix.
            ScoreManager.score += 20;
		}
		if (hit.tag == "Building") {
			//hit.GetComponent<Rigidbody> ().isKinematic = false;

			//hit.GetComponent<Rigidbody> ().AddForce (transform.forward * strength);
			buildingHealth = hit.GetComponent<buildingHealth> ();
			buildingHealth.TakeDamage (60);
			ScoreManager.score += 5;

		}
		if (hit.tag == "House") {
			//hit.GetComponent<Rigidbody> ().isKinematic = false;

			//hit.GetComponent<Rigidbody> ().AddForce (transform.forward * strength);
			houseHealth = hit.GetComponent<houseHealth> ();
			houseHealth.TakeDamage (60);
			ScoreManager.score += 5;

		}

	}
	void OnTriggerEnter(Collider col) {
		if (!canSlide){ //cant slide MEANS we are sliding
			if (col.gameObject.tag =="Enemy"){
				if (col.gameObject.GetComponent<enemyMovement>().canSlideHit) {

					Debug.Log("Im colliding while sliding");
					col.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
					col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
					col.gameObject.GetComponent<enemyMovement>().punching();
					enemHealth = col.gameObject.GetComponent<EnemyHealth>();
					
					
					col.gameObject.GetComponent<Rigidbody>().AddForce (transform.forward * 5000 + transform.up * 200 ); //BOWLING PINS
					//int damage = (int)(enemyHitMultiplier*40);
					enemHealth.TakeDamage(30);
					ScoreManager.score += 30;
				}
			}
		}
	}

}
