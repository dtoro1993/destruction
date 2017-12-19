using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class houseHealth : MonoBehaviour
{
	public int startingHealth = 160;
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


	void Awake ()
	{
		//        anim = GetComponent <Animator> ();
		//        enemyAudio = GetComponent <AudioSource> ();
		//        hitParticles = GetComponentInChildren <ParticleSystem> ();
		//        capsuleCollider = GetComponent <CapsuleCollider> ();

		rigBody = GetComponent<Rigidbody> ();
		currentHealth = startingHealth;
	}


	void Update ()
	{



	}

	void FixedUpdate ()
	{
		if(isSinking)
		{
			//rigBody.isKinematic = false;
			transform.Translate (Vector3.down * sinkSpeed * Time.deltaTime);
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
			Destroy (gameObject, 4f);
			ScoreManager.score += scoreval;
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
		Destroy (gameObject, 2f);
	}

}
