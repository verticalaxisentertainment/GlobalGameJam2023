using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public static EnemyScript instance;
    public int enemyHealth;
    public int enemyDamage = 10;

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
        enemyHealth -= PlayerScript.instance.swordStrength;
        Debug.Log("enemyhit");
    }
    private void Awake()
    {
        instance = this;
        EnemyTagSet();
    }

    private void EnemyTagSet()
    {
        if (this.CompareTag("Dino"))
        {
            enemyHealth = 1000000;
            enemyDamage = 1000000;
        }
        else if (this.CompareTag("Boxer"))
        {
            enemyHealth = 1000;
            enemyDamage = 1000;
        }
        else if (this.CompareTag("Warior"))
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
