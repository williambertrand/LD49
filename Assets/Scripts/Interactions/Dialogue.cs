using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Character
{
    public Sprite portraitSprite;
    public string name;
    public bool isPlayer;

    public Character(string name, bool isPlayer, string portraitPath)
    {
        this.name = name;
        this.isPlayer = isPlayer;
        portraitSprite = Resources.Load<Sprite>(portraitPath);

        if(portraitSprite == null)
        {
            Debug.LogError("Could not find portrait at: " + portraitPath);
        }
    }
}

[System.Serializable]
public class DialogueItem
{
    public Character speaker;
    [TextArea(3, 5)]
    public string text;

    public DialogueItem(Character speaker, string text)
    {
        this.speaker = speaker;
        this.text = text;
    }

}

[System.Serializable]
public class Dialogue
{   
    public List<DialogueItem> lines;

    public Dialogue()
    {
        lines = new List<DialogueItem>();
    }
}