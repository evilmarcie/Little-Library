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
        customerNotif.SetActive(false);
        pauseMenu.SetActive(false);
        overlayUI.SetActive(false);
        settingsMenu.SetActive(false);
        quitWarning.SetActive(false);
        
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
                SetButtonInteractions(true);
                if (gameStarted == true)
                {
                    pauseMenu.SetActive(true);
                }
            }
        }
        
    }

    public void OpenDatabase()
    {
        databaseMenu.SetActive(true);
        databaseMenu.GetComponent<DatabaseUI>().OpenBooksMenu();
        resetDatabase();
        SetButtonInteractions(false);

    }

    public void CloseDatabase()
    {
        databaseMenu.SetActive(false);
        SetButtonInteractions(true);
    }

    public GameObject pauseMenu;
    public bool gamePaused = false;

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        gamePaused = true;
        SetButtonInteractions(false);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        gamePaused = false;
        SetButtonInteractions(true);
    }

     void resetDatabase()
    {
        homeMenu.SetActive(true);
        infoMenu.SetActive(false);
        booksScrollbar.value = 1;
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

        if (SessionManager.instance.bookBoxToday == false)
        {
            Debug.Log("book box today false");
            StartCoroutine(StartBookBox());
        }

    }

    IEnumerator StartBookBox()
    {
        Debug.Log("bookbox routine");
        while (Bookbox.instance == null)
        {
            yield return new WaitForEndOfFrame();
        }
        Bookbox.instance.boxParent.SetActive(true);
        Bookbox.instance.GenerateBookBox();   
        yield break;
        
    }

    public bool LoadingFromShelves;

    public void toCounter()
    {
        SaveManager.instance.SaveBookshelves();

        LoadingFromShelves = true;

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
        SaveManager.instance.SaveGame();
        // fade out of some kind
        Application.Quit();
    }

    public GameObject quitWarning;

    public void QuitWarning()
    {
        quitWarning.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void CancelQuit()
    {
        quitWarning.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NextDay()
    {
        SaveManager.instance.SaveGame();
        // reload to counter fully with load screen
        LoadingFromShelves = false;
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
        pauseMenu.SetActive(false);
    }

    public GameObject databasebutton;

    public void SetButtonInteractions(bool set)
    {
        rightButton.GetComponent<Button>().interactable = set;
        databasebutton.GetComponent<Button>().interactable = set;        
    }
}
