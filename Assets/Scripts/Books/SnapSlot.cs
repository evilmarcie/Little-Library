using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SnapSlot : MonoBehaviour, IDropHandler
{

    public float shelfHolding = 0;
    float maxCapacty = 4;

    public TextMeshProUGUI shelfFullText;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        PlayableBook book = dropped.GetComponent<PlayableBook>();
        book.myShelf = this;
        shelfHolding = 0;
        
        foreach(PlayableBook child in gameObject.GetComponentsInChildren<PlayableBook>())
        {
            if (child.coverActiveView == true)
            {
                shelfHolding += 1;
            }
            else if (child.coverActiveView == false)
            {
                shelfHolding += 0.5f;
            }
        }
        
        float booksize = 0;

        if(book.coverActiveView == true)
        {
             booksize = 1;
        }
        else if(book.coverActiveView == false)
        {
             booksize = 0.5f;
        }

        if ((shelfHolding + booksize) <= maxCapacty)
        {
            book.parentAfterDrag = transform;
            book.onShelf = true;
        }
        else
        {
            Instantiate(shelfFullText, book.transform);     
        }

    }

    public void BookRemoved(PlayableBook book)
    {    
    
    }
}
