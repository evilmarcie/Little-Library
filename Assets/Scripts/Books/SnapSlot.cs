using System.Collections;
using Mono.Cecil.Cil;
using TMPro;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

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
        if (book.coverView.activeSelf)
        {
            shelfHolding += 1;
        }
        else
        {
            shelfHolding += 0.5f;
        }

        if (shelfHolding <= maxCapacty)
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

        if (book.coverView.activeSelf)
        {
            shelfHolding -= 1;
        }
        else
        {
            shelfHolding -= 0.5f;
        }
    
    }
}
