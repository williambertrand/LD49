using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<Character> characters;


    public Character GetCharcter(string name)
    {
        return characters.Find(c => c.name == name);
    }
}
