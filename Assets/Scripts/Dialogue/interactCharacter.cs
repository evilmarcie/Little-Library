using UnityEngine;
using UnityEngine.EventSystems;

public class interactCharacter : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if ((CounterManager.instance.currentStage == CounterManager.DialogueStage.Prompt) 
        && (CounterManager.instance.dialogueUI.activeSelf == false))
        {
            CounterManager.instance.RepeatPrompt();
        }
    }
}
