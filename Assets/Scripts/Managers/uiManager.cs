using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    public GameObject databaseMenu;
    public GameObject pauseMenu;
    public bool gamePaused = false;

    public GameObject homeMenu;
    public GameObject infoMenu;
    public Scrollbar booksScrollbar;
    public Scrollbar achievementsScrollbar;
    public GameObject leftButton;
    public GameObject rightButton;

    public static uiManager UImanager;

    void Awake()
    {
        if (UImanager == null)
        {
            UImanager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        leftButton.SetActive(false);
        databaseMenu.SetActive(false);

    }

    void Update()
    {

        if (gamePaused == true)
        {
            //buttons and other features unable to be interacted with
        }
        
    }

    public void OpenDatabase()
    {
        databaseMenu.SetActive(true);
        databaseMenu.GetComponent<DatabaseUI>().OpenBooksMenu();
        resetDatabase();

    }

    public void CloseDatabase()
    {
        databaseMenu.SetActive(false);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        gamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        gamePaused = false;
    }

     void resetDatabase()
    {
        homeMenu.SetActive(true);
        infoMenu.SetActive(false);
        booksScrollbar.value = 1;
        achievementsScrollbar.value = 1;
    }

    public void toShelves()
    {
        //SaveManager.instance.SaveGame();

        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.Bookshelves, SetActive: true)
            .Load(SceneDatabase.Slots.UI, SceneDatabase.Scenes.UI)
            .WithOverlay()
            .Perform();

        SaveManager.instance.LoadGame();

        leftButton.SetActive(true);
        rightButton.SetActive(false);
    }

    public void toCounter()
    {
        SaveManager.instance.SaveGame();

          SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.Counter, SetActive: true)
            .Load(SceneDatabase.Slots.UI, SceneDatabase.Scenes.UI)
            .WithOverlay()
            .Perform();

        //SaveManager.instance.LoadGame();

        rightButton.SetActive(true);
        leftButton.SetActive(false);
    }

    
    //book display
    public Image infoCover;
    public TextMeshProUGUI infoTitle;
    public TextMeshProUGUI infoAuthor;

    //book info
    public TextMeshProUGUI titleHeader;
    public TextMeshProUGUI authorInfo;
    public TextMeshProUGUI publishedInfo;
    public TextMeshProUGUI genreInfo;
    public TextMeshProUGUI descInfo;


    public void setBookInfo(BookData book, Sprite cover)
    {
        if (databaseMenu.activeSelf == false)
        {
            databaseMenu.SetActive(true);
        }
        
        homeMenu.SetActive(false);
        infoMenu.SetActive(true);

        //book display
        infoCover.sprite = cover;
        infoTitle.text = book.bookTitle;
        infoAuthor.text = book.authorName;

        //info
        titleHeader.text = book.bookTitle;
        authorInfo.text = "Author: " + book.authorName;
        publishedInfo.text = "Published: " + book.publicationYear;
        genreInfo.text = "Genre: " + book.BookGenre;
        descInfo.text = "Description: " + book.bookBio; 

    }

}
