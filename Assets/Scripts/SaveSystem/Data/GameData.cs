using System;
using System.Collections.Generic;

[Serializable]
public class BookSaveData
{
  public string bookID;
  public string coverSpriteID;
  public string spineSpriteID;
  public string shelfID;
  public int siblingIndex;
  public bool coverView;
}

[Serializable]
public class GameData
{
    public List<BookSaveData> books = new List<BookSaveData>();
}
