using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CounterManager : MonoBehaviour
{
    public Character activeCustomer;
    public Character[] potentialCustomers;
    public GameObject character;

    public void customerEnter()
    {
        randomCustomer();

        if (activeCustomer.visitedToday == true)
        {
            randomCustomer();
        }
        else
        {
            Image characterImg = character.GetComponent<Image>();
            characterImg.sprite = activeCustomer.characterSprite;
        }

    }

    public void randomCustomer()
    {
        activeCustomer = potentialCustomers[UnityEngine.Random.Range(0, potentialCustomers.Length)];
    }

    public GameObject dialogueBox;
    public GameObject nameBox;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public void BeginInteraction()
    {
        nameText.text = activeCustomer.name;
        Image nameBoxImage = nameBox.GetComponent<Image>();
        nameBoxImage.color = activeCustomer.characterColour;
    }
}
