using System;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    public string shelfID;

     [ContextMenu("Create New GUID")]
    public void GenerateUID()
    {
        shelfID = Guid.NewGuid().ToString();
    }
}
