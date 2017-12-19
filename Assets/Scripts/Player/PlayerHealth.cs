using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
	GameObject GameOver;
	private Texture2D myGUITexture;
	public Text healthText;
	public float startingHealth; //max health
	public float currentHealth;
	//GameObject GameOver;
	playMove playerMovement;
	bool isDead;
	bool damaged;
    bool pickedUp;
	private Animator anim;
    healthPotion health;
    public Slider healthSlider;
	private Image targetbar;
    
    public Image damageimage;
//    public AudioClip deathClip;
    public float flashSpeed = 10f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
//    Animator anim;
//    AudioSource playerAudio;

    void Awake ()
    {
		
//        anim = GetComponent <Animator> ();
//        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <playMove> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        anim = GetComponent<Animator>();
        anim.SetBool("Dead", false);

		currentHealth = startingHealth;

		if (healthSlider.fillRect != null){
			targetbar = healthSlider.fillRect.GetComponent<Image> ();
		}
    }

    void Update ()
    {
		healthText.text = currentHealth.ToString ("f0") + " %";

		if (currentHealth == 0) {
			playerMovement.enabled = false;
		}

        if(damaged)
        {
            damageimage.color = flashColour;
        }
        else
        {
            damageimage.color = Color.Lerp (damageimage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage (float amount)
    {
        anim.SetTrigger("Damaged");
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
		HealthBarColor ();
//        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
        
    }

	public void HealthBarColor(){
		float currenthealthbar = (currentHealth*100)/startingHealth;

		if (currenthealthbar >= 66) {
			targetbar.color = Color.green;
		} else if (currenthealthbar <= 65 && currenthealthbar >= 33) {
			targetbar.color = Color.yellow;
		} else if (currenthealthbar <= 32 && currenthealthbar >= 0) {
			targetbar.color = Color.red;
		}
	}

    public void Death ()
    {
        isDead = true;
        anim.SetBool("Dead", true);

        //playerShooting.DisableEffects ();

//        anim.SetTrigger ("Die");

//        playerAudio.clip = deathClip;
//        playerAudio.Play ();

        playerMovement.enabled = false;
        //playerShooting.enabled = false;
    }
}