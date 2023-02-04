using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public Animator grandpaAnimation;
    public Animator book;
    public GameObject LevelSelectCanvas;
    public GameObject FadeoutCanvas;
    public CinemachineVirtualCamera BookCam;


    public IEnumerator FadeOut(int index)
    {
        FadeoutCanvas.SetActive(true);
        FadeoutCanvas.GetComponentInParent<Animator>().SetBool("Fade",true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
        StopCoroutine(FadeOut(index));
    }

    public IEnumerator FadeIn()
    {
        FadeoutCanvas.GetComponentInParent<Animator>().SetBool("Fade",true);
        FadeoutCanvas.SetActive(true);
        FadeoutCanvas.GetComponentInParent<Animator>().SetBool("Fade",false);
        yield return new WaitForSeconds(1);
        StopCoroutine(FadeIn());
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex==1)
        {
            LevelSelectCanvas.SetActive(false);
            DialogueManager.Instance.dialogueCanvas.SetActive(false);
            StartCoroutine(FadeIn());
            StartCoroutine(WarriorLevel());
        }
    }


    public IEnumerator WarriorLevel()
    {
        yield return new WaitForSeconds(1.5f);
        grandpaAnimation.SetInteger("AnimationState",1);
        DialogueManager.Instance.dialogueCanvas.SetActive(true);
        StartCoroutine(DialogueManager.Instance.Typing());

        StartCoroutine(BoredLevel());
    }

    public IEnumerator BoredLevel()
    {
        while(DialogueManager.Instance.dialogueIndex!=3)
        {
            yield return new WaitForSeconds(0f);
        }
        CameraController.Instance.cameras[0].Priority=0;
        StopCoroutine(BoredLevel());
    }

}
