using UnityEngine;
using System.Collections;

public class fixrotation : MonoBehaviour {

	public float turnSpeed = 4.0f;
	public float verticalOffset;
	public float zOffset;
	public Transform player;

	private Vector3 offset;

	void Start () {
		offset = new Vector3(player.rotation.x, player.position.y + verticalOffset, player.position.z + zOffset);
		//Cursor.lockState = CursorLockMode.Locked;  //for debugging need new class
		//Screen.lockCursor = true;

		//need to align offset better for starting position of camera
	}

	void cursorUnlock() {
		/*if (Input.GetKeyDown (KeyCode.Escape)) {
			if (Cursor.lockState == CursorLockMode.Locked) {
				Cursor.lockState = CursorLockMode.None;
			} else {
				Cursor.lockState = CursorLockMode.Locked; 
			}
			Screen.lockCursor = !Screen.lockCursor;
		}*/
		Cursor.lockState = CursorLockMode.None;
	}   /*Don't want to press ESC every time when trying 
	      to start the game. Arrow should display 
		  automatically and then disappear on gameplay...
		  not sure how to do that yet -- david */
	void Update(){
		 
		//transparentRay();
	}
	void transparentRay(){
		RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, (player.transform.position-transform.position)); //could add hitmask
		 for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
			Color col =hit.collider.gameObject.GetComponent<MeshRenderer>().material.color;
			col.a = 100;
			hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = col;
		}
	}  //probably impossible to implement this correctly haha
	void LateUpdate()
	{
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = -Input.GetAxis("Mouse Y");


	
		offset = Quaternion.AngleAxis (mouseX * turnSpeed, Vector3.up) * offset; //offset of where object is relevant to player
	

		transform.position = player.position + offset;   //moves rotating object orbiting the player plus the offset

		transform.LookAt(player.position);
		//cursorUnlock ();

	} //Script allows fixed about to rotate x axis around player. Camera follows it. This is not for camera object itself
}