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
    //public List<GameObject> booksOwned;
    //check that maintains cover etc
    public BookData book;
    public Sprite coverSprite;
    public Sprite spineSprite;
    public Transform shelfParent;

    //other

    public GameData()
    {
        //inital values
        // data when there is no values to load
        activeCustomer = null;
        //booksOwned = new List<GameObject>();
    }
}
