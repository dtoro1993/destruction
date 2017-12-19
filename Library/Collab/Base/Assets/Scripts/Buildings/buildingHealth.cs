using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buildingHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;
	public int scoreval = 80;
	//    public float sinkSpeed = 2.5f;
	//    public int scoreValue = 10;
	//    public AudioClip deathClip;
	//    Animator anim;
	//    AudioSource enemyAudio;
	//    ParticleSystem hitParticles;
	//    CapsuleCollider capsuleCollider;
	bool isDead;
	bool isSinking;
	float sinkSpeed = 25f;
	Rigidbody rigBody;
    //public Transform prefab;
    public float restartDelay = 1f;
    float restartTimer;
	float sinkTimer;
	Vector3 buildPosition;



	void Awake ()
	{
		//        anim = GetComponent <Animator> ();
		//        enemyAudio = GetComponent <AudioSource> ();
		//        hitParticles = GetComponentInChildren <ParticleSystem> ();
		//        capsuleCollider = GetComponent <CapsuleCollider> ();

		rigBody = GetComponent<Rigidbody> ();
		currentHealth = startingHealth;

       

		buildPosition = transform.position;
		
    }

    void Start()
    {
		
		buildPosition = transform.position;
		currentHealth = startingHealth;
		rigBody = GetComponent<Rigidbody> ();
		
        
      
    }

	 void FixedUpdate ()
    {
        if(isSinking)
        {
			//rigBody.isKinematic = false;

			transform.Translate (-Vector3.forward * sinkSpeed * Time.deltaTime);
			
        }

        if (currentHealth <= 0)
        {
			Death();
            ReEnable();
            
        }
    }


	public void TakeDamage (int amount)
	{
		Debug.Log("Taking " + amount + " Damage");
		//if(isDead)
		//  return;

		//        enemyAudio.Play ();

		currentHealth -= amount;
       
		//        hitParticles.transform.position = hitPoint;
		//        hitParticles.Play();

		if(currentHealth <= 0)
		{
			Death ();
        }
	}

	void Death ()
	{
		if(isDead != true){
			//rigBody.isKinematic = false;
			isSinking = true;
			//Destroy (gameObject, 4f);
			ScoreManager.score += scoreval;
            StartSinking();
            Debug.Log("dead!");
			
            
        }
        
		isDead = true;
        //        capsuleCollider.isTrigger = true;

        //        anim.SetTrigger ("Dead");

        //        enemyAudio.clip = deathClip;
        //        enemyAudio.Play ();
    }


	public void StartSinking ()
	{
		//        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
		//        GetComponent <Rigidbody> ().isKinematic = true;
		//        isSinking = true;
		//        //ScoreManager.score += scoreValue;
		//Destroy (gameObject, 2f);
		sinkTimer +=Time.deltaTime;
		if (sinkTimer >= 2) {
        	gameObject.SetActive(false);
			//isSinking = false;

		}
	}

    public void ReEnable()
    {
        restartTimer += Time.deltaTime;

        if(restartTimer >= restartDelay)
        {
            Debug.Log("restart delay!");
			transform.position = buildPosition;
            currentHealth = startingHealth;
			isSinking = false;
			isDead = false;
            gameObject.SetActive(true);
			restartTimer = 3;
			
        }
    }
}
