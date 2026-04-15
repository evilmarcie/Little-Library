using System;
using System.Collections;
using System.Collections.Generic;
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
    //public GameObject leftButton;
    public GameObject rightButton;

    public static uiManager instance;

    public GameObject signUI;
    public bool uiLoaded = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        uiLoaded = true;

        //leftButton.SetActive(false);
        databaseMenu.SetActive(false);
        EODmenu.SetActive(false);

    }

    IEnumerator Start()
    {
        while (uiLoaded == false)
        {
            yield return new WaitForEndOfFrame();
        }
        SessionManager.instance.DayStart();
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
        CounterManager.instance.CounterLoaded = false;
        
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.Bookshelves, SetActive: true)
            .Load(SceneDatabase.Slots.UI, SceneDatabase.Scenes.UI)
            .WithOverlay()
            .Perform();
        
        SaveManager.instance.SaveCounter();

        //leftButton.SetActive(true);
        rightButton.SetActive(false);
        signUI.SetActive(false);
    }

    public void toCounter()
    {
        SaveManager.instance.SaveBookshelves();

        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.Counter, SetActive: true)
            .Load(SceneDatabase.Slots.UI, SceneDatabase.Scenes.UI)
            .WithOverlay()
            .Perform();

        rightButton.SetActive(true);
        signUI.SetActive(true);

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

    public GameObject EODmenu;
    public TextMeshProUGUI dayHeader;
    public TextMeshProUGUI rating;
    public GameObject bookSummary;

    public void DayEnd()
    {
        EODmenu.SetActive(true);
        dayHeader.text = "DAY " + SessionManager.instance.day.ToString();
        rating.text = SessionManager.instance.currentRating.ToString();
        // book summary (instantiate each book from save?)
    }

    public void SaveAndQuit()
    {
        // save full game
        // end application
    }

    public void NextDay()
    {
        SessionManager.instance.ResetDay();
        EODmenu.SetActive(false);
    }

    public TextMeshProUGUI ratingSign;
    public void UpdateRatingUI(int currentRating)
    {
        ratingSign.text = currentRating.ToString();

        // set text colour depending on value
    }

    public TextMeshProUGUI daySign;

    public void UpdateDayUI(int day)
    {
        daySign.text = "DAY " + day.ToString();
    }

}
