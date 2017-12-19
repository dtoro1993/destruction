using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public int scoreval;
    private Animator anim;
    //    public float sinkSpeed = 2.5f;
    //    public int scoreValue = 10;
    //    public AudioClip deathClip;
    //    AudioSource enemyAudio;
    //    ParticleSystem hitParticles;
    //    CapsuleCollider capsuleCollider;
    public bool isDead;
    bool isSinking;
	public float destroyDuration = 1f;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
//        enemyAudio = GetComponent <AudioSource> ();
//        hitParticles = GetComponentInChildren <ParticleSystem> ();
//        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }


   /* void Update ()
    {
        if(isSinking)
        {
	       //transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }*/


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
			if (gameObject.name == "heloEnemy") {
				//Rotate slowly and sink then destroy
//				transform.Rotate (10);
				transform.Translate (Vector3.down * 5 * Time.deltaTime);
				Destroy (gameObject, 3f);
			} else {
				Destroy (gameObject, 2f);
				ScoreManager.score += scoreval;
			}
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
		Destroy (gameObject, destroyDuration);
    }
}
