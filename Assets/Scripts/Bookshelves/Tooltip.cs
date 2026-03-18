using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem.Android;

public class Tooltip : MonoBehaviour
{    
    public TextMeshProUGUI titleText;
    
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

    public void updateTooltip(BookData currentBook)
    {
        titleText.text = currentBook.bookTitle;
    }

    public static void updateTooltip_Static(BookData currentBook)
    {
        instance.updateTooltip(currentBook);
    }
}
