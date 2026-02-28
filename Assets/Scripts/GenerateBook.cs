using System.Collections;
using UnityEngine;

public class GenerateBook : MonoBehaviour
{
    public GameObject book;

    public void generateBook()
    {
        Instantiate(book);
    
    }
}
