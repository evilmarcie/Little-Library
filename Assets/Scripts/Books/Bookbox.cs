using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bookbox : MonoBehaviour, IPointerClickHandler
{
    public static Bookbox instance;
    public bool loaded = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (SessionManager.instance.bookBoxToday == false)
        {
            GenerateBookBox();
        }
    }

    public GameObject bookPrefab;
    public GameObject boxHolder;

    private int bookboxMaxCapacity = 5;
    // so able to be changed later when there is multiple box sizes

    public GameObject boxParent;

    public int bookCount;

    void Update()
    {
        if (SessionManager.instance.currentDayStage == SessionManager.DayStage.unpackDelivery)
        {
            if (boxHolder.transform.childCount == 0)
            {
                Debug.Log("zero children");
                boxParent.SetActive(false); 
                uiManager.instance.CustomerNotification(true);
                SessionManager.instance.completeBookBox = true;
                SessionManager.instance.currentDayStage = SessionManager.DayStage.cxArrive;
                Destroy(boxParent);
            } 
        }
    }

    public List<BookData> childBooks;

    public void GenerateBookBox()
    {
        List<BookData> childBooks = new List<BookData>();
        if (childBooks.Count != 0){childBooks.Clear();}

        for (int booksGenerated = 0; booksGenerated < bookboxMaxCapacity; booksGenerated++)
        {
            GameObject thisBook = null;
            BookData thisBookData = null;
           
            do
            {
                if (thisBook!=null){Destroy(thisBook);}
                thisBook = Instantiate(bookPrefab, boxHolder.transform);
                thisBook.GetComponent<bookPrefab>().RandomValues();
                thisBookData = thisBook.GetComponent<bookPrefab>().book;
            }
            while(childBooks.Contains(thisBookData));
                
            childBooks.Add(thisBookData);
        }

        if (boxHolder.transform.childCount > 0)
        {
            boxHolder.SetActive(false);
            SessionManager.instance.bookBoxToday = true;
            SessionManager.instance.currentDayStage = SessionManager.DayStage.unpackDelivery;
        }
    }

    public bool boxOpen = false;
    public Animator boxAnimator;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (boxOpen == false)
        {
            //boxAnimator.ResetTrigger("closeBox");
            boxAnimator.SetTrigger("openBox");
            boxHolder.SetActive(true);
            boxOpen = true;
        }
        else if (boxOpen == true)
        {
            //boxAnimator.ResetTrigger("openBox");
            boxAnimator.SetTrigger("closeBox");
            boxHolder.SetActive(false);
            boxOpen = false;
        }
    }
}
