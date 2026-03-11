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

        booksScrollbar.value = 1;
    }
}
