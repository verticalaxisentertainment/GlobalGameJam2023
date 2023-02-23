using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

public class Sword : MonoBehaviour
{
    public static Sword instance;
    public bool isGameOver;

    public int playerHealt = 100;
    public ProceduralImage healtbar;
    public TMP_Text healthText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        healthText.text=playerHealt.ToString();
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
