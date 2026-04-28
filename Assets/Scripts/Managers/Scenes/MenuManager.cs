using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance; 

    void Awake()
    {
        instance = this;
        saveSlots = loadMenu.GetComponentsInChildren<SaveSlot>();
        loadMenu.SetActive(false);
        //SaveManager.instance.LoadGame();
    }

    public int loadDay;

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

            if (creditsMenu.activeSelf == true)
            {
                creditsMenu.SetActive(false);
            }
        }
    }

    public void SettingsMenu()
    {
        uiManager.instance.OpenSettings();
    }

    public GameObject loadMenu;

    private SaveSlot[] saveSlots;

    public void LoadMenu()
    {
        Dictionary<string, GameData> profilesGameData = SaveManager.instance.LoadAllProfiles();

        foreach (SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
        }

        loadMenu.SetActive(true);
    }
    public GameObject creditsMenu;

    public void OpenCreditsMenu()
    {
        creditsMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
