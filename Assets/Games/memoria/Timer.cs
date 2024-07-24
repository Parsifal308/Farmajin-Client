using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //public GameObject PauseComponent;
    public Text timerText;
    public Image progressBar;
    public Money money;
    public GameObject NextBotton;
    public GameObject pauseMenu;
    public Cambiar cambiar;
    public GameObject Restar;
    public GameObject Restar2;
    public GameObject Continue;
    public GameObject timerObject;
    public AudioSource Audio_Failed;
    public GameController gameController;
    public Button pauseButton;
    public GameObject GameOverMenu;


    //public tabsScreen tabsScreen;
    public int NuevoTime = 0;
    public float timeRemaining = 0;
    private float initialTime;
    private bool timerIsRunning = false;
    public GameObject playButton;

    public void Start()
    {
        initialTime = timeRemaining;
        timerIsRunning = true;
            // Establecer el color inicial de la barra de progreso a verde
        progressBar.color = Color.green;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timerText.text = "Tiempo: " + Mathf.Round(timeRemaining).ToString();
                // Calcular el factor de interpolación en función del progreso del tiempo
                float progress = timeRemaining / initialTime;
                // Interpolar entre verde y rojo
                progressBar.color = Color.Lerp(Color.green, Color.red, 1f - progress);

                progressBar.fillAmount = progress;
                //progressBar.fillAmount = timeRemaining / initialTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                cambiar.Incomplete();
                money.Penalize();
                //pauseMenu.SetActive(true);
                GameOverMenu.SetActive(true);
                StopTimer();
                //pauseMenu.SetActive(true);
                pauseButton.interactable = false;
                playButton.SetActive(false);


                if (gameController.yes_audio.activeSelf)
                    Audio_Failed.Play();
                else{
                    Audio_Failed.Stop();
                }


            }
        }
        
    }
    public void StopTimer()
    {
        timerIsRunning = false;
        
    }
    public void RestarTime()
    {
        timerIsRunning = true;
    }

    public void newTime()
    {
        timeRemaining = NuevoTime;
        initialTime = timeRemaining;

    }

     public void timerOff()
    {
        timerObject.SetActive(false); // Desactiva el objeto

        
    }
     public void timerOn()
    {
        
       timerObject.SetActive(true); // activa el objeto
        
    }

}

