using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI.ProceduralImage;

public class Opponent : MonoBehaviour
{
    public static Opponent Instance;

    public int health=100;

    public Rigidbody[] rigidbodies;
    public Collider[] colliders;
    public GameObject healthBar;
    public AudioSource hitSound;

    private TMP_Text healthText;
    private Animator animator;

    public NavMeshAgent navMeshAgent;
    private int i=0;

    public bool punched=false;


    public IEnumerator Hit(float damage)
    {
        float givenDamage=damage/100.0f;

        float health=healthBar.GetComponentInChildren<ProceduralImage>().fillAmount-givenDamage;

        while(true)
        {
            if(health<healthBar.GetComponentInChildren<ProceduralImage>().fillAmount)
            {
                healthBar.GetComponentInChildren<ProceduralImage>().fillAmount-=Time.deltaTime/10;
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(0);
        }
        yield return new WaitForSeconds(1);
    }

   

    private void Awake()
    {
        Instance = this;
        navMeshAgent=GetComponent<NavMeshAgent>();
        healthText=GetComponentInChildren<TMP_Text>();
        animator=GetComponent<Animator>();
        hitSound=GetComponent<AudioSource>();
    }


    private void Update()
    {
        navMeshAgent.destination=Camerascript.instance.transform.position;

        healthText.text=health.ToString();

        if(punched)
        {
            animator.SetInteger("Punch",2);
            punched=false;
        }
        if(navMeshAgent.remainingDistance<=3.0f&&!punched)
        {
            animator.SetInteger("Punch",1);
        }
        if(navMeshAgent.remainingDistance>3.0f&&!punched)
        {
            animator.SetInteger("Punch",0);
        }
    }
    void Start()
    {
        rigidbodies=GetComponentsInChildren<Rigidbody>();
        colliders=GetComponentsInChildren<Collider>();

        foreach(var rb in rigidbodies)
        {
            rb.isKinematic=true;
        }
        rigidbodies[0].isKinematic=false;

        foreach(var cl in colliders)
        {
            cl.isTrigger=true;
        }
        colliders[0].isTrigger=false;
        colliders[9].isTrigger=false;
        colliders[13].isTrigger=false;
    }

    public void ActivateRagdoll()
    {
        GetComponent<Animator>().enabled=false;
        foreach(var rb in rigidbodies)
        {
            rb.isKinematic=false;
        }
        rigidbodies[0].isKinematic=true;

        foreach(var cl in colliders)
        {
            cl.isTrigger=false;
        }
        colliders[0].isTrigger=true;

        foreach (var rb in rigidbodies)
        {
            rb.AddExplosionForce(50,Glove.Instance.transform.position,15,1,ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Glove.Instance.playerHealt -= EnemyScript.instance.enemyDamage;
            Glove.Instance.healtbar.fillAmount = Glove.Instance.playerHealt / 100f;
            healthText.text= Glove.Instance.playerHealt.ToString();
        }
    }
}
