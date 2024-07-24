using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfMenuPreguntados : MonoBehaviour
{
    public GameObject confPanel, salirButton, seguirButton;
    public TimerPreguntados timer;

    public void MenuOn(){
        confPanel.SetActive(true);
        timer.StopTimer();

    }
    public void MenuOff(){
        confPanel.SetActive(false);
        timer.RestarTime();
    }
}
