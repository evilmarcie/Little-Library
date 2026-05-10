using UnityEngine;

public class PersistentCamera : MonoBehaviour
{
    public static Camera Instance;

    void Awake()
    {
        Instance = GetComponent<Camera>();
        DontDestroyOnLoad(gameObject);
    }
}
