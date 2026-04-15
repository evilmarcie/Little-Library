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

    IEnumerator Start()
    {
        while ((CounterManager.instance.CounterLoaded == false) && (uiManager.instance.uiLoaded == false))
        {
            yield return new WaitForEndOfFrame();
        }
        DayStart();
    }

    public int day = 1;

    // trigger on counter manager load
    public void DayStart()
    {
        uiManager.instance.UpdateDayUI(day);

        if (day == 1)
        {
            Debug.Log("tutorial");
            CounterManager.instance.customerEnter();
        }
        else
        {
            // trigger book box delivery, cx arrive once book box is empty
            CounterManager.instance.customerEnter();
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
}
