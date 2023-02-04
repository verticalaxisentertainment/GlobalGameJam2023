using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI Instance;

    public GameObject mainMenu;
    public GameObject dialogueCanvas;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        mainMenu.SetActive(true);
        dialogueCanvas.SetActive(false);
        LevelManager.Instance.LevelSelectCanvas.SetActive(false);
        LevelManager.Instance.FadeoutCanvas.SetActive(false);
    }

    public void OnPlayClick()
    {
        mainMenu.SetActive(false);
        dialogueCanvas.SetActive(true);
        LevelManager.Instance.grandpaAnimation.SetInteger("AnimationState",1);

        if(DialogueManager.Instance.dialogueArrays[DialogueManager.Instance.dialogueIndex].grandPa)
            LevelManager.Instance.audioSource.clip=LevelManager.Instance.grandPasounds[Random.Range(0,1)];
        else
            LevelManager.Instance.audioSource.clip=LevelManager.Instance.childSounds[Random.Range(0,5)];

        LevelManager.Instance.audioSource.Play();

        StartCoroutine(DialogueManager.Instance.Typing()); 
    }

    public void OnClickExit()
    {
        Application.Quit(1);
    }

}
