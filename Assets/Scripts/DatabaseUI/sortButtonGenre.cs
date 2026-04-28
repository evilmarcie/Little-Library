using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class sortButtonGenre : MonoBehaviour
{
    public bookGenre buttonGenre; 
    private Button button;
    public GameObject content;
    public Button[] books;
    public Scrollbar booksScrollbar;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        books = content.GetComponentsInChildren<Button>();

        foreach (Button bookButton in books)
        {
            if (bookButton.GetComponent<uiBook>() != null)
            {
                bookGenre genre = bookButton.GetComponent<uiBook>().book.BookGenre;
                if(genre.HasFlag(buttonGenre) == false)
                {
                    bookButton.gameObject.SetActive(false);
                }
            }
        }

        BooksMenu booksMenu = GetComponentInParent<BooksMenu>();
        if (booksMenu != null)
        {
            foreach (GameObject button in booksMenu.genreButtons)
            {
                bookGenre buttonsgenre = button.GetComponent<sortButtonGenre>().buttonGenre;
                if (buttonsgenre == buttonGenre)
                {
                    
                }
                else
                {
                    button.SetActive(false);
                }
            }    
        }
        else
        {
            Debug.Log("cannot find parent");
        }
        

        booksScrollbar.value = 1;
    }
}
