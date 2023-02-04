using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public Animator grandpaAnimation;
    public CinemachineVirtualCamera BookCam;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    
}
