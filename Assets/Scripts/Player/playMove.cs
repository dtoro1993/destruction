using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class playMove : MonoBehaviour {

	public float moveSpeed;
	//public float turnSpeed; //should be based on camera sens?
	public Transform FixedCam;
	public float climbAngle;
	public float climbSpeed;

	//private float camY;
	public float jumpForce;
    public float playerRotateSpeed;
	private bool canJump;
	private bool canClimb=false;
	Animator anim;
	Rigidbody rib;


	void Start()
	{
		rib = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
	}

	void Update()
	{ /*
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("walkingfow", true);
        }
        else
        {
            anim.SetBool("walkingfow", false);
            anim.SetBool("idle", true);
        }

        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("walkingback", true);
        }
        else
        {
            anim.SetBool("walkingfow", false);
            anim.SetBool("idle", true);
        } */
        //commented out anim controller code for gorilla. feel free to readd


        Jump ();
		if (canClimb) {
            anim.SetBool("Climbing",true);
			Climb ();

		} else {
            anim.SetBool("Climbing",false);
			transform.Translate (moveSpeed * Input.GetAxis ("Horizontal") * Time.deltaTime, 0f, moveSpeed * Input.GetAxis ("Vertical") * Time.deltaTime);  //WASD AND arrowkey movement
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");
            MoveAnimBehavior(v, h);

            //Animator anim = GetComponent<Animator>();
            //anim.Play("Walking");
            //dependent on animation controller
            RotateWithCam ();
		}
	}
    void MoveAnimBehavior(float v, float h)
    {
        if (v > h)
        {

            if (v > 0.1 || v < -0.1) anim.SetFloat("Speed", v);
            if (v == 0) anim.SetFloat("Speed", -.2f);

        }
        if (h > v)
        {
            if (h < 0.1) anim.SetFloat("Speed", h);

            if (h == 0) anim.SetFloat("Speed", -.2f);
            if (h >= 0.1 && v < 0.1) anim.SetFloat("Speed", -0.2f);  //this is the bingo line. without it it breaks.
        }
        if (v < -0 && h < -0.1)
        {
            anim.SetFloat("Speed", -0.2f);
        }
        if (v < -0.1 && h > 0)
        {
            anim.SetFloat("Speed", -0.2f);
        }
        if (v == h)
        {
            if (v > 0.1)
            {
                anim.SetFloat("Speed", v);
            }
            if (v < 0.1)
            {
                anim.SetFloat("Speed", v);
            }
        }//this is the code to determine movement animations
    }

	void RotateWithCam () {


		
        Quaternion camRot = Quaternion.LookRotation(transform.position - FixedCam.position);  //faces player orientation same of fixed cam
        camRot.x = 0;
        camRot.z = 0;
        //transform.rotation = camRot;
	
 

    //Vector3 newRot = camRot.eulerAngles;

    //Vector3 currentRot =transform.eulerAngles;
    Quaternion oldRot = transform.rotation;
    

   //transform.eulerAngles = currentRot;
    //transform.eulerAngles = Vector3.Lerp(currentRot, newRot, Time.deltaTime * 2);
    //keep these old comments for reference
    transform.rotation = Quaternion.Lerp(oldRot,camRot,Time.deltaTime* playerRotateSpeed);
       
    
    
}


	 //player rotates with camera. got it working -tim

	void Jump() {
		if (canJump && Input.GetKeyDown("space")) {
            anim.SetTrigger("Jump");
				rib.useGravity = true;
				rib.AddForce(0,jumpForce,0,ForceMode.Impulse);
				canJump = false;
				canClimb = false;
		    	

		}
	}  //adds vertical positive force to player. 

	void Climb(){
		//Debug.Log("climbing");
		if(Input.GetAxis("Vertical") > .1){
             //Debug.Log("Climbing up");
			 
			 rib.useGravity = false;
             transform.Translate (new Vector3(0,1,0) * Time.deltaTime*climbSpeed);
     
     }
         if(Input.GetAxis("Vertical") < -.1){
			 
             rib.useGravity = false;
             transform.Translate (new Vector3(0,-1,0) * Time.deltaTime*climbSpeed);		 
		}
	}
	void OnCollisionEnter()
	{
        anim.ResetTrigger("Jump");

        canJump = true; //can only reset jump on enter. exiting will immediately set it false
		if(canClimb && Input.GetAxis("Vertical") < -.1) canClimb=false;  //allows dismounting from climbing when reaching ground
	}
	void OnTriggerEnter (Collider colis) {
		
		//insert code when needed
        //so far climing only needs triggerstay
	}
	void OnTriggerExit(){
		canClimb=false;
		rib.useGravity = true;
	}
	void OnTriggerStay(Collider colis){
        if (colis.gameObject.tag == "Climb" && Input.GetAxis("Vertical") > .1)
        {



            Vector3 colisPos = colis.transform.position;

            colisPos.y = transform.position.y;
            colisPos.x = transform.position.x;
            //we dont want the position of center of the object. only its z
            float anglePos = Vector3.Angle(transform.forward, colisPos - transform.position);
            //angle between player and wall

            //Debug.Log(colis.transform.eulerAngles.y);
            if (colis.transform.eulerAngles.y > 0)
            {
                anglePos = anglePos - colis.transform.eulerAngles.y % 180;
            } //modulus 180. since eulerAngles are nonnegative values
              //if the object has a built in rotation in gameworld. ACCOUNT for it here

            /*magic to account for rotation of climbing wall. will now work with wall rotated at any angle
			 *Debug.Log(anglePos);
			 */

            if ((anglePos > -climbAngle && anglePos < climbAngle))
            {

                canClimb = true;
                canJump = true;
                rib.velocity = new Vector3(0, 0, 0);
            } //if within set perpendicular angle, then we allow climbing

        }//angle math to determine whether player is 'aligned' with wall before allowing climbing 
    }  /*duplicate code with OnTriggerEnter. will combine in future- tim*/

	
}