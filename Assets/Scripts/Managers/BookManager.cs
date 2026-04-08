using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
     public static BookManager instance;

    public List<BookData> allBooks;
    private Dictionary<string, BookData> lookup;

    void Awake()
    {
        instance = this;
        
        lookup = new Dictionary<string, BookData>();

        foreach(var book in allBooks)
        {
            lookup [book.bookID] = book;
        }
    }

    public BookData GetBookData(string id)
    {
        return lookup [id];
    }
}
