using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEditor;

public class TriggerTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject popup;
    GameObject popupParent;
    BookData currentBook;

    void Awake()
    {
        popupParent = GameObject.Find("PopupParent");
        popup = popupParent.transform.GetChild(0).gameObject;
        Tooltip tooltip = popup.GetComponent<Tooltip>();
        PlayableBook book = gameObject.GetComponent<PlayableBook>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        popup.SetActive(true);

        currentBook = gameObject.GetComponent<bookPrefab>().book;
        Tooltip.updateTooltip_Static(currentBook);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        popup.SetActive(false);
    }
}
