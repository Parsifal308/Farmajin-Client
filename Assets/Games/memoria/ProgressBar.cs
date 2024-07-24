using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    public Image fill;
    public TextMeshProUGUI amount;
    public int currentValue;

    public GameController gameController;

    public int TotalCards;
    public void Start()
    {
       fill.fillAmount = Normalise();
        Debug.Log(amount.text = $"{currentValue}/{TotalCards}"); 
       
    }

    public void Add()
    {
       currentValue = gameController.ronda;
        if(currentValue > TotalCards)
        {currentValue = TotalCards;}

         fill.fillAmount = Normalise();
         amount.text = $"{currentValue}/{TotalCards}";
    }
    private float Normalise()
    { 
        currentValue = gameController.ronda;
        return (float)currentValue / TotalCards ;
    }

   
}
