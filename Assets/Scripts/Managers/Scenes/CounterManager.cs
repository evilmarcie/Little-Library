using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CounterManager : MonoBehaviour, ISaveCounter
{ 
    public static CounterManager instance;
    void Awake()
    {
        instance = this;
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

    void Start()
    {
        character.SetActive(false);
        dialogueUI.SetActive(false);
    }

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
        dialogueText.text = activeCustomer.initialGreeting;
        haveMet.Add(activeCustomer);
    }

    public void Greeting()
    {
        int greetingInt = UnityEngine.Random.Range(0, activeCustomer.greetingLines.Count);
        string greeting = activeCustomer.greetingLines[greetingInt];

        TypeLine(greeting);
    }

    string prompt;

    public void Prompt()
    {

        currentStage = DialogueStage.Prompt;

        int promptInt = UnityEngine.Random.Range(0, activeCustomer.promptLines.Count);
        prompt = activeCustomer.promptLines[promptInt];

        TypeLine(prompt);
    }

    public void RepeatPrompt()
    {
        TypeLine(prompt);
    }

    public void GiveBook(BookData givenBook)
    {
        SetDialogueSprites();

        currentStage = DialogueStage.GiveBook;

        nameText.text = activeCustomer.name;
        string giveBook = givenBook.bookTitle+" by "+ givenBook.authorName+"?";

        TypeLine(giveBook);

    }

    string response;

    public void Response(BookData givenBook)
    {
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

        TypeLine(response);
    }

    public GameObject dialogueUI;

    public void EndInteraction()
    {
        dialogueUI.SetActive(false);
        dialogueText.text = null;

        if(currentStage == DialogueStage.Response)
        {
            character.SetActive(false);
            activeCustomer = null;
            nameText.text = null;
            prompt = null;
            currentStage = DialogueStage.Inactive;
        }
    }
    
    public float textSpeed;

    IEnumerator TypeLine(string text)
    {
        foreach(char c in text.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
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
        }
        else if(currentStage == DialogueStage.Response)
        {
            EndInteraction();
        }   
    }

    public BookData givenBook;

    public void SaveCounter(ref CounterData counterData)
    {
        counterData.currentCustomer = activeCustomer;
        counterData.dialogueStageInt = (int)currentStage;
        
        //save met cx list
        //save visited today list
    }

    public void LoadCounter(CounterData counterData)
    {
        activeCustomer = counterData.currentCustomer;
        givenBook = BookManager.instance.GetBookData(counterData.givenBookID);
        currentStage = (DialogueStage)counterData.dialogueStageInt;

        if (currentStage != DialogueStage.Inactive)
        {
            SetCharacterSprites();
        }

        if (counterData.triggerGiveBook == true)
        {
            GiveBook(givenBook);
        }
    }
}
