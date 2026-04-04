using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[Serializable]
public class GameData
{
    //counterScene
    public Character activeCustomer;

    //shelvesScene
    public List<BookGroup> ShelfGroup = new List<BookGroup>();

    [Serializable]
    public struct BookGroup
    {
        public string Id;
        public List<BookData> Books;
    }
    public List<BookInfo> Books = new List<BookInfo>();

    [Serializable]
    public struct BookInfo
    {
        public BookData bookInfo;
    }

    //other

    public GameData()
    {
        //inital values
        // data when there is no values to load

        activeCustomer = null;
        ShelfGroup.Clear();
        Books.Clear();
    }
}
