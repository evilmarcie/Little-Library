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
        
        string json = JsonUtility.ToJson(save, true);
        File.WriteAllText(Application.persistentDataPath + "LittleLibrary_SaveData", json);
    }

    public void Load()
    {
       string path = Application.persistentDataPath + "LittleLibrary_SaveData";

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
    }

    public static SaveManager Instance;

    void OnEnable()
    {
        Instance = this;
    }
}