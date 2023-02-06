using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI.ProceduralImage;

public class Gladiator : MonoBehaviour
{
    public static Gladiator Instance;


    public int health=100;
    public CinemachineVirtualCamera gameCam;

    public ProceduralImage healthImage;
    public TMP_Text healthText;

    public GameObject player;
    public Animator animator;
    private NavMeshAgent meshAgent;

    private bool once=true;

    private void Awake()
    {
        Instance= this;
    }
    void Start()
    {
        meshAgent=GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        healthText.text=health.ToString();
        meshAgent.destination=player.transform.position;

        healthImage.fillAmount=health/100.0f;
        if(meshAgent.remainingDistance<=15.6f&&once)
        {
            animator.SetInteger("State",1);
            once=false;
        }
        if(meshAgent.remainingDistance>3.0f&&!once)
        {
            animator.SetInteger("State",0);
        }

        if(health<=0.0f)
        {
            StartCoroutine(LevelManager.Instance.FadeOut(SceneManager.GetActiveScene().buildIndex+1));
        }

        if(Input.GetKeyDown(KeyCode.P)) 
        {
            StartCoroutine(LevelManager.Instance.FadeOut(SceneManager.GetActiveScene().buildIndex+1));
        }
    }


    public void Shake()
    {
        StartCoroutine(Shakee());
    }

    private IEnumerator Shakee()
    {
        gameCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain=3;
        yield return new WaitForSeconds(0.5f);
        gameCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain=0;
    }
}
