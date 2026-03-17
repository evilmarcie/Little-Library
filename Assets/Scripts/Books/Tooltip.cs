using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public Image background;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI authorText;
    public TextMeshProUGUI genreText;

    public BookData currentBook;

    private void Awake()
    {
        RectTransform backgroundRect =  background.GetComponent<RectTransform>();
    }

    public void TooltipActive()
    {
        titleText.text = currentBook.bookTitle;
        authorText.text = currentBook.authorName;
        genreText.text = currentBook.BookGenre.ToString();
    }

}
