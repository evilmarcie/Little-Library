using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    string gameFileName = "GameData.json";
    string counterFileName = "CounterData.json";
    string bookshelfFileName = "BookshelfData.json";


    public static SaveManager instance;
    void Awake()
    {
        instance = this;
    }

    public string profileID;

    public string ProfilePath(string profileID)
    {
        string profilePath = Path.Combine(Application.persistentDataPath, fileID(profileID));
        Directory.CreateDirectory(profilePath);
        Debug.Log("directory exists");
        return profilePath;
    }

    #region bookshelves
    private List<ISaveShelves> bookshelfDataObjects;

    private List<ISaveShelves> FindAllSaveShelvesObj()
    {
        IEnumerable<ISaveShelves> saveShelvesObj = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveShelves>();
        return new List<ISaveShelves>(saveShelvesObj);
    }

    public void SaveBookshelves()
    {

        bookshelfDataObjects = FindAllSaveShelvesObj();

        BookshelvesData shelvesData = new BookshelvesData();

        foreach (ISaveShelves shelfDataObj in bookshelfDataObjects)
        {
            shelfDataObj.SaveShelves(ref shelvesData);
        }
        
        string profilePath = ProfilePath(profileID);
        try
        {
            string path = Path.Combine(profilePath, bookshelfFileName);
            //Directory.CreateDirectory(path);
            string json = JsonUtility.ToJson(shelvesData, true);
            File.WriteAllText(path, json);
            Debug.Log("saved shelves");
        }
        catch
        {
            Debug.LogError("Error saving new file");
        }
        
    }

    public GameObject bookPrefab;

    public void LoadBookshelves()
    {
        string path = Path.Combine(Application.persistentDataPath, fileID(profileID), bookshelfFileName);

        if (!File.Exists(path)) return;

        string json = File.ReadAllText(path);
        BookshelvesData shelvesData = JsonUtility.FromJson<BookshelvesData>(json);

        bookshelfDataObjects = FindAllSaveShelvesObj();

        foreach (ISaveShelves shelfDataObj in bookshelfDataObjects)
        {
            shelfDataObj.LoadShelves(shelvesData);
        }

        foreach (var info in shelvesData.Books)
        {
            if (info.onShelf == true)
            {
                GameObject book = Instantiate(bookPrefab);
                bookPrefab bookScript = book.GetComponent<bookPrefab>();
                bookScript.loadingFromSave = true;
                PlayableBook playableBook = book.GetComponent<PlayableBook>();

                bookScript.spriteInfo = SpriteManager.instance.GetSpriteInfo(info.spritesID);
                bookScript.book = BookManager.instance.GetBookData(info.bookID);
                bookScript.SetValues();

                if (info.coverView == true)
                {
                    playableBook.CoverActive();
                }
                else
                {
                    playableBook.SpineActive();
                }

                var shelf = ShelvesManager.instance.GetShelf(info.shelfParentID);

                if (shelf == null)
                {
                    Debug.Log("cant locate shelf");
                    return;
                }
                else
                {
                    book.transform.SetParent(shelf.transform);
                    book.transform.SetSiblingIndex(info.parentsOrder);
                }

                if (info.onShelf == true)
                {
                    playableBook.onShelf = true;
                }
            }
        }
    }

    #endregion

    #region counter
    private List<ISaveCounter> CounterDataObjects;

    private List<ISaveCounter> FindAllSaveCounterObj()
    {
        IEnumerable<ISaveCounter> saveCounterObj = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveCounter>();
        return new List<ISaveCounter>(saveCounterObj);
    }

    public void SaveCounter()
    {

        CounterDataObjects = FindAllSaveCounterObj();

        CounterData counterData = new CounterData();

        foreach (ISaveCounter counterDataObj in CounterDataObjects)
        {
            counterDataObj.SaveCounter(ref counterData);
        }

        string profilePath = ProfilePath(profileID);
        try
        {
            string path = Path.Combine(profilePath, counterFileName);
            string json = JsonUtility.ToJson(counterData, true);
            File.WriteAllText(path, json);
            Debug.Log("save counter");
        }
        catch(Exception e)
        {
            Debug.LogError("Error saving new file" + e);
        }
    }

    public void LoadCounter()
    {
        //get shelf data
        string shelfPath = Path.Combine(Application.persistentDataPath, fileID(profileID), bookshelfFileName);
        if (!File.Exists(shelfPath)) { Debug.Log("no bookshelfdata"); return; }
        Debug.Log("found shelfdata");
        string jsonShelf = File.ReadAllText(shelfPath);
        BookshelvesData shelvesData = JsonUtility.FromJson<BookshelvesData>(jsonShelf);

        //get counter data
        string path = Path.Combine(Application.persistentDataPath, fileID(profileID), counterFileName);
        if (!File.Exists(path)) { Debug.Log("no counterdata"); return; }
        Debug.Log("found counterdata");
        string json = File.ReadAllText(path);
        CounterData counterData = JsonUtility.FromJson<CounterData>(json);

        //find counterdata objects to load
        CounterDataObjects = FindAllSaveCounterObj();
        if (CounterDataObjects.Count == 0) { Debug.Log("cannot find counter data objects"); }

        foreach (ISaveCounter counterDataObj in CounterDataObjects)
        {
            counterData.givenBookID = shelvesData.RecommendedBook.bookID;
            counterData.triggerGiveBook = shelvesData.triggerGiveBook;
            counterDataObj.LoadCounter(counterData);
        }

        Debug.Log("load counterdata");
    }

    #endregion

    #region game
    private List<ISaveGame> GameDataObjects;

    private List<ISaveGame> FindAllGameDataObjects()
    {
        IEnumerable<ISaveGame> GameDataObj = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveGame>();
        return new List<ISaveGame>(GameDataObj);
    }
    public void SaveGame()
    {
        string profilePath = ProfilePath(profileID);

        GameData gameData = new GameData();

        //get shelf data
        string shelfPath = Path.Combine(profilePath, bookshelfFileName);
        if (!File.Exists(shelfPath)) { Debug.Log("no bookshelfdata"); return; }
        Debug.Log("found shelfdata");
        string jsonShelf = File.ReadAllText(shelfPath);
        BookshelvesData shelvesData = JsonUtility.FromJson<BookshelvesData>(jsonShelf);

        // write books on shelf data to game data
        foreach (BookshelvesData.BookInfo book in shelvesData.Books)
        {
            GameData.BookInfo info = new GameData.BookInfo
            {
                bookID = book.bookID,
                coverView = book.coverView,
                onShelf = book.onShelf,
                parentsOrder = book.parentsOrder,
                shelfParentID = book.shelfParentID,
                spritesID = book.spritesID
            };
            gameData.Books.Add(info);
        }

        // regular game data
        GameDataObjects = FindAllGameDataObjects();

        foreach (ISaveGame gameDataObj in GameDataObjects)
        {
            gameDataObj.SaveGame(ref gameData);
        }

        // write new game data
        string path = Path.Combine(profilePath, gameFileName);
        try
        {
            //Directory.CreateDirectory(path);
            string json = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(path, json);
            Debug.Log("save");
        }
        catch
        {
            Debug.LogError("Error saving new file");
        }
    }

    public bool noGameSave;

    public void LoadGame()
    {
        GameData gameData = ReadGameData(profileID);

        GameDataObjects = FindAllGameDataObjects();
        if (GameDataObjects.Count == 0) { Debug.Log("cannot find game data objects"); }

        foreach (ISaveGame gameDataObj in GameDataObjects)
        {
            gameDataObj.LoadGame(gameData);
        }
        string profilePath = ProfilePath(profileID);

        // // write new counter data from game data
        // CounterData counterData = new CounterData();
        // foreach (string id in gameData.metCustomersID)
        // {
        //     string custID = id;
        //     counterData.metCustomersID.Add(custID);
        // }
        // string counterPath = Path.Combine(profilePath, counterFileName);
        
        // try
        // {
        //     string jsonCounter = JsonUtility.ToJson(counterData, true);
        //     File.WriteAllText(counterPath, jsonCounter);
        //     Debug.Log("updated counterdata");
        // }
        // catch
        // {
        //     Debug.LogError("Error saving new counterdata");
        // }

        // write new bookshelf data from game data
        BookshelvesData shelvesData = new BookshelvesData();
        BookshelvesData.BookInfo info = new BookshelvesData.BookInfo();

        foreach (GameData.BookInfo books in gameData.Books)
        {
            info.bookID = books.bookID;
            info.coverView = books.coverView;
            info.onShelf = books.onShelf;
            info.parentsOrder = books.parentsOrder;
            info.shelfParentID = books.shelfParentID;
            info.spritesID = books.spritesID;
            shelvesData.Books.Add(info);
        }
        shelvesData.triggerGiveBook = false;

        string shelfPath = Path.Combine(profilePath, bookshelfFileName);
        try
        {
            string jsonShelf = JsonUtility.ToJson(shelvesData, true);
            File.WriteAllText(shelfPath, jsonShelf);
            Debug.Log("updated shelfdata");
        }
        catch
        {
            Debug.LogError("Error saving new shelfdata");
        }
    }
    #endregion

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(Application.persistentDataPath).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileID = dirInfo.Name;
            string fullPath = Path.Combine(Application.persistentDataPath, fileID(profileID), gameFileName);
            if (!File.Exists(fullPath))
            {
                Debug.Log(profileID + " has no save data");
                continue;
            }
            GameData profileData = ReadGameData(fileID(profileID));
            profileDictionary.Add(profileID, profileData);
            Debug.Log(profileID + " has save data");
        }
        return profileDictionary;
    }

    public GameData ReadGameData(string profileID)
    {
        string path = Path.Combine(Application.persistentDataPath, fileID(profileID), gameFileName);
        if (!File.Exists(path)) { return null; }
        string json = File.ReadAllText(path);
        GameData thisData = JsonUtility.FromJson<GameData>(json);
        Debug.Log("read game data");
        return thisData;
    }

    public string fileID(string rawID)
    {
        return profileID = rawID + "/";
    }
}
