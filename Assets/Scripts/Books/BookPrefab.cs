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

<<<<<<< Updated upstream
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
=======
    public bool loadingFromSave = false;

    void Start()
    {
        if (loadingFromSave == false)
        {
            RandomValues();
            SetValues();
        }
    }

    public void RandomValues()
    {
        book = books[UnityEngine.Random.Range(0, books.Length)];
        sprites = SpriteManager.instance.BookSprites;
        spriteNumber = UnityEngine.Random.Range(0, sprites.Count);
        spriteInfo = sprites[spriteNumber];
    }

    public void SetValues()
    {
        coverImage.sprite = spriteInfo.cover;
        spine.sprite = spriteInfo.spine;
        spriteID = spriteInfo.id;
>>>>>>> Stashed changes
        
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

<<<<<<< Updated upstream
=======
    public void SaveShelves(ref BookshelvesData bookshelvesData)
    {
        BookshelvesData.BookInfo info = new BookshelvesData.BookInfo();

        info.bookID = book.bookID;
        info.spritesID = spriteID;

        PlayableBook playableBook = GetComponent<PlayableBook>();
        info.coverView = playableBook.coverActiveView;

        info.shelfParentID = transform.parent.GetComponent<Shelf>().shelfID;
        info.parentsOrder = transform.GetSiblingIndex();

        info.onShelf = playableBook.onShelf;

        bookshelvesData.Books.Add(info);

        Destroy(this.gameObject);
    }

>>>>>>> Stashed changes
}
