using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStars : MonoBehaviour
{
    [SerializeField]
    public Transform puzzleField;
    
    [SerializeField]
    private GameObject btn;
    public int numCards;
    //public GameObject NextLevel;


    public void Buttons(int numCards)
    {
        for (int i = 0; i < numCards; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(puzzleField, false);
        }
        
    }

}


