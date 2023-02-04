using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoAITriggerScript : MonoBehaviour
{
    Collider[] colliders = new Collider[3];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            EnemyAiScript.instance.isAIActive = true;
            EnemyAiScript.instance.gameObject.GetComponent<Animator>().SetInteger("State",1);
        }
    }
}
