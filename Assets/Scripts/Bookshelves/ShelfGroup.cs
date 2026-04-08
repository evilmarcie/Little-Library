using UnityEngine;

public class ShelfGroup : MonoBehaviour
{
    public static ShelfGroup shelfGroup;
    void Awake()
    {
        if (shelfGroup == null)
        {
            shelfGroup = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
