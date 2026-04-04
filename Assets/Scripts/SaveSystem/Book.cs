using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Book : MonoBehaviour
{
    public BookData bookdata;
    public string coverSpriteID;
    public string spineSpriteID;
    public Image coverImage;
    public Image spineImage;
    public void SetCoverSprite(Sprite sprite, string id)
    {
        coverSpriteID = id;
        //coverImage.sprite = sprite;

    }

    public void SetSpineSprite(Sprite sprite, string id)
    {
        spineSpriteID = id;
        //spineImage.sprite = sprite;
    }
}
