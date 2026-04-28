using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        GameData data = SaveManager.instance.ReadGameData(SaveManager.instance.profileID);
        if (data!=null)
        {
            day = data.lastDayCompleted+1;
            currentRating = data.customerRating;
        }
        StartCoroutine(DayStart());
    }

    // trigger on counter manager load
    public IEnumerator DayStart()
    {
        Debug.Log("trigger day start");
        while ((CounterManager.instance == null) && (uiManager.instance == null) 
        && (CounterManager.instance.CounterLoaded == false) && uiManager.instance.uiLoaded == false)
        {
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("managers active");
        
    }

    public void ResetDay()
    {
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.Counter, SetActive: true)
            .Load(SceneDatabase.Slots.UI, SceneDatabase.Scenes.UI)
            .WithOverlay()
            .WithClearUnusedAssets()
            .Perform();
        
        bookBoxToday = false;
        completeBookBox = false;
        currentDayStage = DayStage.delivery;
        day += 1;
        StartCoroutine(DayStart());
  
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
        
    }

    public enum DayStage { delivery, unpackDelivery, cxArrive, inDialogue, pickBooks, EOD }
    public DayStage currentDayStage;

}
