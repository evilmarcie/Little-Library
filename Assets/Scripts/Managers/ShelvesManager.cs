using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ShelvesManager : MonoBehaviour
{
    public List<Shelf> shelves = new List<Shelf>();
    private Dictionary<string, Shelf> lookup;

    void Awake()
    {
        instance = this; 
        
        shelves.Clear();
        FindShelves();

        if (shelves == null)
        {
            return;
        }
        else
        {
            lookup = new Dictionary<string, Shelf>();

            foreach (var shelf in shelves)
            {
                lookup [ shelf.shelfID ] = shelf;
            } 
        }

    }

    public void FindShelves()
    {
        shelves = FindObjectsByType<Shelf>(FindObjectsSortMode.None).ToList();
        Debug.Log(shelves);
    }
    
    public Shelf GetShelf(string id)
    {
        return lookup[id];
    }

    public static ShelvesManager instance;
}
