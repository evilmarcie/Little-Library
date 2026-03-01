using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class uiBook : MonoBehaviour

{
    public BookData book;
    public Sprite cover;

     private Button button;

    public Image coverDisplay;
    public TextMeshProUGUI title;
    public TextMeshProUGUI author;

    // menus
    public GameObject homeMenu;
    public GameObject infoMenu;

     // book display (info menu)
    public Image infoCover;
    public TextMeshProUGUI infoTitle;
    public TextMeshProUGUI infoAuthor;

    //book info
    public TextMeshProUGUI titleHeader;
    public TextMeshProUGUI authorInfo;
    public TextMeshProUGUI publishedInfo;
    public TextMeshProUGUI genreInfo;
    public TextMeshProUGUI descInfo;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    void Start()
    {
        coverDisplay.sprite = cover;
        title.text = book.bookTitle;
        author.text = book.authorName;
        gameObject.name = book.bookTitle.ToString() + "button";
    }

    void OnButtonClicked()
    {
        homeMenu.SetActive(false);
        infoMenu.SetActive(true);
        bookInfo();
    }

      public void bookInfo()
    {
        //book display
        infoCover.sprite = cover;
        infoTitle.text = book.bookTitle;
        infoAuthor.text = book.authorName;

        //info
        titleHeader.text = book.bookTitle;
        authorInfo.text = "Author: " + book.authorName;
        publishedInfo.text = "Published: " + book.publicationYear;
        genreInfo.text = "Genre: " + book.BookGenre;
        descInfo.text = "Description: " + book.bookBio; 
        
    }
}

