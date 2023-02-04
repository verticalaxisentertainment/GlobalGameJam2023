using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAiScript : MonoBehaviour
{
    public static EnemyAiScript instance;
    public bool isAIActive;
    private NavMeshAgent navMeshAgent;
    public GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(isAIActive)
        {
            navMeshAgent.destination = character.transform.position;       

        }
    }

    private void Awake()
    {
        instance = this;
        navMeshAgent= GetComponent<NavMeshAgent>();

    }

}
