using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

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
        File.WriteAllText(Application.persistentDataPath + "LittleLibrary_BookshelfData", json);

        
    }

    public GameObject bookPrefab;

    public void LoadBookshelves()
    
    {
        bookshelfDataObjects = FindAllSaveShelvesObj();

        string path = Application.persistentDataPath + "LittleLibrary_BookshelfData";

        if (!File.Exists(path)) return;

        string json = File.ReadAllText(path);
        BookshelvesData shelvesData = JsonUtility.FromJson<BookshelvesData>(json);
        
        foreach (ISaveShelves shelfDataObj in bookshelfDataObjects)
        {
            shelfDataObj.LoadShelves(shelvesData);
        }

        foreach (var info in shelvesData.Books)
        {
            GameObject book = Instantiate(bookPrefab);

            bookPrefab bookScript = book.GetComponent<bookPrefab>();

            //figure out best way to convert ID to gameobjects
        }   
        
    }

    public void SaveGame()
    {
        
    }

    public void LoadGame()
    {
        
    }

    public void SaveCounter()
    {
        
    }

    public void LoadCounter()
    {
        
    }

}
