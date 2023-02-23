using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [System.Serializable]
    public class DialogueArray
    {
        public string dialogueText;
        public bool onEvent;
        public bool grandPa;
        public bool child;
        public int soundIndex;

        public UnityEvent _event;
    }

    public int dialogueIndex;
    public DialogueArray[] dialogueArrays;
    private TMP_Text textObject;
    private string currentString;
    public GameObject dialogueCanvas;
    public TMP_Text skipText;

    private bool typing = true;
    private bool clikedwhiletyping = false;


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            OnSkipDialogue();
    }
    void Start()
    {
        textObject = dialogueCanvas.GetComponentInChildren<TMP_Text>();
    }

    public IEnumerator Typing()
    {
        textObject.text = "";
        if (dialogueArrays[dialogueIndex].grandPa)
        {
            LevelManager.Instance.grandpaAnimation.SetInteger("AnimationState", 1);
            LevelManager.Instance.audioSource.clip = LevelManager.Instance.grandPasounds[dialogueArrays[dialogueIndex].soundIndex];
        }
        else
        {
            LevelManager.Instance.grandpaAnimation.SetInteger("AnimationState", 0);
            LevelManager.Instance.audioSource.clip = LevelManager.Instance.childSounds[dialogueArrays[dialogueIndex].soundIndex];
        }
        LevelManager.Instance.audioSource.Play();
        currentString = dialogueArrays[dialogueIndex].dialogueText;
        typing = true;
        foreach (var i in currentString)
        {
            textObject.text += i.ToString();
            if (clikedwhiletyping)
            {
                textObject.text = currentString;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }

        typing = false;
        clikedwhiletyping = false;
        skipText.gameObject.SetActive(true);
        if (!LevelManager.Instance.audioSource.loop)
            LevelManager.Instance.audioSource.Stop();
        StopCoroutine(Typing());
    }

    public void OnSkipDialogue()
    {
        if (typing)
        {
            clikedwhiletyping = true;
        }



        if (!typing && !dialogueArrays[dialogueIndex].onEvent)
        {
            Debug.Log("selam");
            if (dialogueIndex != dialogueArrays.Length - 1)
                dialogueIndex++;
            skipText.gameObject.SetActive(false);
            StartCoroutine(Typing());
        }

        if (dialogueArrays[dialogueIndex].onEvent && !typing)
        {
            if (dialogueIndex != dialogueArrays.Length - 1)
                dialogueIndex++;
            skipText.gameObject.SetActive(false);
            StartCoroutine(Typing());
            dialogueArrays[dialogueIndex]._event.Invoke();
        }
    }
}
