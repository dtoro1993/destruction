using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tailRotor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//Rotate around self to animate
		transform.Rotate(0, 0, 10, Space.Self);
	}
}
