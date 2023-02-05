using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

public class PlayerCollider : MonoBehaviour
{
    private IEnumerator Shake()
    {
        CameraController.Instance.cameras[0].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain=3;
        yield return new WaitForSeconds(0.5f);
        CameraController.Instance.cameras[0].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain=0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Glove"))
        {
            StartCoroutine(Shake());
            Glove.Instance.playerHealt-=10;
        }
    }
}
