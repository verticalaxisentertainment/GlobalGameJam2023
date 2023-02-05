using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

public class PlayerCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Glove"))
        {
            Glove.Instance.playerHealt-=10;
            StartCoroutine(Glove.Instance.Damage(10));
        }
    }
}
