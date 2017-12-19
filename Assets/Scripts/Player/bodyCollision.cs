using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyCollision : MonoBehaviour {

	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider)
	{
		//if(collider.gameObject.tag == "building") // this string is your newly created tag
		//{
			// TODO: anything you want
			// Even you can get Bullet object
		//	rb.AddForce(30,3,3);
		//	Debug.Log ("has collided");
	//	}
	}
}
