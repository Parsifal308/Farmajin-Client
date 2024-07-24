using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PauseAcces : MonoBehaviour
{
    public GameObject pauseMenu;
    public Timer timer;
    public GameObject Restart;
    public GameObject Restart2;
    public GameObject Next; 
    public Cambiar cambiar;
    public GameObject playButton;
    public Button pauseButton;

    public Money money;

    public void openPauseMenu()
    {
        timer.StopTimer();
        money.Penalize();
        pauseMenu.SetActive(true);
        Restart.SetActive(false);
        Next.SetActive(false);  
        cambiar.Continue(); 
        Restart2.SetActive(true);
        playButton.SetActive(true);
        
    }

    public void closePauseMenu()
    {
        pauseMenu.SetActive(false);
        timer.RestarTime();
        pauseButton.interactable = true;
        playButton.SetActive(false);
        //pauseButton.SetActive(true);

    }

    public void FinalMenu()
    {
        timer.StopTimer();
        pauseMenu.SetActive(true);
        Restart.SetActive(true);
        Restart2.SetActive(false);
        Next.SetActive(true);
    }
}
