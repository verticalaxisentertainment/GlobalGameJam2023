using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SwordScript : MonoBehaviour
{
    public int swordStrength;
    // Start is called before the first frame update
    void Start()
    {
        if (this.CompareTag("Hand"))
        {
            swordStrength = 5;
        }
        else if (this.CompareTag("Flower"))
        {
            swordStrength = 1;
        }
        else if (this.CompareTag("Club"))
        {
            swordStrength = 10;
        }
        else if (this.CompareTag("Sword"))
        {
            swordStrength = 20;
        }
        else
        {
            swordStrength = 10000000;
            Debug.Log("Something went wrong");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
