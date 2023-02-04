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


    private void Awake()
    {
        instance=this;
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation=player.transform.rotation;
        
        //animator=player.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(i==3)i=0;
            i++;
            animator.SetInteger("State",i);
            animator.SetBool("Click",true);
        }
        else
        {
            animator.SetBool("Click",false);
        }


        rotationHorizontal += Input.GetAxis("Mouse X") * horizontalSpeed;
        rotationVertical -= Input.GetAxis("Mouse Y") * verticalSpeed;

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z);

        transform.eulerAngles = new Vector3(rotationVertical, rotationHorizontal, 0f);
        player.transform.eulerAngles = new Vector3(0, rotationHorizontal, 0);
    }
}
