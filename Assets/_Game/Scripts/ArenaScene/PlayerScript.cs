using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI.ProceduralImage;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;

    public ProceduralImage healtbar;
    public int swordStrength;
    public int playerHealt = 100;
    private float timer = 0;
    public float attackSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
       PlayerSetTags();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= attackSpeed && Vector3.Distance(transform.position, EnemyScript.instance.transform.position) < 5)
        {
            playerHealt -= EnemyScript.instance.enemyDamage;
            healtbar.fillAmount = playerHealt / 100f;
            timer= 0;
        }

        if(playerHealt <= 0)
        {
            SceneManager.LoadScene(0);
            Debug.Log("Player Died");
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    playerHealt -= EnemyScript.instance.enemyDamage;
    //}

    private void Awake()
    {
        instance = this;
    }


    private void PlayerSetTags()
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
}
