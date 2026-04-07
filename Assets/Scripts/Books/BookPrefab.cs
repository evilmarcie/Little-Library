using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class bookPrefab : MonoBehaviour, ISaveShelves
{
    //arrays
    public BookData[] books = {};
    private List<SpriteInfo> sprites;

    //variables
    public SpriteInfo spriteInfo;
    public int spriteNumber;
    public string spriteID;
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
        
        sprites = GetComponent<SpriteManager>().BookSprites;
        spriteNumber = UnityEngine.Random.Range(0, sprites.Count);
        spriteInfo = sprites[spriteNumber];

        bookCover = spriteInfo.cover;
        spine.sprite = spriteInfo.spine;
        spriteID = spriteInfo.id;

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

    public void LoadShelves(BookshelvesData bookshelvesData)
    {
        
    }

    public void SaveShelves(ref BookshelvesData bookshelvesData)
    {
        BookshelvesData.BookInfo info = new BookshelvesData.BookInfo();

        info.bookID = book.bookID;
        info.spritesID = spriteID;
        info.coverView = GetComponent<PlayableBook>().coverView;
        info.shelfParentID = transform.parent.GetComponent<Shelf>().shelfID;
        info.parentsOrder = transform.GetSiblingIndex();

        bookshelvesData.Books.Add(info);
    }

}
