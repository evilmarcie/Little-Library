using System;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "BookData", menuName = "Scriptable Objects/BookData")]
public class BookData : ScriptableObject
{
    public string bookTitle;
    public string authorName;
    public float publicationYear;
    public bookGenre BookGenre;
    public string bookBio;

}

[Flags]
public enum bookGenre { TBA, Horror, Romance, Childrens, Academic, Play, Tragedy, Gothic, Literary, Vampire, Fantasy, Epic, Crime, Mystery };
