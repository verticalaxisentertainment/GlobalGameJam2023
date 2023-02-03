using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetectionScript : MonoBehaviour
{
    public GameObject sword;
    public ParticleSystem dyingEffect;
    public SwordScript swordScript;
    public Collision swordCollision;
    public int healt;

    // Start is called before the first frame update
    void Start()
    {
        swordCollision = sword.GetComponent<Collision>();
        if (this.CompareTag("Dino")) 
        {
            healt = 1000000;
        }
        else if (this.CompareTag("Boxer"))
        {
            healt = 1000;
        }
        else if (this.CompareTag("Warior"))
        {
            healt = 100;
        }
        else 
        {
            healt = 100;
            Debug.Log("Something Went Wrong");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(healt <= 0) 
        {
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        healt -= swordScript.swordStrength;
    }
}
