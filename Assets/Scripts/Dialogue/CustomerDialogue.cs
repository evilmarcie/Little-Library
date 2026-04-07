using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerDialogue : MonoBehaviour
{
    public Character customer;

    void Awake()
    {
        Character customer = gameObject.GetComponent<Character>();
    }

    public void Greeting()
    {
        //random greeting
    }

    public void RecieveBook(BookData book)
    {
         if (customer.lovedBooks.Contains(book))
        {
            //random loved response
        }
        else if (customer.likedBooks.Contains(book))
        {
            //random liked response
        }
        else if (customer.neutralBooks.Contains(book))
        {
            //random neutral response
        }
        else if (customer.dislikedBooks.Contains(book))
        {
            //random disliked response
        }
        else
        {
            Debug.Log("error, book not found in lists");
        }
    }
}
