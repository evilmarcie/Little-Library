using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SessionManager : MonoBehaviour, ISaveGame
{

    #region instance
    public static SessionManager instance;

    void Awake()
    {
        instance = this;
        sessionLoaded = true;
    }
    #endregion

    public int day = 1;
    public bool bookBoxToday = false;
    public bool completeBookBox = false;
    bool sessionLoaded = false;

    IEnumerator Start()
    {
        while (sessionLoaded == false)
        {
            yield return new WaitForEndOfFrame();
        }
        DayStart();
    }

    // trigger on counter manager load
    public void DayStart()
    {
        Debug.Log("day start trigger");
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

    public void SaveGame(ref GameData gameData)
    {
        
        gameData.lastDayCompleted = day;   
        gameData.customerRating = currentRating;

    }

    public void LoadGame(GameData gameData)
    {
        currentRating = gameData.customerRating;
        day = gameData.lastDayCompleted;
    }

    public enum DayStage { delivery, unpackDelivery, cxArrive, inDialogue, pickBooks, EOD }
    public DayStage currentDayStage;

}
