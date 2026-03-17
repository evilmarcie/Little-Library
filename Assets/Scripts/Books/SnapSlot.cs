using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class SnapSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        PlayableBook book = dropped.GetComponent<PlayableBook>();
        
        if (transform.childCount < 4)
        {
             book.parentAfterDrag = transform;
        }
        else
        {
            
            Transform originalParent = book.parentAfterDrag;
            GameObject current = transform.GetChild(0).gameObject;
            PlayableBook currentBook = current.GetComponent<PlayableBook>();

            currentBook.transform.SetParent(originalParent);
            
        }

    }
}
