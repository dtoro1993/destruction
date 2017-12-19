using UnityEngine;

public class enemyManager : MonoBehaviour
{
	Transform player;
	public PlayerHealth playerHealth;
	public GameObject enemy;
	public float spawnTime = 10f;
	public Transform[] spawnPoints;


	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
	}


	void Spawn ()
	{
				if (playerHealth.currentHealth <= 0f) {
					return;
				} else {
			
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		Instantiate (enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
				}
	}
}
