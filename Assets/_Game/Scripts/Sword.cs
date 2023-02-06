using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public static Sword instance;
    public bool isGameOver;

    private void Awake()
    {
        instance = this;
    }
    private void OnCollisionEnter(Collision collision)
    {
        isGameOver = true;

        if(collision.gameObject.CompareTag("Opponent"))
        {
            Debug.Log("Vurdu");
            Gladiator.Instance.health-=10;
        }
    }
}
