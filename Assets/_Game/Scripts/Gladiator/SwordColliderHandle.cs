using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordColliderHandle : MonoBehaviour
{
    public Collider swordCollider;
    public void Activate(int active)
    {
        if(active == 1)
            swordCollider.isTrigger = false;
        if(active==0)
            swordCollider.isTrigger=true;
    }
}
