using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    void Awake()
    {
        loadMenu.SetActive(false);
    }

    public void StartGame()
    {
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.Session, SceneDatabase.Scenes.Session)
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.Counter, SetActive: true)
            .Load(SceneDatabase.Slots.UI, SceneDatabase.Scenes.UI)
            .Unload(SceneDatabase.Slots.Menu)
            .WithOverlay()
            .WithClearUnusedAssets()
            .Perform();

        uiManager.instance.overlayUI.SetActive(true);
        uiManager.instance.gameStarted = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (loadMenu.activeSelf == true)
            {
                loadMenu.SetActive(false);
            }
        }
    }

    public GameObject settingsMenu;

    public void SettingsMenu()
    {
        uiManager.instance.OpenSettings();
    }

    public GameObject loadMenu;

    public void LoadMenu()
    {
        loadMenu.SetActive(true);
    }

    public void LoadGameButton()
    {
        StartGame();
    }

    public void NewGameButton()
    {
        StartGame();
        // new save file
    }
}
