using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEditor;


public class PlayableBook : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Transform shelfParent;
    public Image CoverImage;
    public Image SpineImage;
    public GameObject coverView;
    public GameObject spineView;

    public SnapSlot myShelf;
    public bool onShelf = false;
    public BookshelfManager BookshelfManager;

    public uiManager UImanager;

    void Awake()
    {
        coverView.SetActive(true);
        spineView.SetActive(false);

        BookshelfManager = FindFirstObjectByType<BookshelfManager>();
        
    }

    void Start()
    {
        UImanager = uiManager.UImanager;
    }

    // put onto shelf 

    public void OnBeginDrag(PointerEventData eventData)
    
    {
        onShelf = false;
        BookshelfManager.holdingBook = true;

        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        CoverImage.raycastTarget = false;
        SpineImage.raycastTarget = false;

        if(myShelf != null)
        {
            myShelf.BookRemoved(this);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);

        CoverImage.raycastTarget = true;
        SpineImage.raycastTarget = true;

        BookshelfManager.holdingBook = false;

    }

    // toggle cover to spine view

    bool PointerDown = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown = true;
     
    } 
    public void OnPointerUp(PointerEventData eventData)
    {
        PointerDown = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            Debug.Log("doubleclicked");
            bookPrefab thisBook = gameObject.GetComponent<bookPrefab>();
            BookData book = thisBook.book;
            Sprite cover = thisBook.bookCover;
            UImanager.setBookInfo(book, cover);
        }
    }

    void Update()
    {
        if (PointerDown == true && Input.GetKeyDown(KeyCode.R))
        {
            RectTransform rt = GetComponent<RectTransform>();

            if (coverView.activeSelf)
            {
                coverView.SetActive(false);
                spineView.SetActive(true);
                rt.sizeDelta = new Vector2(83.74f, 276);
            }
            else
            {
                coverView.SetActive(true);
                spineView.SetActive(false);
                rt.sizeDelta = new Vector2(192, 276);
            }
        }

    }
}
