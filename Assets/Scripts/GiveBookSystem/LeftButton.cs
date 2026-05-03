using UnityEngine;
using UnityEngine.EventSystems;
public class LeftButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        uiManager.instance.toCounter();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if ((BookshelfManager.instance.holdingBook == true) 
        && (SessionManager.instance.currentDayStage == SessionManager.DayStage.pickBooks)
        && (Holder.instance!=null))
        {
            Holder.instance.ActivateHolder();
        }
    }
}
