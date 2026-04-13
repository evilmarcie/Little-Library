using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<Character> characters;

    private Dictionary<string, Character> lookup;

    void Awake()
    {
        instance = this;
        
        lookup = new Dictionary<string, Character>();
        
        foreach (var character in characters)
        {
            lookup [character.characterID] = character;
        }
    }

    public Character GetCharacter(string id)
    {
        return lookup[id];
    }

    public static CharacterManager instance;
}
