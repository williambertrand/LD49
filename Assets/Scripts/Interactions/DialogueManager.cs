using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    #region Singleton
    public static DialogueManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion


    [SerializeField] private GameObject dialogueContainer;
    [SerializeField] private GameObject uiToHide;
    [SerializeField] private TMP_Text speakerName;
    [SerializeField] private TMP_Text text;


    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        dialogueContainer.SetActive(false);
        sentences = new Queue<string>();
    }


    public void StartDialogue(Dialogue d)
    {
        sentences.Clear();
        foreach (string sentence in d.sentences)
        {
            sentences.Enqueue(sentence);
        }

        speakerName.text = d.speakerName;
        DisplayNextSentence();
        dialogueContainer.SetActive(true);
        uiToHide.SetActive(false);

        Player.Instance.stability.Pause();

    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string nextSentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(nextSentence));

    }

    public void EndDialogue()
    {
        dialogueContainer.SetActive(false);
        uiToHide.SetActive(true);
        Player.Instance.stability.Resume();
    }


    //Animate text typing
    IEnumerator TypeSentence(string sentence)
    {
        text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return null; // wait till end of frame
        }
    }
}