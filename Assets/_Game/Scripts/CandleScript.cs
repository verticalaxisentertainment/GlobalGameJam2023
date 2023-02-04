using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleScript : MonoBehaviour
{
    public Light pointLight;
    void Start()
    {
        pointLight=GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        pointLight.intensity= Mathf.Lerp(UnityEngine.Random.Range(1.5f,2),UnityEngine.Random.Range(2.5f,3),Mathf.Sin(Time.time));
    }
}
