using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{

    #region  refs
    public GameObject databaseMenu;
    public GameObject homeMenu;
    public GameObject infoMenu;
    public Scrollbar booksScrollbar;
    public Scrollbar achievementsScrollbar;
    //public GameObject leftButton;
    public GameObject rightButton;
    public GameObject overlayUI;

    public static uiManager instance;

    public GameObject signUI;
    public bool uiLoaded = false;
    #endregion
    
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

        databaseMenu.SetActive(false);
        EODmenu.SetActive(false);
        deliveryNotif.SetActive(false);
        customerNotif.SetActive(false);
        pauseMenu.SetActive(false);
        overlayUI.SetActive(false);
        settingsMenu.SetActive(false);
        
        musicSlider.value = MusicManager.music.volume;
    }

    public bool gameStarted = false;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if ((gameStarted == true) && (settingsMenu.activeSelf == false))
            {
                PauseGame();
            }

            if (settingsMenu.activeSelf == true)
            {
                settingsMenu.SetActive(false);
            }
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

    public GameObject pauseMenu;
    public bool gamePaused = false;

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

    public GameObject bookBox;

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

        rightButton.SetActive(false);
        signUI.SetActive(false);

        if (deliveryNotif.activeSelf == true)
        {
            deliveryNotif.SetActive(false);
        }

        StartCoroutine(StartBookBox());

    }

    IEnumerator StartBookBox()
    {
        while (Bookbox.instance == null)
        {
            yield return new WaitForEndOfFrame();
        }
        
        if (SessionManager.instance.bookBoxToday == false)
        {
            Bookbox.instance.GenerateBookBox();   
        }
        yield break;
        
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

        if (customerNotif.activeSelf == true)
        {
            customerNotif.SetActive(false);
        }

    }

    #region refs
   
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

    #endregion

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

    public GameObject customerNotif;
    public GameObject deliveryNotif;

    public void CustomerNotification(bool set)
    {
        customerNotif.SetActive(set);
        Debug.Log("cx notif");
    }

    public void DeliveryNotification(bool set)
    {
        deliveryNotif.SetActive(set);
    }

    public Slider musicSlider;

    public void changeVolume()
    {
        MusicManager.music.volume = musicSlider.value;
    }

    public GameObject settingsMenu;

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }
}
