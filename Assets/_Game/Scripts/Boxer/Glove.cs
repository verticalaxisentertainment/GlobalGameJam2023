using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Glove : MonoBehaviour
{
    public static Glove Instance;

    public GameObject hitParticle;
    bool pass=true;

    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator DeactivateCollision()
    {
        
        yield return new WaitForSeconds(0.2f);
        GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(1.2f);
        GetComponent<Collider>().enabled = false;
        pass=true;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)&&pass)
        {
            pass = false;
            StartCoroutine(DeactivateCollision());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Opponent"))
        {
            Instantiate(hitParticle,collision.GetContact(0).point,Quaternion.identity);
            Opponent.Instance.ActivateRagdoll();
            StartCoroutine(NextScene());
        }
    }


    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(LevelManager.Instance.FadeOut(SceneManager.GetActiveScene().buildIndex+1));
    }
}
