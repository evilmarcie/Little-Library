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
}


