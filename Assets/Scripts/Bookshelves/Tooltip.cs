using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using System.Diagnostics.Tracing;

public class Tooltip : MonoBehaviour
{    
    public Image background;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI authorText;
    public TextMeshProUGUI genreText;

    public static Tooltip instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        gameObject.SetActive(false);

    } 

    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void ShowTooltip()
    {
        //TooltipInfo(currentBook);
        gameObject.SetActive(true);
    }

    public void TooltipInfo()
    {
        //titleText.text = currentBook.bookTitle;
        //authorText.text = currentBook.authorName;
        //genreText.text = currentBook.BookGenre.ToString();
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static()
    {
        instance.ShowTooltip();
    }

    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }

}
