using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SessionManager : MonoBehaviour
{

    #region instance
    public static SessionManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public int day = 1;
    public bool bookBoxToday = false;
    public bool completeBookBox = false;

    // trigger on counter manager load
    public void DayStart()
    {
        uiManager.instance.UpdateDayUI(day);
        uiManager.instance.DeliveryNotification(true);

        if (day == 1)
        {
            Debug.Log("tutorial");
        }
    }

    public void ResetDay()
    {
        day += 1;
        // do like a load screen / save & reload to counter scene
        DayStart();
        CounterManager.instance.visitedToday.Clear();
    }

    public int currentRating = 100;

    // trigger after cx response
    public void updateCustomerSatisfaction(int adjustment)
    {
        currentRating += adjustment;
        
        if (currentRating > 100)
        {
            currentRating = 100;
        }

        uiManager.instance.UpdateRatingUI(currentRating);
    }

    public enum DayStage { delivery, unpackDelivery, cxArrive, inDialogue, pickBooks }
    public DayStage currentDayStage;

}
