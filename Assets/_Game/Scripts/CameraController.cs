using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    public CinemachineVirtualCamera[] cameras;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        cameras =FindObjectsOfType<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
