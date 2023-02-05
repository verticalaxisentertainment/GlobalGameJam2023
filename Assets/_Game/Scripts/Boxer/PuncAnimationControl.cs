using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuncAnimationControl : MonoBehaviour
{
    public Collider Rglove,Lglove;

    public void Activate(int active)
    {
        if(active==1)
        {
            Rglove.enabled = true;
            Lglove.enabled = true;
        }
        else
        {
             Rglove.enabled = false;
             Lglove.enabled = false;
        }
    }
}
