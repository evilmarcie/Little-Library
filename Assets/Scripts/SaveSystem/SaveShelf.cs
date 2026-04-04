using System;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class SaveShelf : MonoBehaviour, ISaveData
{
  public string Id;
  public GameData.BookGroup shelfData;
  [SerializeField] private GameObject bookPrefab;

    [ContextMenu("Create New GUID")]
    public void GenerateUID()
    {
        Id = Guid.NewGuid().ToString();
    }

     public void SaveData(ref GameData data)
    {
        shelfData.Id = Id;
        foreach (bookPrefab book in GetComponentsInChildren<bookPrefab>())
        {
            shelfData.Books.Add(book.book);
        }
        data.ShelfGroup.Add(shelfData);
        Debug.Log(data.ShelfGroup);
    }

    public void LoadData(GameData data)
    {
        shelfData = data.ShelfGroup.Find(match:group => group.Id == Id);
        RestoreHierarchy();
        Debug.Log(shelfData);
    }

    public void RestoreHierarchy()
    {
        foreach (BookData data in shelfData.Books)
        {
            GameObject bookGO = Instantiate(bookPrefab, transform.parent);
            bookPrefab book = bookGO.GetComponent<bookPrefab>();
            book.book = data;

            Debug.Log("restored hierarchy");
        }
    }
}