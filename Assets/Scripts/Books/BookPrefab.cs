using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class bookPrefab : MonoBehaviour
{
    //arrays
    public BookData[] books = {};
    public SpriteInfo coverSpriteInfo;
    public SpriteInfo spineSpriteInfo;
    public List<SpriteInfo> CoverSprites;
    public List<SpriteInfo> SpineSprites;

    //variables
    public Sprite bookCover;
    public Sprite spineSprite;
    public int spriteNumber;
    public BookData book;

    //rendering book gameobject
    public Image coverImage;
    public TextMeshProUGUI title;
    public TextMeshProUGUI author;

    //spine
    public Image spine;
    public TextMeshProUGUI spineTitle;

    //ids
    public string coverSpriteID;
    public string spineSpriteID;
    public string bookId;
    

    void Start()
    {
        // randomised book & cover
        book = books[UnityEngine.Random.Range(0, books.Length)];
        
        //random cover
        CoverSprites = GetComponent<SpriteManager>().CoverSprites;
        spriteNumber = UnityEngine.Random.Range(0, CoverSprites.Count);
        coverSpriteInfo = CoverSprites[spriteNumber];
        bookCover = coverSpriteInfo.sprite;

        //match spine to cover
        SpineSprites = GetComponent<SpriteManager>().SpineSprites;
        spineSpriteInfo = SpineSprites[spriteNumber];
        spineSprite = spineSpriteInfo.sprite;


        //set bookcover sprite to image renderer
        coverImage.sprite = bookCover;
        spine.sprite = spineSprite;
        
        //set book title to title text
        title.text = book.bookTitle;
        spineTitle.text = book.bookTitle;

        //set author to author text
        author.text = book.authorName;

        //change book gameobject name to match book title
        gameObject.name = book.bookTitle.ToString();

        //get id info
        bookId = book.bookID;
        coverSpriteID = coverSpriteInfo.id;
        spineSpriteID = spineSpriteInfo.id;
        
    }

}
