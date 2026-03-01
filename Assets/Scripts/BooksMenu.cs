using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BooksMenu : MonoBehaviour
{
    public GameObject homeMenu;
    public GameObject infoMenu;

    public void returnButton()
    {
        infoMenu.SetActive(false);
        homeMenu.SetActive(true);
    }


}
