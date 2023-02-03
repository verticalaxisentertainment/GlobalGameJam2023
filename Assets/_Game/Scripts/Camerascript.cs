using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerascript : MonoBehaviour
{
    public GameObject player;
    public float horizontalSpeed = 1;
    public float verticalSpeed = 1;

    private float rotationHorizontal = 0;
    private float rotationVertical = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;

        rotationHorizontal += Input.GetAxis("Mouse X") * horizontalSpeed;
        rotationVertical -= Input.GetAxis("Mouse Y") * verticalSpeed;


        transform.eulerAngles = new Vector3(rotationVertical, rotationHorizontal, 0f);
        player.transform.eulerAngles = new Vector3(0f, rotationHorizontal, 0f);
    }
}
