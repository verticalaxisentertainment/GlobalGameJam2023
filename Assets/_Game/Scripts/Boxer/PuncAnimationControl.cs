using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuncAnimationControl : MonoBehaviour
{
    public Collider glove;

    public void Activate(int active)
    {
        if(active==1)
        {
            glove.enabled = true;
        }
        else
        {
             glove.enabled = false;
        }
    }
}
