using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CounterManager : MonoBehaviour, ISaveCounter
{ 
    public static CounterManager instance;
    public bool CounterLoaded = false;

    void Awake()
    {
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
        SaveManager.instance.LoadCounter();
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

    public void customerEnter()
    {
        randomCustomer();
        
        if (visitedToday.Contains(activeCustomer))
        {
            //while (visitedToday.Contains(activeCustomer))
            //{
                //randomCustomer();
            //}
        }
        visitedToday.Add(activeCustomer);
        SetCharacterSprites();
        SetDialogueSprites();
        BeginInteraction();
    }

    public void SetCharacterSprites()
    {
        character.SetActive(true);
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
        StartCoroutine(TypeLine(firstMeeting));
        haveMet.Add(activeCustomer);
    }

    public void Greeting()
    {
        int greetingInt = UnityEngine.Random.Range(0, activeCustomer.greetingLines.Count);
        string greeting = activeCustomer.greetingLines[greetingInt];

        StartCoroutine(TypeLine(greeting));
    }

    string prompt;

    public void Prompt()
    {

        currentStage = DialogueStage.Prompt;

        int promptInt = UnityEngine.Random.Range(0, activeCustomer.promptLines.Count);
        prompt = activeCustomer.promptLines[promptInt];

        StartCoroutine(TypeLine(prompt));
    }

    public void RepeatPrompt()
    {
        StartCoroutine(TypeLine(prompt));
    }

    public void GiveBook(BookData givenBook)
    {
        SetDialogueSprites();

        currentStage = DialogueStage.GiveBook;

        nameText.text = activeCustomer.name;
        string giveBook = givenBook.bookTitle+" by "+ givenBook.authorName+"?";

        StartCoroutine(TypeLine(giveBook));

    }

    string response;

    public void Response(BookData givenBook)
    {
        Debug.Log("response");

        currentStage = DialogueStage.Response;

         if (activeCustomer.lovedBooks.Contains(givenBook))
        {
            int responseINT = UnityEngine.Random.Range(0, activeCustomer.lovedResponse.Count);
            response = activeCustomer.lovedResponse[responseINT];
        }
        else if (activeCustomer.likedBooks.Contains(givenBook))
        {
            int responseINT = UnityEngine.Random.Range(0, activeCustomer.likedResponse.Count);
            response = activeCustomer.likedResponse[responseINT];
        }
        else if (activeCustomer.neutralBooks.Contains(givenBook))
        {
            int responseINT = UnityEngine.Random.Range(0, activeCustomer.neutralResponse.Count);
            response = activeCustomer.neutralResponse[responseINT];
        }
        else if (activeCustomer.dislikedBooks.Contains(givenBook))
        {
            int responseINT = UnityEngine.Random.Range(0, activeCustomer.dislikedResponse.Count);
            response = activeCustomer.dislikedResponse[responseINT];
        }
        else
        {
            Debug.Log("error, book not found in lists");
        }

        Debug.Log(response);
        StartCoroutine(TypeLine(response));
    }

    public GameObject dialogueUI;

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
        }
    }
    
    float textSpeed = 0.05f;

    IEnumerator TypeLine(string text)
    { 

        dialogueText.text = " ";

        foreach(char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        // add finish line when pressed if mid typing
    }

    public enum DialogueStage { Greeting, Prompt, GiveBook, Response, Inactive}
    public DialogueStage currentStage;

    public void SetDialogueStage(DialogueStage stage)
    {
        currentStage = stage;
    }

    public void NextDialogue()
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

        Debug.Log("next dialogue");
    }

    public BookData givenBook;

    public void SaveCounter(ref CounterData counterData)
    {
        counterData.currentCustomerID = activeCustomer.characterID;
        counterData.dialogueStageInt = (int)currentStage;
        
        foreach (Character met in haveMet)
        {
            string metID = met.characterID;
            counterData.metCustomersID.Add(metID);
        }

        foreach (Character visited in visitedToday)
        {
            string visitedID = visited.characterID;
            counterData.metCustomersID.Add(visitedID);
        }
    }

    public void LoadCounter(CounterData counterData)
    {
        Debug.Log("load start");

        string activeCharID = counterData.currentCustomerID;
        activeCustomer = CharacterManager.instance.GetCharacter(activeCharID);

        currentStage = (DialogueStage)counterData.dialogueStageInt;

        foreach (string met in counterData.metCustomersID)
        {
            string metID = met;
            Character character = CharacterManager.instance.GetCharacter(metID);
            haveMet.Add(character);
        }

        foreach (string visited in counterData.visitedTodayID)
        {
            string visitedID = visited;
            Character character = CharacterManager.instance.GetCharacter(visitedID);
            haveMet.Add(character);
        }

        if (currentStage != DialogueStage.Inactive)
        {
            SetCharacterSprites();
        }

        givenBook = BookManager.instance.GetBookData(counterData.givenBookID);
        Debug.Log(givenBook.bookTitle);

        if (counterData.triggerGiveBook == true)
        {
            GiveBook(givenBook);
        }
        Debug.Log("loaded");
    }

}
