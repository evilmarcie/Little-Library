using UnityEngine;
using UnityEngine.EventSystems;

public class TriggerTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject popup;
    GameObject popupParent;
    BookData currentBook;
    GameObject trigger;

    void Awake()
    {
        popupParent = GameObject.Find("PopupParent");
        popup = popupParent.transform.GetChild(0).gameObject;
        PlayableBook book = gameObject.GetComponent<PlayableBook>();
        Tooltip tooltip = popup.GetComponent<Tooltip>();
    }

    public bool pointerHover = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        trigger = this.gameObject;
        currentBook = gameObject.GetComponent<bookPrefab>().book;
        Tooltip.tooltipActive_Static(currentBook, trigger);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        Tooltip.tooltipInactive_Static();
        
    }
}