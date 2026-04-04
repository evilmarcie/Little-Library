using System.Collections;
using UnityEngine;

public class GenerateBook : MonoBehaviour
{
    public GameObject book;
    public GameObject holder;

    public void generateBook()
    {
        Instantiate(book, holder.transform);
    }
}
