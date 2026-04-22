using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    void Awake()
    {
        instance = this;
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
        
        string json = JsonUtility.ToJson(shelvesData, true);
        File.WriteAllText(Application.persistentDataPath + "BookshelfData", json);
        
    }

    public GameObject bookPrefab;

    public void LoadBookshelves()   
    {
       string path = Application.persistentDataPath + "BookshelfData";

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
        
        string json = JsonUtility.ToJson(counterData, true);
        File.WriteAllText(Application.persistentDataPath + "CounterData", json);
    }

    public void LoadCounter()
    {
        //get shelf data
        string shelfPath = Application.persistentDataPath + "BookshelfData";
        if (!File.Exists(shelfPath)) {Debug.Log("no bookshelfdata"); return;}
        Debug.Log("found shelfdata");
        string jsonShelf = File.ReadAllText(shelfPath);
        BookshelvesData shelvesData = JsonUtility.FromJson<BookshelvesData>(jsonShelf);

        //get counter data
        string path = Application.persistentDataPath + "CounterData";
        if (!File.Exists(path)) {Debug.Log("no counterdata"); return;}
        Debug.Log("found counterdata");
        string json = File.ReadAllText(path);
        CounterData counterData = JsonUtility.FromJson<CounterData>(json);

        //find counterdata objects to load
        CounterDataObjects = FindAllSaveCounterObj();
        if (CounterDataObjects.Count == 0){Debug.Log("cannot find counter data objects");};
                                
        foreach (ISaveCounter counterDataObj in CounterDataObjects)
        {
            Debug.Log("execute loading");
            counterData.givenBookID = shelvesData.RecommendedBook.bookID;
            counterData.triggerGiveBook = shelvesData.triggerGiveBook;
            counterDataObj.LoadCounter(counterData);
        }
        
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
        GameDataObjects = FindAllGameDataObjects();

        GameData gameData = new GameData();

        foreach (ISaveGame gameDataObj in GameDataObjects)
        {
            gameDataObj.SaveGame(ref gameData);
        }
        
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(Application.persistentDataPath + "Game Save", json);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "Game Save";
        if (!File.Exists(path)) {Debug.Log("no game save"); return;}
        Debug.Log("found game save");
        string json = File.ReadAllText(path);

        GameData gameData = JsonUtility.FromJson<GameData>(json);

        GameDataObjects = FindAllGameDataObjects();
        if (GameDataObjects.Count == 0){Debug.Log("cannot find game data objects");};
                                
        foreach (ISaveGame gameDataObj in GameDataObjects)
        {
            gameDataObj.LoadGame(gameData);
        }

        // write new counter data from game data

        CounterData counterData = new CounterData();
        counterData.metCustomersID = gameData.metCustomersID;

        string jsonCounter = JsonUtility.ToJson(counterData, true);
        File.WriteAllText(Application.persistentDataPath + "CounterData", jsonCounter);

        // write new bookshelf data from game data

        BookshelvesData shelvesData = new BookshelvesData();
        BookshelvesData.BookInfo info = new BookshelvesData.BookInfo();
        
        foreach(GameData.BookInfo books in gameData.Books)
        {
            info.bookID = books.bookID;
            info.coverView = books.coverView;
            info.onShelf = books.onShelf;
            info.parentsOrder = books.parentsOrder;
            info.shelfParentID = books.shelfParentID;
            info.spritesID = books.spritesID;
            shelvesData.Books.Add(info);
        }
        
        string jsonShelves = JsonUtility.ToJson(shelvesData, true);
        File.WriteAllText(Application.persistentDataPath + "BookshelfData", jsonShelves);
    }
    #endregion
}   
