using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerPreguntados : MonoBehaviour
{
    //public GameObject PauseComponent;
    public Text timerText;
    public Image progressBar;
    public GameObject timerObject;
    public AudioSource Audio_Failed;
    public QuizManager quizManager;


    //public tabsScreen tabsScreen;
    public float timeRemaining = 0;
    private float initialTime;
    private bool timerIsRunning = false;


    private void Start()
    {
        initialTime = timeRemaining;
        timerIsRunning = true;
        Audio_Failed.Stop();
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
            }
            else
            {
                Debug.Log("Time has run out!");
                //quizManager.TimeExpired();

            // Llamar a una función en el QuizManager para marcar la pregunta como mala y pasar a la siguiente pregunta
                if (!quizManager.IsQuestionAnswered())
                {
                    quizManager.TimeExpired();
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
        timeRemaining = 15f;
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
    public void ResumeTimer()
    {
        timerIsRunning = true;
    }
}