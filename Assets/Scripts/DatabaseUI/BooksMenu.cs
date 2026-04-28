using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor.Animations;
using UnityEngine.Animations;
using System.Collections.Generic;

public class BooksMenu : MonoBehaviour
{
    //menus
    public GameObject homeMenu;
    public GameObject infoMenu;

    public Scrollbar booksScrollbar;

    // bookgenre sorting
    public GameObject content;
    public Button[] books;
    bool includeInactive = true;

    public void returnButton()
    {
        infoMenu.SetActive(false);
        homeMenu.SetActive(true);
    }

    public void genreReturnButton()
    {
        books = content.GetComponentsInChildren<Button>(includeInactive);
        
        foreach (Button bookButton in books)
        {
            bookButton.gameObject.SetActive(true);
        }

        booksScrollbar.value = 1;

        foreach (GameObject buttons in genreButtons)
        {
            buttons.SetActive(true);
        }
    }

    public List<GameObject> genreButtons;
    
}
