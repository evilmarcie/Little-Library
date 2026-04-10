using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public bookPrefab prefab;

    public void Save()
    {
        GameData save = new GameData();

        foreach (bookPrefab book in FindObjectsByType<bookPrefab>(FindObjectsSortMode.None))
        {
            BookSaveData data = new BookSaveData();

            data.bookID = book.bookId;
            data.coverSpriteID = book.coverSpriteID;
            data.spineSpriteID = book.spineSpriteID;

            Shelf shelf = book.transform.parent.GetComponent<Shelf>();
            data.shelfID = shelf.shelfID;

            data.siblingIndex = book.transform.GetSiblingIndex();

            save.books.Add(data);
        }
        
<<<<<<< Updated upstream
        string json = JsonUtility.ToJson(save, true);
        File.WriteAllText(Application.persistentDataPath + "LittleLibrary_SaveData", json);
=======
        string json = JsonUtility.ToJson(shelvesData, true);
        File.WriteAllText(Application.persistentDataPath + "BookshelfData", json);

        
>>>>>>> Stashed changes
    }

    public void Load()
    {
<<<<<<< Updated upstream
       string path = Application.persistentDataPath + "LittleLibrary_SaveData";
=======
        bookshelfDataObjects = FindAllSaveShelvesObj();

        string path = Application.persistentDataPath + "BookshelfData";
>>>>>>> Stashed changes

        if (!File.Exists(path)) return;

        string json = File.ReadAllText(path);
        GameData save = JsonUtility.FromJson<GameData>(json);

        foreach (BookSaveData data in save.books)
        {
            bookPrefab book = Instantiate(prefab);

            book.book = BookManager.Instance.GetByID(data.bookID);
            book.bookId = data.bookID;

            var cover = SpriteManager.Instance.GetSprite(data.coverSpriteID);
            book.bookCover = cover;
            book.coverSpriteID = data.coverSpriteID;

            var spine = SpriteManager.Instance.GetSprite(data.spineSpriteID);
            book.spineSprite = spine;
            book.spineSpriteID = data.spineSpriteID;

            Shelf shelf = ShelfManager.Instance.GetShelf(data.shelfID);
            book.transform.SetParent(shelf.transform);
            book.transform.SetSiblingIndex(data.siblingIndex);
        }
<<<<<<< Updated upstream
=======

        foreach (var info in shelvesData.Books)
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
        
>>>>>>> Stashed changes
    }

    public static SaveManager Instance;

    void OnEnable()
    {
        Instance = this;
    }
}