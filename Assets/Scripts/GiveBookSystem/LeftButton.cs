using UnityEngine;
using UnityEngine.EventSystems;
public class LeftButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public GameObject holder;

    public void OnPointerClick(PointerEventData eventData)
    {
        uiManager.instance.toCounter();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (BookshelfManager.instance.holdingBook == true) //& customer interaction stage
        {
            holder.SetActive(true);
            // trigger animation
        }
    }
}
