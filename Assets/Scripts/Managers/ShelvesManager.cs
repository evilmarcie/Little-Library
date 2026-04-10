using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ShelvesManager : MonoBehaviour
{
    public List<Shelf> shelves = new List<Shelf>();
    private Dictionary<string, Shelf> lookup;
    public static ShelvesManager instance;
    public bool FullyLoaded = false;

    void Awake()
    {
        
        shelves = new List<Shelf>();
        lookup = new Dictionary<string, Shelf>();
        instance = this; 

    }

    public void FindShelves()
    {
        
        if (shelves == null || shelves.Count == 0 || lookup == null || lookup.Count == 0)
        {
            shelves.Clear();
            shelves = FindObjectsByType<Shelf>(FindObjectsSortMode.None).ToList();
            
            if (shelves == null || shelves.Count == 0)
            {
                return;
            }
            else
            {
                lookup = new Dictionary<string, Shelf>();

                foreach (var shelf in shelves)
                {
                    lookup[shelf.shelfID] = shelf;
                }
                if (shelves.Count > 0)
                {
                    FullyLoaded = true;
                }
            }
        }
        else
        {
            FullyLoaded = true;
        }
    
    }
    
    public Shelf GetShelf(string id)
    {
        return lookup[id];
    }
}
