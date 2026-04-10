using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    void Awake() { instance = this; }

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
        
<<<<<<< Updated upstream
        string json = JsonUtility.ToJson(shelvesData, true);
        File.WriteAllText(Application.persistentDataPath + "LittleLibrary_BookshelfData", json);
=======
        string json = JsonUtility.ToJson(shelvesData, true);
        File.WriteAllText(Application.persistentDataPath + "BookshelfData", json);

        
>>>>>>> Stashed changes

        
    }

    public GameObject bookPrefab;

    public void LoadBookshelves()
    
    {
<<<<<<< Updated upstream
        bookshelfDataObjects = FindAllSaveShelvesObj();

        string path = Application.persistentDataPath + "LittleLibrary_BookshelfData";
=======
        bookshelfDataObjects = FindAllSaveShelvesObj();

        string path = Application.persistentDataPath + "BookshelfData";
>>>>>>> Stashed changes

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
