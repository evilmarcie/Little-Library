using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
    public class SpriteInfo
    {
        public string id;
        public Sprite sprite;
    }

public class SpriteManager : MonoBehaviour
{
    public List<SpriteInfo> CoverSprites;
    public List<SpriteInfo> SpineSprites;
    private Dictionary<string, Sprite> lookup;

    void Awake()
    {
        lookup = new Dictionary<string, Sprite>();
        
        foreach (var s in CoverSprites)
        {
            lookup [s.id] = s.sprite;
        }
    }

    public Sprite GetSprite(string id)
    {
        return lookup[id];
    }

    public static SpriteManager Instance;

    void OnEnable()
    {
        Instance = this;
    }
}
