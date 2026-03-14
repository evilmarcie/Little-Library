using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.ComponentModel.Design;
using UnityEditor.Rendering;
using Unity.VisualScripting;

public class PlayableBook : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    public Image image;
    public GameObject coverView;
    public GameObject spineView;

    void Awake()
    {
        coverView.SetActive(true);
        spineView.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    bool PointerDown = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown = true;
     
    } 
    public void OnPointerUp(PointerEventData eventData)
    {
        PointerDown = false;
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
