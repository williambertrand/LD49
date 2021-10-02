using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character
{
    Sprite portraitSprite;
    string name;
    bool isPlayer;
}

[System.Serializable]
public class DialogueItem
{
    public Character speaker;
    [TextArea(3, 5)]
    public string lines;

}

[System.Serializable]
public class Dialogue
{
    public string speakerName;
    [TextArea(3, 5)]
    public List<string> sentences;
}