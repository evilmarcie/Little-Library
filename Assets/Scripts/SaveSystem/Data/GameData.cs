using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class GameData
{
    //counterScene
    public Character activeCustomer;

    //shelvesScene
    public List<BookSaving.BookGroup> Groups = new List<BookSaving.BookGroup>();

    //other

    public GameData()
    {
        //inital values
        // data when there is no values to load
        activeCustomer = null;
        Groups.Clear();
    }
}
