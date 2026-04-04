using System;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "BookData", menuName = "Scriptable Objects/BookData")]
public class BookData : ScriptableObject
{
    [ContextMenu("Create New GUID")]
    public void GenerateUID()
    {
        bookID = Guid.NewGuid().ToString();
    }
    public string bookID;
    public string bookTitle;
    public string authorName;
    public float publicationYear;
    public bookGenre BookGenre;
    public string bookBio;

}


[Flags]
public enum bookGenre { 
    TBA = 0x00, 
    Horror = 0x01, 
    Romance = 1 << 1, 
    ChildrensFiction = 1 << 2, 
    Academic = 1 << 3, 
    Play = 1 << 4, 
    Tragedy = 1 << 5, 
    Gothic = 1 << 6, 
    LiteraryFiction = 1 << 7, 
    Vampire = 1 << 8, 
    Fantasy = 1 << 9, 
    Epic = 1 << 10, 
    Crime = 1 << 11, 
    Mystery = 1 << 12,  
    Adventure = 1 << 13,
    ScienceFiction = 1 << 14
    };
