using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class bookPrefab : MonoBehaviour
{
    //arrays
    public BookData[] books = {};
    public Sprite[] CoverSprites = {};
    public Sprite [] SpineSprites = {};

    //variables
    public Sprite bookCover;
    public Sprite spineSprite;
    public BookData book;

    //rendering book gameobject
    public Image coverImage;
    public TextMeshProUGUI title;
    public TextMeshProUGUI author;

    //spine
    public Image spine;
    public TextMeshProUGUI spineTitle;
    

    void Start()
    {
        // randomised book & cover
        book = books[UnityEngine.Random.Range(0, books.Length)];
        bookCover = CoverSprites[UnityEngine.Random.Range(0, CoverSprites.Length)]; 

        //match spine to cover
        int coverIndex = Array.IndexOf(CoverSprites, bookCover);
        spineSprite = SpineSprites[coverIndex];

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
        
    }

}
