using System;
using System.Diagnostics;
using NUnit.Framework.Internal;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    //arrays
    public BookData[] books = {};
    public Sprite[] CoverSprites = {};

    //variables
    public Sprite bookCover;
    public BookData book;

    //rendering book gameobject
    public GameObject cover;
    SpriteRenderer coverRenderer;
    public TextMeshPro title;
    public TextMeshPro author;
    

    void Start()
    {
        // randomised book & cover
        book = books[UnityEngine.Random.Range(0, books.Length)];
        bookCover = CoverSprites[UnityEngine.Random.Range(0, CoverSprites.Length)]; 

        //set bookcover sprite to sprite renderer
        coverRenderer = cover.GetComponent<SpriteRenderer>();
        coverRenderer.sprite = bookCover;
        
        //set book title to title text
        title.text = book.bookTitle;

        //set author to author text
        author.text = book.authorName;

        //change book gameobject name to match book title
        gameObject.name = book.bookTitle.ToString();
        
    }

}
