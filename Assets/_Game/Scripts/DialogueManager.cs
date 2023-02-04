using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [System.Serializable]
    public class DialogueArray
    {
        public string dialogueText;
        public bool onEvent;
    }

    public int dialogueIndex;
    public DialogueArray[] dialogueArrays;
    public string[] dialogueText;
    private TMP_Text textObject;
    private string currentString;
    public GameObject dialogueCanvas;
    public TMP_Text skipText;

    private bool typing=true;
    private bool clikedwhiletyping=false;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        textObject=dialogueCanvas.GetComponentInChildren<TMP_Text>();
    }

    public IEnumerator Typing()
    {
        textObject.text="";
        currentString=dialogueArrays[dialogueIndex].dialogueText;
        typing=true;
        foreach(var i in currentString) 
        {
            textObject.text+=i.ToString();
            if(clikedwhiletyping)
            {
                textObject.text=currentString;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }


        typing=false;
        clikedwhiletyping=false;
        skipText.gameObject.SetActive(true);
        StopCoroutine(Typing());
    }

    public void OnSkipDialogue()
    {
        if(dialogueArrays[dialogueIndex].onEvent&&!typing)
        {
            LevelManager.Instance.BookCam.Priority=20;
            LevelManager.Instance.grandpaAnimation.SetInteger("AnimationState",0);
            dialogueCanvas.SetActive(false);

        }
        
        if(typing)
        {
            clikedwhiletyping=true;
        }

        if(!typing)
        {
            if(dialogueIndex!=dialogueArrays.Length-1)
                dialogueIndex++;
            skipText.gameObject.SetActive(false);
            StartCoroutine(Typing());
        }
    }
}
