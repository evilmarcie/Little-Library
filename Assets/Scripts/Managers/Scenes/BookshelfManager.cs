using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BookshelfManager : MonoBehaviour
{
    public GameObject bookPrefab;
    public Canvas canvas;

    public void GenerateBook()
    {
        GameObject book = Instantiate(bookPrefab, new Vector3(-778, 111, 0), Quaternion.identity);
        book.transform.SetParent(canvas.transform, false);
    
    }

    public bool holdingBook;
}
