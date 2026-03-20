using UnityEngine;

public class DatabaseCanvas : MonoBehaviour
{
    public static DatabaseCanvas UIoverlays;

    void Awake()
    {
        if (UIoverlays == null)
        {
            UIoverlays = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
       
    }
}
