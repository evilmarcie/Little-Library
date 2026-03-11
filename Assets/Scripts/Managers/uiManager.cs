using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    public GameObject databaseMenu;
    public GameObject pauseMenu;
    public bool gamePaused = false;

    public GameObject homeMenu;
    public GameObject infoMenu;
    public Scrollbar booksScrollbar;
    public Scrollbar achievementsScrollbar;

    void Start()
    {
        databaseMenu.SetActive(false);

        if (gamePaused == true)
        {
            //buttons and other features unable to be interacted with
        }
        
    }

    public void OpenDatabase()
    {
        databaseMenu.SetActive(true);
        databaseMenu.GetComponent<DatabaseUI>().OpenBooksMenu();
        resetDatabase();

    }

    public void CloseDatabase()
    {
        databaseMenu.SetActive(false);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        gamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        gamePaused = false;
    }

     void resetDatabase()
    {
        homeMenu.SetActive(true);
        infoMenu.SetActive(false);
        booksScrollbar.value = 1;
        achievementsScrollbar.value = 1;
    }

    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    public void toShelves()
    {
        // Scene Bookshelves = SceneManager.GetSceneByName("Bookshelves");
        // SceneManager.SetActiveScene(Bookshelves);

        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.Bookshelves, SceneDatabase.Scenes.Bookshelves, SetActive: true)
            .Load(SceneDatabase.Slots.UI, SceneDatabase.Scenes.UI)
            .WithOverlay()
            .Perform();
    }

    public void toCounter()
    {
        //Scene Counter = SceneManager.GetSceneByName("Counter");
        //SceneManager.SetActiveScene(Counter);

         SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.Counter, SceneDatabase.Scenes.Counter, SetActive: true)
            .Load(SceneDatabase.Slots.UI, SceneDatabase.Scenes.UI)
            .WithOverlay()
            .Perform();
    }

}
