using UnityEngine;

public class CoreManager : MonoBehaviour
{
    void Start()
    {
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.Menu, SceneDatabase.Scenes.Home)
            .Perform();
    }
  
}
