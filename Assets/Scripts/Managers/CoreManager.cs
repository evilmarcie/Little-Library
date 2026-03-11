using UnityEngine;

public class CoreManager : MonoBehaviour
{
    void Start()
    {
        // load audio manager, set up save system, etc.

        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.Menu, SceneDatabase.Scenes.Home)
            .Perform();
    }

   
}
