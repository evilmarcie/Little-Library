using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class bookPrefab : MonoBehaviour, ISaveShelves, ISaveGame
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
    public string bookID;

    //rendering book gameobject
    public Image coverImage;
    public TextMeshProUGUI title;
    public TextMeshProUGUI author;

    //spine
    public Image spine;
    public TextMeshProUGUI spineTitle;

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
        
        title.text = book.bookTitle;
        spineTitle.text = book.bookTitle;

        author.text = book.authorName;

        gameObject.name = book.bookTitle.ToString();
        
        bookID = book.bookID;

    }

    public void SaveShelves(ref BookshelvesData bookshelvesData)
    {
        BookshelvesData.BookInfo info = new BookshelvesData.BookInfo();

        PlayableBook playableBook = GetComponent<PlayableBook>();
        
        if (playableBook.onShelf == true)
        {
            info.bookID = book.bookID;
            info.spritesID = spriteID;
        
            info.coverView = playableBook.coverActiveView;

            info.shelfParentID = transform.parent.GetComponent<Shelf>().shelfID;
            info.parentsOrder = transform.GetSiblingIndex();

            info.onShelf = playableBook.onShelf;

            bookshelvesData.Books.Add(info);

            Destroy(this.gameObject);
        }
        else return;
    }

    public void LoadShelves(BookshelvesData bookshelvesData)
    {
        
    }

    public void SaveGame(ref GameData gameData)
    {
        GameData.BookInfo info = new GameData.BookInfo();
        PlayableBook playableBook = GetComponent<PlayableBook>();
        
        if (playableBook.onShelf == true)
        {
            info.bookID = book.bookID;
            info.spritesID = spriteID;
        
            info.coverView = playableBook.coverActiveView;

            info.shelfParentID = transform.parent.GetComponent<Shelf>().shelfID;
            info.parentsOrder = transform.GetSiblingIndex();

            info.onShelf = playableBook.onShelf;

            gameData.Books.Add(info);

            Destroy(this.gameObject);
        }
        else return;
    }

    public void LoadGame(GameData gameData)
    {
        
    }
}
