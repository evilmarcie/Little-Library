using UnityEngine;

   public interface ISaveGame
    {
        public void SaveGame(ref GameData gameData);
        public void LoadGame(GameData gameData);
    }

    public interface ISaveCounter
    {
        public void SaveCounter(ref CounterData counterData);
        public void LoadCounter(CounterData counterData);
    }

    public interface ISaveShelves
    {
        public void SaveShelves(ref BookshelvesData bookshelvesData);
        public void LoadShelves(BookshelvesData bookshelvesData);
    }
