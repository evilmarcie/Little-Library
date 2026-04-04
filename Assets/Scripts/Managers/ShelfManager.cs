using System.Collections.Generic;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    public List<Shelf> shelves;
    private Dictionary<string, Shelf> lookup;

    void Awake()
    {
        lookup = new Dictionary<string, Shelf>();

        foreach (var shelf in shelves)
        {
            lookup [ shelf.shelfID ] = shelf;
        }
    }

    public Shelf GetShelf(string id)
    {
        return lookup[id];
    }

    public static ShelfManager Instance;
    void OnEnable()
    {
        Instance = this;
    }
}
