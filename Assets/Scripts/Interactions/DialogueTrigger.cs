using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{

    public List<string> dialogueIds;
    private int currentDialogue = 0;

    [SerializeField] private bool hasMultipleDialogues;
    private bool hasTalked;

    public override void OnInteract()
    {

        if(!hasMultipleDialogues && hasTalked)
        {
            return;
        }

        hasTalked = true;

        //Trigger the dialogue
        DialogueManager.Instance.StartDialogue(dialogueIds[currentDialogue]);
        Player.Instance.interaction.Dismiss();
        currentDialogue += 1;

        if(currentDialogue == dialogueIds.Count)
        {
            currentDialogue = dialogueIds.Count - 1;
        }
    }
}