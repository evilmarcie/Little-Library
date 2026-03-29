using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    //basic info
    public string fullName;
    public string characterName;
    public string pronouns;
    public int age;
    public string career;

    //visuals
    public Sprite characterSprite;

    //book preferences
    public bookGenre favGenre;
    public BookData[] lovedBooks;
    public BookData[] likedBooks;
    public BookData[] neutralBooks;
    public BookData[] dislikedBooks;

}


