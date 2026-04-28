using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using System.Collections.Generic;

public class Tooltip : MonoBehaviour
{    
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI authorText;
    
    public static Tooltip instance;
    GameObject me;

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

        me = this.gameObject;

    } 

    void Update()
    {
        transform.position = Input.mousePosition + new Vector3(50, -50, 0);

        books = GameObject.FindGameObjectsWithTag("book");
    }


    public void tooltipActive(BookData currentBook, GameObject trigger)
    {
        PlayableBook triggerBook = trigger.GetComponent<PlayableBook>();
        
        if((triggerBook.onShelf == true) && (BookshelfManager.instance.holdingBook == false))
        {
            titleText.text = currentBook.bookTitle;
            authorText.text = "By " + currentBook.authorName;
            me.SetActive(true);
        }
    
    }

    public GameObject[] books;

    public void tooltipInactive()
    {
        foreach (GameObject book in books)
        {
            if (book != null)
            {
                TriggerTooltip checkHover = book.GetComponent<TriggerTooltip>();
                if (checkHover.pointerHover == false)
                {
                    me.SetActive(false);
                }
            }
        }
    
    }

    public static void tooltipActive_Static(BookData currentBook,  GameObject trigger)
    {
        instance.tooltipActive(currentBook, trigger);
    }

    public static void tooltipInactive_Static()
    {
        instance.tooltipInactive();
    }
}
