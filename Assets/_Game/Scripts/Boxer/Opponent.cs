using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    public static Opponent Instance;

    public Rigidbody[] rigidbodies;
    public Collider[] colliders;

    private void Awake()
    {
        Instance = this;
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
}
