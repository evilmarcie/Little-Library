using System;
using System.Collections.Generic;
using UnityEngine;

public class BookSaving : MonoBehaviour
{

    [Serializable]
    public class GameData
    {
        public List<BookGroup> Groups;
    }

    [Serializable]
    public class BookGroup
    {
        public string Id;
        public List<BookData> Books;
    }

    [Serializable]
    public class BookInfo
    {
        
    }
}

