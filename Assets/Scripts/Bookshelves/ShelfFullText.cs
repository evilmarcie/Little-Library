using UnityEngine;
using UnityEngine.UIElements;

public class ShelfFullText : MonoBehaviour
{
    public float destroyTime = 5;

    void Start()
    {
        //RectTransform rt = GetComponent<RectTransform>();
        //rt.localPosition += new Vector3 (10, 10, 0);
        
        Destroy(gameObject, destroyTime);

        
    }
}
