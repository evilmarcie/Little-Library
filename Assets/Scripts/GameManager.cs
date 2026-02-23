using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject databaseMenu;

    void Start()
    {
        databaseMenu.SetActive(false);
    }

    public void OpenDatabase()
    {
        databaseMenu.SetActive(true);
    }

    public void CloseDatabase()
    {
        databaseMenu.SetActive(false);
    }
}
