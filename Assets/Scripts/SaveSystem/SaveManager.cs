using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VectorGraphics;

public class SaveManager : MonoBehaviour
{

    [Header("File Storage Config")]
    private string fileName = "littlelibrary.savedata";
    private FileDataHandler dataHandler;
    public static SaveManager instance { get; private set; }
    private GameData gameData;
    private List<ISaveData> saveDataObjects;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("save manager error");
        }
        instance = this;
    }

    void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.saveDataObjects = FindAllSaveDataObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        // trigger tutorial?
    }

    public void LoadGame()
    {

        Debug.Log("load");

        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("no save data found");
            NewGame();
        }
        
        foreach (ISaveData saveDataObj in saveDataObjects)
        {
            saveDataObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        Debug.Log("save");

        foreach (ISaveData saveDataObj in saveDataObjects)
        {
            saveDataObj.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }

    private List<ISaveData> FindAllSaveDataObjects()
    {
        IEnumerable<ISaveData> saveDataObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveData>();
        return new List<ISaveData>(saveDataObjects);
    }
}
