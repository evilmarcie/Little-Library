using System;
using System.Collections.Generic;

[Serializable]
public class BookshelvesData
{
    public List<BookInfo> Books = new List<BookInfo>();
    public BookInfo RecommendedBook;
    public bool triggerGiveBook = false;

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
}
