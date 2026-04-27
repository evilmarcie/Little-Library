using UnityEngine;

public class MusicManager : MonoBehaviour, ISaveGame
{
    public static AudioSource music;

    void Awake()
    {
        music = GetComponent<AudioSource>();
    }
    public void SaveGame(ref GameData gameData)
    {
        gameData.musicVolume = (int)music.volume;
    }

    public void LoadGame(GameData gameData)
    {
        //music.volume = gameData.musicVolume;
    }
}
