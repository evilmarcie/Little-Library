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

        uiManager = uiManager.instance;
    }

    public uiManager uiManager;
    
    void OnButtonClicked()
    {
        uiManager.setBookInfo(book, cover);
    }
}

