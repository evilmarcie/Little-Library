using System.Collections;
using UnityEngine;

public class BookshelfManager : MonoBehaviour
{
    public static BookshelfManager instance;

    void Awake()
    {
            instance = this; 
    }

    public GameObject holderPrefab;
    public GameObject leftButton;

    IEnumerator Start()
    {
        ShelvesManager.instance.FindShelves();
        while (ShelvesManager.instance.FullyLoaded == false)
        {
            yield return new WaitForEndOfFrame();
        }
        SaveManager.instance.LoadBookshelves();
        if ((SessionManager.instance.currentDayStage == SessionManager.DayStage.pickBooks) && (Holder.instance==null))
        {
            Debug.Log("activate holder");
            Instantiate(holderPrefab, leftButton.transform.position, Quaternion.identity, ShelfGroup.shelfGroup.canvas.transform);
            yield break;  
        }
        if((Bookbox.instance == null) && 
        (SessionManager.instance.bookBoxToday == false) && 
        (SessionManager.instance.currentDayStage == SessionManager.DayStage.delivery))
        {
            Debug.Log("instantiate bookbox");
            Instantiate(bookbox, ShelfGroup.shelfGroup.canvas.transform);
        }
        
    }

    public GameObject bookbox;
    
    public GameObject heldBook;
    public bool holdingBook;
}
