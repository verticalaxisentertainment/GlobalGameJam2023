using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float moveSpeed;
    public float jumpStrength;
    //public GameObject character;
    public Rigidbody characterRigidBody;
    private float speed;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        characterRigidBody = GetComponent<Rigidbody>();
     
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayerRelativeToCamera();
    }


    void MovePlayerRelativeToCamera()
    {
        //  Player Input;
        if(Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift)) 
        {
            speed = moveSpeed * 3;
        }
        else 
        {
            speed = moveSpeed;
        }
        float playerVerticalInput = Input.GetAxis("Vertical") * speed *Time.deltaTime;
        float playerHorizontalInput = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        //   Get camera relative directions

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        forward = forward.normalized;
        right = right.normalized;

        // Now combining all vectors woooo
        Vector3 forwardRelativeCamera = playerVerticalInput * forward;
        Vector3 rightRelativeCamera = playerHorizontalInput * right;


        //  Now with witcchcraft i am going to translate it into worldspace
        
        Vector3 cameraRelativeMovement = forwardRelativeCamera + rightRelativeCamera;
        
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            characterRigidBody.AddForce(Vector3.up * jumpStrength,ForceMode.Impulse);
            isGrounded = false;
        }
        transform.Translate(cameraRelativeMovement, Space.World);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Ground") 
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
