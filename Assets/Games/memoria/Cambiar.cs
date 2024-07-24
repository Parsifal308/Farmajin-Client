using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cambiar : MonoBehaviour
{
    // Start is called before the first frame update
   
    public TextMeshProUGUI CambiarMensaje;
    
    public void Incomplete()
    {
        CambiarMensaje.text = "Game Over";
    }

    // Update is called once per frame
    public void Complete()
    {
         CambiarMensaje.text = "Felicidades!";
    }
    public void Continue()
    {
        CambiarMensaje.text  = "Pausa";
    }
}
