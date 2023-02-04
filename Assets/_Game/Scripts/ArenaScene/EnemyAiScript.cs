using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Cinemachine;

public class EnemyAiScript : MonoBehaviour
{
    public static EnemyAiScript instance;
    public bool isAIActive;
    private NavMeshAgent navMeshAgent;
    public GameObject character;
    public CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        
    }

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

    private IEnumerator Shakee()
    {
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain=1;
        yield return new WaitForSeconds(0.2f);
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain=0;
        StopCoroutine(Shakee());
    }

    public void Shake()
    {
        StartCoroutine(Shakee());
    }

    public void Hit()
    {
        Debug.Log("hit");
        PlayerScript.instance.playerHealt-=100000;
    }

}
