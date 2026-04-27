using UnityEngine;
using UnityEngine.EventSystems;

public class dialogueController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
{
    public static dialogueController instance;

    void Awake()
    {
        instance = this;
    }

    public bool pointerDown = false;

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
        Debug.Log("pointer down");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CounterManager.instance.NextDialogue();
        pointerDown = true;
        Debug.Log("pointer up");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
