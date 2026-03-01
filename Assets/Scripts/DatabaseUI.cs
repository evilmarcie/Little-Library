using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DatabaseUI : MonoBehaviour
{
    public GameObject BooksMenu;
    public GameObject CXMenu;
    public GameObject AchievementsMenu;

    public void OpenBooksMenu()
    {
        BooksMenu.SetActive(true);
        CXMenu.SetActive(false);
        AchievementsMenu.SetActive(false);
    }

    public void OpenCXMenu()
    {
        BooksMenu.SetActive(false);
        CXMenu.SetActive(true);
        AchievementsMenu.SetActive(false);
    }

    public void OpenAchievementsMenu()
    {
        BooksMenu.SetActive(false);
        CXMenu.SetActive(false);
        AchievementsMenu.SetActive(true);
    }
    
}
