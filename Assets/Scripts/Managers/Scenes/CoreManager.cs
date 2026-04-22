using UnityEngine;

public class CoreManager : MonoBehaviour
{
    void Start()
    {
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.Menu, SceneDatabase.Scenes.Home)
            .Load(SceneDatabase.Slots.UI, SceneDatabase.Scenes.UI)
            .Perform();
        
        SaveManager.instance.LoadGame();
    }
  
}
