using UnityEngine;

public interface ISaveData
{
    void LoadData(GameData data);
    void SaveData(ref GameData data);
}
