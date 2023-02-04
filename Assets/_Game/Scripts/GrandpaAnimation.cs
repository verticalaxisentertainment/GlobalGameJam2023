using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandpaAnimation : MonoBehaviour
{
    public GameObject bangParticle;
    public GameObject hand;
    IEnumerator BangwithTime()
    {
        CameraController.Instance.cameras[1].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain=1;
        yield return new WaitForSeconds(0.5f);
        CameraController.Instance.cameras[1].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain=0;
        StopCoroutine(BangwithTime());
    }

    public void Bang()
    {
        Instantiate(bangParticle,hand.transform);
        StartCoroutine(BangwithTime());
    }
}
