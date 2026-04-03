using System;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class SaveShelf : MonoBehaviour, ISaveData
{
  public string Id;
  public BookSaving.BookGroup GroupData;
  [SerializeField] private GameObject bookPrefab;

    [ContextMenu("Create New GUID")]
    public void GenerateUID()
    {
        Id = Guid.NewGuid().ToString();
    }

     public void SaveData(ref GameData data)
    {
        GroupData.Id = Id;
        foreach (Book book in GetComponentsInChildren<Book>())
        {
            GroupData.Books.Add(book.Data);
        }
        data.Groups.Add(GroupData);
        Debug.Log("save shelf data");
    }

    public void LoadData(GameData data)
    {
        GroupData = data.Groups.Find(match:group => group.Id == Id);
        RestoreHierarchy();
        Debug.Log("load shelf data");
    }

    public void RestoreHierarchy()
    {
        foreach (BookData data in GroupData.Books)
        {
            GameObject bookGO = Instantiate(bookPrefab, transform.parent);
            Book book = bookGO.GetComponent<Book>();
            book.Data = data;

            Debug.Log("restored hierarchy");
        }
    }
}