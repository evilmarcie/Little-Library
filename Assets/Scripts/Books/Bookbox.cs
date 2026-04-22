using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Bookbox : MonoBehaviour, IPointerClickHandler
{
    public static Bookbox instance;

    void Awake()
    {
        instance = this;
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
                boxParent.SetActive(false); // ideally add animate out, temp just disappear
                uiManager.instance.CustomerNotification(true);
                SessionManager.instance.completeBookBox = true;
                SessionManager.instance.currentDayStage = SessionManager.DayStage.cxArrive;
            } 
        }
    }

    public void GenerateBookBox()
    {
        boxParent.SetActive(true);

        for (int booksGenerated = 0; booksGenerated < bookboxMaxCapacity; booksGenerated++)
        {
            Instantiate(bookPrefab, boxHolder.transform);
        }
        SessionManager.instance.bookBoxToday = true;

        SessionManager.instance.currentDayStage = SessionManager.DayStage.unpackDelivery;
       
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
