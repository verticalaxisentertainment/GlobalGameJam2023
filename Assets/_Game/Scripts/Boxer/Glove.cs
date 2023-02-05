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
    bool pass=true;

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
        healthText.text= playerHealt.ToString();
        //healtbar.fillAmount=playerHealt/100.0f;

        if(playerHealt<=0.0f)
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
            StartCoroutine(Opponent.Instance.Hit(10));
            if(Opponent.Instance.health<=0)
            {
                Opponent.Instance.navMeshAgent.Stop();
                StartCoroutine(NextScene());
                Opponent.Instance.ActivateRagdoll();
            }
        }
    }

    

    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(LevelManager.Instance.FadeOut(SceneManager.GetActiveScene().buildIndex+1));
    }

    public IEnumerator Damage(float damage)
    {
        float givenDamage=damage/100.0f;

        float health=Glove.Instance.healtbar.fillAmount-givenDamage;

        while(true)
        {
            if(health<Glove.Instance.healtbar.fillAmount)
            {
                Glove.Instance.healtbar.fillAmount-=Time.deltaTime/10;
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(0);
        }
        yield return new WaitForSeconds(1);
    }
}
