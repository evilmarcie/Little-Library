using UnityEngine;
using UnityEngine.EventSystems;

public class dialogueController : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        CounterManager.instance.NextDialogue();
    }
}
