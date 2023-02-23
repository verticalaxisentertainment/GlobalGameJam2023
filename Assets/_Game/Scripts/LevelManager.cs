using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    public AudioClip[] grandPasounds, childSounds;
    public AudioSource audioSource;

    public GameObject flower, sword, showerParticleEffect, collectableSword;
    public bool move = false;

    public IEnumerator FadeOut(int index)
    {
        FadeoutCanvas.SetActive(true);
        FadeoutCanvas.GetComponentInParent<Animator>().SetBool("Fade", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
        StopCoroutine(FadeOut(index));
    }

    public IEnumerator FadeIn()
    {
        FadeoutCanvas.GetComponentInParent<Animator>().SetBool("Fade", true);
        FadeoutCanvas.SetActive(true);
        FadeoutCanvas.GetComponentInParent<Animator>().SetBool("Fade", false);
        yield return new WaitForSeconds(1);
        StopCoroutine(FadeIn());
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Cursor.visible = true;
            LevelSelectCanvas.SetActive(false);
            DialogueManager.Instance.dialogueCanvas.SetActive(false);
            StartCoroutine(FadeIn());
            StartCoroutine(WarriorLevel());
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Cursor.visible = false;
            move = true;
        }
        if (SceneManager.GetActiveScene().name == "FirstDistraction")
        {
            Cursor.visible = true;
            DialogueManager.Instance.dialogueCanvas.SetActive(false);
            StartCoroutine(FadeIn());
            StartCoroutine(FirstDistraction());
        }
        if (SceneManager.GetActiveScene().name == "Arena")
        {
            Cursor.visible = false;
            PlayerScript.instance.healtbar.GetComponentInParent<Canvas>().gameObject.SetActive(false);
            DialogueManager.Instance.dialogueCanvas.SetActive(false);
            flower.SetActive(false);
            sword.SetActive(false);
            collectableSword.SetActive(false);
            StartCoroutine(FadeIn());
            StartCoroutine(ArenaLevel());
        }
        if (SceneManager.GetActiveScene().name == "AfterDinasour")
        {
            Cursor.visible = true;
            LevelSelectCanvas.SetActive(false);
            DialogueManager.Instance.dialogueCanvas.SetActive(false);
            StartCoroutine(FadeIn());
            StartCoroutine(AfterDinasour());
        }
        if (SceneManager.GetActiveScene().name == "Final_Arena")
        {
            move = true;
            Cursor.visible = false;
        }
        if (SceneManager.GetActiveScene().name == "Final")
        {
            Cursor.visible = true;
            LevelSelectCanvas.SetActive(false);
            DialogueManager.Instance.dialogueCanvas.SetActive(false);
            StartCoroutine(FadeIn());
            StartCoroutine(Final());
        }
    }

    public void StartingCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    private IEnumerator FirstDistraction()
    {
        yield return new WaitForSeconds(1.4f);
        DialogueManager.Instance.dialogueCanvas.SetActive(true);
        StartCoroutine(DialogueManager.Instance.Typing());
    }

    private IEnumerator WarriorLevel()
    {
        yield return new WaitForSeconds(1.5f);
        grandpaAnimation.SetInteger("AnimationState", 1);
        DialogueManager.Instance.dialogueCanvas.SetActive(true);
        StartCoroutine(DialogueManager.Instance.Typing());

        StartCoroutine(BoredLevel());
    }

    private IEnumerator BoredLevel()
    {
        while (DialogueManager.Instance.dialogueIndex != 3)
        {
            yield return new WaitForSeconds(0f);
        }
        CameraController.Instance.cameras[0].Priority = 0;
        audioSource.loop = true;
        audioSource.Play();
        //StopCoroutine(BoredLevel());
    }

    private IEnumerator ArenaLevel()
    {
        yield return new WaitForSeconds(1);
        DialogueManager.Instance.dialogueCanvas.SetActive(true);
        StartCoroutine(DialogueManager.Instance.Typing());

        while (true)
        {
            if (DialogueManager.Instance.dialogueIndex == 3)
            {
                Instantiate(showerParticleEffect, flower.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                flower.SetActive(true);
                break;
            }
            yield return new WaitForSeconds(0);
        }
        while (true)
        {
            if (DialogueManager.Instance.dialogueIndex == 5)
            {
                DialogueManager.Instance.skipText.enabled = false;
                flower.SetActive(false);
                Instantiate(showerParticleEffect, collectableSword.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                collectableSword.SetActive(true);
                move = true;
                //sword.SetActive(true);
                break;
            }
            yield return new WaitForSeconds(0);
        }

        //StopCoroutine(ArenaLevel());
    }

    private IEnumerator AfterDinasour()
    {
        yield return new WaitForSeconds(1);
        DialogueManager.Instance.dialogueCanvas.SetActive(true);
        StartCoroutine(DialogueManager.Instance.Typing());
    }

    private bool last = false;
    private IEnumerator Final()
    {
        yield return new WaitForSeconds(1);
        DialogueManager.Instance.dialogueCanvas.SetActive(true);
        StartCoroutine(DialogueManager.Instance.Typing());

        while (true)
        {
            if (DialogueManager.Instance.dialogueIndex == 5)
            {
                last = true;
                break;
            }
            yield return new WaitForSeconds(0);
        }
    }

    public void StartStoryTelling()
    {
        BookCam.Priority = 20;
        grandpaAnimation.SetInteger("AnimationState", 0);
        book.GetComponentInChildren<Animator>().SetBool("Open", true);
        DialogueManager.Instance.dialogueCanvas.SetActive(false);
    }

    public void NextScene(float time)
    {
        DialogueManager.Instance.dialogueCanvas.SetActive(false);
        audioSource.Stop();
        StartCoroutine(NextSceneE(time));
    }


    public IEnumerator NextSceneE(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(FadeOut(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void LoadScene(int index)
    {
        DialogueManager.Instance.dialogueCanvas.SetActive(false);
        audioSource.Stop();
        StartCoroutine(FadeOut(index));
    }

    


    public bool canTakeSword = false;
    public void TaketheSword()
    {
        if (canTakeSword)
        {
            collectableSword.SetActive(false);
            DialogueManager.Instance.dialogueCanvas.SetActive(false);
            sword.SetActive(true);
            audioSource.enabled = false;
            MoveScript.Instance.GetComponent<AudioSource>().Play();
        }
    }

}
