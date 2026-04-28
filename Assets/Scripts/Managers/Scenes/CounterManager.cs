using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class CounterManager : MonoBehaviour, ISaveCounter, ISaveGame
{ 
    public static CounterManager instance;
    public bool CounterLoaded = false;

    void Awake()
    {
        if (instance != null){Destroy(instance);} 
        instance = this;
        character.SetActive(false);
        dialogueUI.SetActive(false);
        CounterLoaded = true;
    }

    IEnumerator Start()
    {
        while (CounterLoaded == false)
        {
            yield return new WaitForEndOfFrame();
        }
        if (uiManager.instance.LoadingFromShelves == true)
        {
            SaveManager.instance.LoadCounter();
            Debug.Log("loading from shelves true");
        }
        if (uiManager.instance.LoadingFromShelves == false)
        {
            uiManager.instance.UpdateDayUI(SessionManager.instance.day);
            uiManager.instance.DeliveryNotification(true);
            instance.Reset();
            
            if (SessionManager.instance.day == 1)
            {
                Debug.Log("tutorial");
            }
        }
    }

    public Character activeCustomer;
    public Character[] potentialCustomers;
    public GameObject character;

    public List<Character> visitedToday;
    public List<Character> haveMet;

    public GameObject dialogueBox;
    public GameObject nameBox;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public void Reset()
    {
        activeCustomer = null;
        visitedToday.Clear();
        currentStage = DialogueStage.Inactive;
        givenBook = null;

        Debug.Log("execute reset");
    }

    public void customerEnter()
    {
        Debug.Log("customer enter");

        randomCustomer();
        
        if (visitedToday.Contains(activeCustomer))
        {
            StartCoroutine(elimiateRepeatCustomers());
        }

        visitedToday.Add(activeCustomer);
        SetCharacterSprites();
        SetDialogueSprites();
        BeginInteraction();
    }

    IEnumerator elimiateRepeatCustomers()
    {
        while (visitedToday.Contains(activeCustomer))
        {
            randomCustomer();
            yield return new WaitForEndOfFrame();
        }
        yield return activeCustomer;
    }

    Vector3 shortCharacterPos = new Vector3(21, -97, 0);
    Vector3 tallCharacterPos = new Vector3(21, -295, 0);

    public void SetCharacterSprites()
    {
        character.SetActive(true);
        if (activeCustomer.shortCharacter == true)
        {
            character.transform.localPosition = shortCharacterPos;
        }
        else
        {
            character.transform.localPosition = tallCharacterPos;
        }
        Image characterImg = character.GetComponent<Image>();
        characterImg.sprite = activeCustomer.characterSprite;
    }

    public void SetDialogueSprites()
    {
        dialogueUI.SetActive(true);
        nameText.text = activeCustomer.name;
    }

    public void randomCustomer()
    {
        activeCustomer = potentialCustomers[UnityEngine.Random.Range(0, potentialCustomers.Length)];
    }

    #region dialogue

    public void BeginInteraction()
    {
        currentStage = DialogueStage.Greeting;

        if (haveMet.Contains(activeCustomer))
        {
            Greeting();
        }
        else
        {
            FirstMeeting();
        }

    }
    public void FirstMeeting()
    {
        string firstMeeting = activeCustomer.initialGreeting;
        Dialogue(firstMeeting);
        haveMet.Add(activeCustomer);
    }

    public void Greeting()
    {
        int greetingInt = UnityEngine.Random.Range(0, activeCustomer.greetingLines.Count);
        string greeting = activeCustomer.greetingLines[greetingInt];

        Dialogue(greeting);
    }

    string prompt;

    public void Prompt()
    {

        currentStage = DialogueStage.Prompt;

        int promptInt = UnityEngine.Random.Range(0, activeCustomer.promptLines.Count);
        prompt = activeCustomer.promptLines[promptInt];

        Dialogue(prompt);

        SessionManager.instance.currentDayStage = SessionManager.DayStage.pickBooks;
    }

    public void RepeatPrompt()
    {
        Dialogue(prompt);

        SessionManager.instance.currentDayStage = SessionManager.DayStage.pickBooks;
    }

    public void GiveBook(BookData givenBook)
    {
        SetDialogueSprites();

        currentStage = DialogueStage.GiveBook;

        nameText.text = activeCustomer.name;
        string giveBook = givenBook.bookTitle+" by "+ givenBook.authorName+"?";

        Dialogue(giveBook);

    }

    string response;

    public void Response(BookData givenBook)
    {

        currentStage = DialogueStage.Response;

         if (activeCustomer.lovedBooks.Contains(givenBook))
        {
            int responseINT = UnityEngine.Random.Range(0, activeCustomer.lovedResponse.Count);
            response = activeCustomer.lovedResponse[responseINT];
            SessionManager.instance.updateCustomerSatisfaction(20);
        }
        else if (activeCustomer.likedBooks.Contains(givenBook))
        {
            int responseINT = UnityEngine.Random.Range(0, activeCustomer.likedResponse.Count);
            response = activeCustomer.likedResponse[responseINT];
            SessionManager.instance.updateCustomerSatisfaction(10);
        }
        else if (activeCustomer.neutralBooks.Contains(givenBook))
        {
            int responseINT = UnityEngine.Random.Range(0, activeCustomer.neutralResponse.Count);
            response = activeCustomer.neutralResponse[responseINT];
            SessionManager.instance.updateCustomerSatisfaction(0);
        }
        else if (activeCustomer.dislikedBooks.Contains(givenBook))
        {
            int responseINT = UnityEngine.Random.Range(0, activeCustomer.dislikedResponse.Count);
            response = activeCustomer.dislikedResponse[responseINT];
            SessionManager.instance.updateCustomerSatisfaction(-20);
        }
        else
        {
            Debug.Log("error, book not found in lists");
        }
        Dialogue(response);
    }

    public GameObject dialogueUI;

    int maxCXperDay = 3;

    public void EndInteraction()
    {
        dialogueUI.SetActive(false);
        dialogueText.text = " ";

        if(currentStage == DialogueStage.Response)
        {
            character.SetActive(false);
            activeCustomer = null;
            nameText.text = null;
            prompt = null;
            currentStage = DialogueStage.Inactive;

            StartCoroutine(nextInteraction());
        }

        uiManager.instance.SetButtonInteractions(true);

    }

    IEnumerator nextInteraction()
    {
        if (visitedToday.Count >= maxCXperDay)
            {
                uiManager.instance.DayEnd();
            }
            else
            {
                  new WaitForSecondsRealtime(5);
                customerEnter();
            }

        yield return new WaitForEndOfFrame();
    }
    
    float textSpeed = 0.05f;

    public void Dialogue(string line)
    {
        SessionManager.instance.currentDayStage = SessionManager.DayStage.inDialogue;

        if (typeLineCoroutine != null)
        {
            StopCoroutine(typeLineCoroutine);
        }  
        typeLineCoroutine = StartCoroutine(TypeLine(line));
    }

    public Coroutine typeLineCoroutine;

    IEnumerator TypeLine(string text)
    { 
        dialogueText.text = " ";

        canStartNextLine = false;

        foreach(char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        canStartNextLine = true;
    }

    public enum DialogueStage { Greeting, Prompt, GiveBook, Response, Inactive}
    public DialogueStage currentStage;

    public void SetDialogueStage(DialogueStage stage)
    {
        currentStage = stage;
    }

    private bool canStartNextLine;

    public void NextDialogue()
    {
        uiManager.instance.SetButtonInteractions(false);

        if (canStartNextLine)
        {
            if (currentStage == DialogueStage.Greeting)
            {
                Prompt();
            }
            else if (currentStage == DialogueStage.Prompt)
            {
                EndInteraction(); 
            }
            // givebook triggered by game event
            else if(currentStage == DialogueStage.GiveBook)
            {
                Response(givenBook);
                Debug.Log("go to book response");
            }
            else if(currentStage == DialogueStage.Response)
            {
                EndInteraction();
            } 
        }
          
    }
    #endregion 

    #region save counter

    public BookData givenBook;

    public void SaveCounter(ref CounterData counterData)
    {
        
        if (activeCustomer != null)
        {
            counterData.currentCustomerID = activeCustomer.characterID;
            //Debug.Log("saving active customer");
        }

        counterData.dialogueStageInt = (int)currentStage;
        
        foreach (Character met in haveMet)
        {
            string metID = met.characterID;
            counterData.metCustomersID.Add(metID);
        }

        foreach (Character visited in visitedToday)
        {
            string visitedID = visited.characterID;
            counterData.visitedTodayID.Add(visitedID);
        }
    }

    public void LoadCounter(CounterData counterData)
    {

        if (counterData.currentCustomerID != string.Empty)
        {
            string activeCharID = counterData.currentCustomerID;
            Debug.Log(activeCharID);
            activeCustomer = CharacterManager.instance.GetCharacter(activeCharID);
            currentStage = (DialogueStage)counterData.dialogueStageInt;
            if (currentStage != DialogueStage.Inactive)
            {
                SetCharacterSprites();
            }
        }
        else
        {
            Debug.Log("no active char");
        }

        if (counterData.metCustomersID.Count > 0)
        {
            foreach (string met in counterData.metCustomersID)
            {
                string metID = met;
                Character character = CharacterManager.instance.GetCharacter(metID);
                if (character!=null){haveMet.Add(character);}
                else{Debug.Log("cannot find char from ID");}
            }   
        }
        else{Debug.Log("met customers list empty");}

        if (counterData.visitedTodayID.Count > 0)
        {
            foreach (string visited in counterData.visitedTodayID)
            {
                string visitedID = visited;
                Character character = CharacterManager.instance.GetCharacter(visitedID);
                if (character!=null){visitedToday.Add(character);}
                else{Debug.Log("cannot find char from ID");}
            }   
        }
        else{Debug.Log("visited today list empty");}

        if (((counterData.givenBookID != null)|(counterData.givenBookID != string.Empty)) 
        && (counterData.triggerGiveBook == true))
        {
            givenBook = BookManager.instance.GetBookData(counterData.givenBookID);
            Debug.Log(givenBook.bookTitle);   
            GiveBook(givenBook);
        }

        if ((SessionManager.instance.completeBookBox == true) && (activeCustomer == null) 
        && (SessionManager.instance.currentDayStage == SessionManager.DayStage.cxArrive))
        {
            customerEnter();
        }
    }

    #endregion

    #region  save game
    public void SaveGame(ref GameData gameData)
    {
        foreach (Character customer in haveMet)
        {
            string id = customer.characterID;
            gameData.metCustomersID.Add(id);
        }
    }

    public void LoadGame(GameData gameData)
    {
        
    }
    #endregion
}
