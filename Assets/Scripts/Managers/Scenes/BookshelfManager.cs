using System.Collections;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BookshelfManager : MonoBehaviour
{
    public static BookshelfManager instance;

    void Awake()
    {
            instance = this;    
    }

    IEnumerator Start()
    {
        ShelvesManager.instance.FindShelves();
        while (ShelvesManager.instance.FullyLoaded == false)
        {
            yield return new WaitForEndOfFrame();
        }
        SaveManager.instance.LoadBookshelves();
        
    }
    
    public GameObject heldBook;
    public bool holdingBook;
}
