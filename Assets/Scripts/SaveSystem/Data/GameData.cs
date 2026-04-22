using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData : MonoBehaviour
{
    // shelf data
    public List<BookInfo> Books = new List<BookInfo>();
    
    [Serializable]
    public struct BookInfo
    {
        public string bookID;
        public string spritesID;
        public bool coverView;
        public string shelfParentID;
        public int parentsOrder;
        public bool onShelf;
    }

    // counter data
    public List<string> metCustomersID = new List<string>();

    // other game data
    public int musicVolume;
    public int customerRating;
    public int lastDayCompleted;

}
