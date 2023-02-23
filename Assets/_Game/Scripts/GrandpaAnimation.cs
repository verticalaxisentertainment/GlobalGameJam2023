using Cinemachine;
using Dreamteck.Splines.Primitives;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class GrandpaAnimation : MonoBehaviour
{
    public static GrandpaAnimation Instance;

    public GameObject bangParticle;
    public GameObject hand;


    private void Awake()
    {
        Instance= this;
    }

    
    IEnumerator BangwithTime()
    {
        CameraController.Instance.cameras[0].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain=1;
        yield return new WaitForSeconds(0.5f);
        CameraController.Instance.cameras[0].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain=0;
        StopCoroutine(BangwithTime());
    }

    public void Bang()
    {
        Instantiate(bangParticle,hand.transform);
        StartCoroutine(BangwithTime());
    }

    public void SetAnimaton(int state)
    {
        gameObject.GetComponent<Animator>().SetInteger("AnimationState", state);
    }
}
