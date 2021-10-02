using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{

    public Dialogue dialogue;

    public override void OnInteract()
    {
        //Trigger the dialogue
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}