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


    [SerializeField] private List<Character> characters;

    [SerializeField] private GameObject dialogueContainer;
    [SerializeField] private List<GameObject> uiToHide;

    [SerializeField] private Image playerPortrait;
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private Image npcPortrait;
    [SerializeField] private TMP_Text npcName;

    [SerializeField] private TMP_Text text;


    private Queue<DialogueItem> items;
    private GameScript gameScript;

    // Start is called before the first frame update
    void Start()
    {
        dialogueContainer.SetActive(false);
        items = new Queue<DialogueItem>();

        // Create the script for the game
        gameScript = new GameScript();
    }


    public void StartDialogue(string dialogueId)
    {
        Dialogue d = gameScript.GetDialogue(dialogueId);
        items.Clear();
        foreach (DialogueItem i in d.lines)
        {
            items.Enqueue(i);
        }

        DisplayNextSentence();
        dialogueContainer.SetActive(true);
        foreach(GameObject g in uiToHide)
        {
            g.SetActive(false);
        }

        Player.Instance.stability.Pause();
        Player.Instance.interaction.isInDialouge = true;

    }

    public void DisplayNextSentence()
    {
        if (items.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueItem nextItem = items.Dequeue();
        StopAllCoroutines();
        // Set character portrait
        SetPortait(nextItem.speaker);
        StartCoroutine(TypeSentence(nextItem.text));

    }

    public void EndDialogue()
    {
        dialogueContainer.SetActive(false);
        foreach (GameObject g in uiToHide)
        {
            g.SetActive(true);
        }
        Player.Instance.stability.Resume();
        Player.Instance.interaction.isInDialouge = false;
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

    private void SetPortait(Character ch)
    {
        if(ch.isPlayer)
        {
            playerPortrait.sprite = ch.portraitSprite;
            playerPortrait.gameObject.SetActive(true);
            npcPortrait.gameObject.SetActive(false);
            playerName.text = ch.name;
            playerName.gameObject.SetActive(true);
            npcName.gameObject.SetActive(false);
        }
        else
        {
            npcPortrait.sprite = ch.portraitSprite;
            npcPortrait.gameObject.SetActive(true);
            playerPortrait.gameObject.SetActive(false);

            npcName.text = ch.name;
            npcName.gameObject.SetActive(true);
            playerName.gameObject.SetActive(false);
        }
    }
}