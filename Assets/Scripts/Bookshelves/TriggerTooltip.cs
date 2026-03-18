using UnityEngine;
using UnityEngine.EventSystems;

public class TriggerTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip;
    [HideInInspector] 
public BookData currentBook;
    bool pointerEnter = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerEnter = false;
    }

    void Update()
    {
        tooltip.GetComponent<Tooltip>();

        if (pointerEnter == true)
        {
            Debug.Log("enter");
            //Physics2D.queriesHitTriggers = true;
            //currentBook = gameObject.GetComponent<bookPrefab>().book;
            Tooltip.ShowTooltip_Static();
        }
        else
        {
            Tooltip.HideTooltip_Static();
        }
    }
}
