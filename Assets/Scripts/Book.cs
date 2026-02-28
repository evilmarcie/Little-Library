using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class Book : MonoBehaviour
{
    
    public BookData[] books = {};
    public Sprite[] CoverSprites = {};
    public Sprite bookCover;
    public BookData book;
    

    void Start()
    {
        book = books[UnityEngine.Random.Range(0, books.Length)];
        bookCover = CoverSprites[UnityEngine.Random.Range(0, CoverSprites.Length)];
        
    }

}
