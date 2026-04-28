using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DatabaseUI : MonoBehaviour
{
    public GameObject BooksMenu;

    public void OpenBooksMenu()
    {
        BooksMenu.SetActive(true);
    }

    public void OpenCXMenu()
    {
        BooksMenu.SetActive(false);
    }

    public void OpenAchievementsMenu()
    {
        BooksMenu.SetActive(false);
    }
    
}
