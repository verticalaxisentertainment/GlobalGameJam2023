using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public static EnemyScript instance;
    public int enemyHealth;
    public int enemyDamage;

    public GameObject destroyParticle;

    // Start is called before the first frame update
    void Start()
    {
        EnemyTagSet();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth <= 0) 
        {
            //Load Victory Scene
            SceneManager.LoadScene(3);
            Debug.Log("Enemydied Died");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Sword"))
        {
            enemyHealth -= PlayerScript.instance.swordStrength;
            Debug.Log("enemyhit");
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            EnemyAiScript.instance.gameObject.GetComponent<Animator>().SetInteger("State",2);
            EnemyAiScript.instance.Hit();
        }

        if(collision.gameObject.CompareTag("Box"))
        {
            Instantiate(destroyParticle,collision.transform.position,Quaternion.identity);
            EnemyAiScript.instance.audioSource.clip=EnemyAiScript.instance.audioClips[1];
            EnemyAiScript.instance.audioSource.Play();
            Destroy(collision.gameObject);
        }
    }
    private void Awake()
    {
        instance = this;
    }

    private void EnemyTagSet()
    {
        if (gameObject.CompareTag("Dino"))
        {
            enemyHealth = 1000000;
            enemyDamage = 1000000;
        }
        else if (gameObject.CompareTag("Boxer"))
        {
            enemyHealth = 1000;
            enemyDamage = 1000;
        }
        else if (gameObject.CompareTag("Warior"))
        {
            enemyHealth = 100;
            enemyDamage = 5;
        }
        else
        {
            enemyHealth = 1;
            enemyDamage = 1;
            Debug.Log("Something Went Wrong");
        }
    }
}
