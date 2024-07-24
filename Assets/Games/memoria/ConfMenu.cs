using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfMenu : MonoBehaviour
{
    public GameObject confPanel, salirButton, seguirButton;
    public Timer timer;

    public void MenuOn(){
        confPanel.SetActive(true);
        timer.StopTimer();

    }
    public void MenuOff(){
        confPanel.SetActive(false);
        timer.RestarTime();
    }
}
