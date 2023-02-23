using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI.ProceduralImage;

public class Glove : MonoBehaviour
{
    public static Glove Instance;

    public GameObject hitParticle;


    public int playerHealt = 100;
    public ProceduralImage healtbar;

    public TMP_Text healthText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        healthText.text=playerHealt.ToString();
    }

    private void Update()
    {
        if(playerHealt<0) playerHealt= 0;
        healthText.text= playerHealt.ToString();
        healtbar.fillAmount = playerHealt / 100.0f;

        if (playerHealt<=0.0f)
        {
            StartCoroutine(LevelManager.Instance.FadeOut(SceneManager.GetActiveScene().buildIndex));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Opponent"))
        {
            Instantiate(hitParticle,collision.GetContact(0).point,Quaternion.identity);
            Opponent.Instance.health-=10;
            Opponent.Instance.punched=true;
            StartCoroutine(Opponent.Instance.Hit(10));
            Opponent.Instance.hitSound.Play();
            if(Opponent.Instance.health<=0)
            {
                Opponent.Instance.navMeshAgent.Stop();
                Opponent.Instance.ActivateRagdoll();
                StartCoroutine(LevelManager.Instance.NextSceneE(2f));
            }
        }
    }
}
