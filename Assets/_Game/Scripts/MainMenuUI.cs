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
    }

    public void OnPlayClick()
    {
        mainMenu.SetActive(false);
        dialogueCanvas.SetActive(true);
        LevelManager.Instance.grandpaAnimation.SetInteger("AnimationState",1);

        StartCoroutine(DialogueManager.Instance.Typing()); 
    }

    public void OnClickExit()
    {
        Application.Quit(1);
    }

}
