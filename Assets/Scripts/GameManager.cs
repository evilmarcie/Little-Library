using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject databaseMenu;
    public GameObject pauseMenu;
    public bool gamePaused = false;

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

}
