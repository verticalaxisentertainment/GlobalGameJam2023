using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BookAnimation : MonoBehaviour
{
    public ParticleSystem blood;
    bool fadeout=false;
    float alpha=0;
    public void BookOpen()
    {
       LevelManager.Instance.LevelSelectCanvas.SetActive(true);
    }

    public void Start()
    {
        blood.gameObject.SetActive(false);
    }

    private void Update()
    {
    }

    public void startBlood(bool start)
    {
        blood.gameObject.SetActive(start);
    }

    public void SelectedChracter()
    {
        StartCoroutine(LevelManager.Instance.FadeOut(1));
    }
}
