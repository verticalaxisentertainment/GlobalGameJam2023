using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerascript : MonoBehaviour
{
    public static Camerascript instance;

    public GameObject player;
    public Animator animator;
    public float horizontalSpeed = 1;
    public float verticalSpeed = 1;

    private float rotationHorizontal = 0;
    private float rotationVertical = 0;


    int i=0;


    void Start()
    {
        transform.rotation=player.transform.rotation;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(animator.gameObject.CompareTag("Boxer"))
            {
                animator.SetBool("Punch",true);
            }
            else
            {
                if(i==3)i=0;
                i++;
                animator.SetInteger("State",i);
                animator.SetBool("Click",true);
            }
        }
        else
        {
            animator.SetBool("Punch",false);
            animator.SetBool("Click",false);
        }


        rotationHorizontal += Input.GetAxis("Mouse X") * horizontalSpeed;
        rotationVertical -= Input.GetAxis("Mouse Y") * verticalSpeed;

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z);

        transform.eulerAngles = new Vector3(rotationVertical, rotationHorizontal, 0f);
        player.transform.eulerAngles = new Vector3(0, rotationHorizontal, 0);
    }
}
