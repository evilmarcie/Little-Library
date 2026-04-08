using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
    public class SpriteInfo
    {
        public string id;
        public Sprite cover;
        public Sprite spine;
    }

    public class SpriteManager : MonoBehaviour
{
    public List<SpriteInfo> BookSprites;

    private Dictionary<string, SpriteInfo> lookup;

    void Awake()
    {
        instance = this;
        
        lookup = new Dictionary<string, SpriteInfo>();
        
        foreach (var spriteInfo in BookSprites)
        {
            lookup [spriteInfo.id] = spriteInfo;
        }
    }

    public SpriteInfo GetSpriteInfo(string id)
    {
        return lookup[id];
    }

    public static SpriteManager instance;
}




