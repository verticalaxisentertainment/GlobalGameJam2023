using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public static MoveScript Instance;


    public float moveSpeed;
    public float jumpStrength;
    //public GameObject character;
    public Rigidbody characterRigidBody;
    private float speed;
    public bool isGrounded;

    private bool canTakeSword=false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        characterRigidBody = GetComponent<Rigidbody>();
     
    }

    void Update()
    {
        MovePlayerRelativeToCamera();
    }


    void MovePlayerRelativeToCamera()
    {
        if(Input.GetKeyDown(KeyCode.E)&&canTakeSword) 
        {
            LevelManager.Instance.collectableSword.SetActive(false);
            DialogueManager.Instance.dialogueCanvas.SetActive(false);
            LevelManager.Instance.sword.SetActive(true);
            LevelManager.Instance.audioSource.enabled=false;
            GetComponent<AudioSource>().Play();
        }

        //  Player Input;
        if(Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift)) 
        {
            speed = moveSpeed * 3;
        }
        else 
        {
            speed = moveSpeed;
        }
        float playerVerticalInput=0;
        float playerHorizontalInput =0;

        if(LevelManager.Instance.move)
        {
            playerVerticalInput = Input.GetAxis("Vertical") * speed *Time.deltaTime;
            playerHorizontalInput = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        }

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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.other.gameObject.CompareTag("Respawn"))
        {
            canTakeSword= true;
            DialogueManager.Instance.skipText.enabled=true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.other.gameObject.CompareTag("Respawn"))
        {
            canTakeSword= false;
            DialogueManager.Instance.skipText.enabled=false;
        }
        isGrounded = false;
    }
}
