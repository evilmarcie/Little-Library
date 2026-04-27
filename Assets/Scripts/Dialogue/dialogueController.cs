using UnityEngine;
using UnityEngine.EventSystems;

public class dialogueController : MonoBehaviour, IPointerClickHandler
{
    public static dialogueController instance;

    void Awake()
    {
        instance = this;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        CounterManager.instance.NextDialogue();
    }
}
