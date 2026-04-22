
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Holder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler, ISaveShelves
{
    public GameObject container;
    public GameObject dropText;
    public Animator holderAnimator;

    public static Holder instance;
    Vector3 startPos;

    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
        startPos = transform.localPosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.SetActive(false);

        if (beforeState == false) //if cover view was inactive before hover
        {
            bookController.SpineActive();
        }

    }

    public bool beforeState;
    GameObject hoveredBook;
    PlayableBook bookController;
    

     public void OnPointerEnter(PointerEventData eventData)
    {
        hoveredBook = eventData.pointerDrag;
        bookController = hoveredBook.GetComponent<PlayableBook>();
        beforeState = bookController.coverActiveView;
        if (beforeState == false) //if cover view inactive
        {
            bookController.CoverActive();
        }
    }

    public GameObject dropped;

    public void OnDrop(PointerEventData eventData)
    {
        dropText.SetActive(false);
        dropped = eventData.pointerDrag;
        PlayableBook Book = dropped.GetComponent<PlayableBook>();
        Book.onShelf = false;
        Book.parentAfterDrag = container.transform;
        holderAnimator.SetTrigger("triggerExit");
        triggerGiveBook = true;
        uiManager.instance.toCounter();
        Destroy(dropped);
        
        
        transform.localPosition = startPos;
    }

    bool triggerGiveBook = false;

    public void SaveShelves(ref BookshelvesData bookshelvesData)
    {
        if (dropped != null)
        {
            bookshelvesData.RecommendedBook.bookID = dropped.GetComponent<bookPrefab>().bookID;
            bookshelvesData.RecommendedBook.spritesID = dropped.GetComponent<bookPrefab>().spriteID;
        }
        bookshelvesData.triggerGiveBook = triggerGiveBook;
    }

    public void LoadShelves(BookshelvesData bookshelvesData)
    {
        
    }
}
