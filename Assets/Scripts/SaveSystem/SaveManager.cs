using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public Book bookPrefab;

    public void Save()
    {
        GameData save = new GameData();

        foreach (Book book in FindObjectsByType<Book>(FindObjectsSortMode.None))
        {
            BookSaveData data = new BookSaveData();

            data.bookID = book.bookdata.bookID;
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
            Book book = Instantiate(bookPrefab);

            book.bookdata = BooksManager.Instance.GetByID(data.bookID);
            var cover = SpriteManager.Instance.GetSprite(data.coverSpriteID);

            book.SetCoverSprite(cover, data.coverSpriteID);
            var spine = SpriteManager.Instance.GetSprite(data.spineSpriteID);
            book.SetSpineSprite(spine, data.spineSpriteID);

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