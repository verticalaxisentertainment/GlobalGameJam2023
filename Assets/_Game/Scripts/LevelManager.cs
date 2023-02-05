using Cinemachine;
using System.Collections;
using System.Collections.Generic;
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
    public AudioClip[] grandPasounds,childSounds;
    public AudioSource audioSource;



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
        audioSource=GetComponent<AudioSource>();
        if(SceneManager.GetActiveScene().buildIndex==1)
        {
            Cursor.visible = true;
            LevelSelectCanvas.SetActive(false);
            DialogueManager.Instance.dialogueCanvas.SetActive(false);
            StartCoroutine(FadeIn());
            StartCoroutine(WarriorLevel());
        }
        if(SceneManager.GetActiveScene().buildIndex==2)
        {
            Cursor.visible = false;
        }
        if(SceneManager.GetActiveScene().buildIndex==3)
        {
            Cursor.visible = true;
            DialogueManager.Instance.dialogueCanvas.SetActive(false);
            StartCoroutine(FadeIn());
            StartCoroutine(FirstDistraction());
        }
        if(SceneManager.GetActiveScene().buildIndex==4)
        {
            Cursor.visible = false;
            DialogueManager.Instance.dialogueCanvas.SetActive(false);
            StartCoroutine(FadeIn());
            StartCoroutine(ArenaLevel());
        }
    }

    public IEnumerator FirstDistraction()
    {
        DialogueManager.Instance.dialogueCanvas.SetActive(true);
        StartCoroutine(DialogueManager.Instance.Typing());
        while(true)
        {
            if(DialogueManager.Instance.dialogueIndex==1)
            {
                grandpaAnimation.GetComponent<Animator>().SetInteger("AnimationState",3);
                //GrandpaAnimation.Instance.Bang();
                break;
            }

            yield return new WaitForSeconds(0.2f);
        }
        while(true)
        {
            if(DialogueManager.Instance.dialogueIndex==2)
            {
                grandpaAnimation.GetComponent<Animator>().SetInteger("AnimationState",1);
            }
            yield return new WaitForSeconds(0.2f);
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
        LevelManager.Instance.audioSource.loop=true;
        LevelManager.Instance.audioSource.Play();
        StopCoroutine(BoredLevel());
    }

    public IEnumerator ArenaLevel()
    {
        yield return new WaitForSeconds(1);
        DialogueManager.Instance.dialogueCanvas.SetActive(true) ;
        StartCoroutine(DialogueManager.Instance.Typing());
    }

}
