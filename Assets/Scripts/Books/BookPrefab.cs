using System;
using System.Collections.Generic;
using System.Linq;
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
        RandomValues();
        SetValues();
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
        
        title.text = book.bookTitle;
        spineTitle.text = book.bookTitle;

        author.text = book.authorName;

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

        PlayableBook playableBook = GetComponent<PlayableBook>();
        info.coverView = playableBook.coverActiveView;

        info.shelfParentID = transform.parent.GetComponent<Shelf>().shelfID;
        info.parentsOrder = transform.GetSiblingIndex();

        bookshelvesData.Books.Add(info);
    }

}
