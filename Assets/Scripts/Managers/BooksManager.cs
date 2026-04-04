using System.Collections.Generic;
using UnityEngine;

public class BooksManager : MonoBehaviour
{
    public static BooksManager Instance;
    void OnEnable()
    {
        Instance = this;
    }

    public List<BookData> allBooks;
   private Dictionary<string, BookData> lookup;

    void Awake()
    {
        lookup = new Dictionary<string, BookData>();
        foreach(var book in allBooks)
        {
            lookup [ book.bookID ] = book;
        }
    }

    public BookData GetByID(string id)
    {
        return lookup [id];
    }
}
