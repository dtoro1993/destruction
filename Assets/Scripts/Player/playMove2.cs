using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class playMove2 : MonoBehaviour
{

    public float moveSpeed;
    public float turnSpeed;
    //public Transform FixedCam;
    public float climbAngle;
    public float climbSpeed;
    public bool isGrounded;
    public float jumpForce;
    private bool canJump;
    private bool canClimb = false;
    Animator anim;
    Rigidbody rib;


    void Start()
    {
        rib = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        jump();
        if (canClimb)
        {
            climb();

        }
        else
        {
            //WASD AND arrowkey movement
            //transform.Translate (moveSpeed * Input.GetAxis ("Horizontal") * Time.deltaTime, 0f, moveSpeed * Input.GetAxis ("Vertical") * Time.deltaTime);

            //this will use the keyboard to rotate instead of camera
            var z = Input.GetAxis("Vertical") * moveSpeed;
            var y = Input.GetAxis("Horizontal") * turnSpeed;
            transform.Translate(0, 0, z);
            transform.Rotate(0, y, 0);
            //****************************************************/
            //rotateWithCam();
        }
    }

    /*void rotateWithCam()
    {
        //transform.rotation = Quaternion.LookRotation(transform.position - FixedCam.position);  //faces player orientation same of fixed cam
        Vector3 eulerAng = transform.rotation.eulerAngles;
        eulerAng.x = 0;
        //dont need to set y angle already done in lookrotation
        eulerAng.z = 0;  //setting x and z to 0 so we only account for y rotation of fixedcam

        transform.rotation = Quaternion.Euler(eulerAng);  //rotate based on altered Euler ang
    } //player rotates with camera. got it working -tim
    */

   void jump()
    {
        if (canJump && Input.GetKeyDown("space"))
        {
            rib.useGravity = true;
            //rib.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            rib.AddForce(0, jumpForce, 0);
            canJump = false;
            canClimb = false;
            isGrounded = true;


        }
    }  //adds vertical positive force to player. 

    void climb()
    {
        //Debug.Log("climbing");
        if (Input.GetAxis("Vertical") > .1)
        {
            //Debug.Log("Climbing up");

            rib.useGravity = false;
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * climbSpeed);

        }
        if (Input.GetAxis("Vertical") < -.1)
        {

            rib.useGravity = false;
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * climbSpeed);
        }
    }
    void OnCollisionEnter()
    {
        canJump = true; //can only reset jump on enter. exiting will immediately set it false
        if (canClimb && Input.GetAxis("Vertical") < -.1) canClimb = false;  //allows dismounting from climbing when reaching ground
    }
    void OnTriggerEnter(Collider colis)
    {

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
    }
    void OnTriggerExit()
    {
        canClimb = false;
        rib.useGravity = true;
    }
    void OnTriggerStay(Collider colis)
    {
        if (colis.gameObject.tag == "Climb")
        {

        }
    }


}