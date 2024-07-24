using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using Farmanji.Managers;
using UnityEngine.UI;
using TMPro;

using Farmanji.Data;

public class QuizManager : MonoBehaviour
{
    public AddStars addStars;
    public AddStars_Inicio addStars_inicio;
    public GameObject EndMenuQuiz;
    public GameObject EndMenuPreguntados;
    

    [SerializeField] public QuizUI quizUI;
    [SerializeField] private GameObject failObject;
    [SerializeField] private GameObject goodObject;
    
    // Variable sonido
    public AudioSource Audio_Correct_Answer;
    public AudioSource bad_answer;


    private List<Question> questions;
// Array de QuizdataScripttable para almacenar los cuestionarios
    public QuizdataScripttable[] quizDatas;
    private Question selectedQuestion;


    public GetActivities getActivities;
    public GetActivitiesQuiz getActivitiesQuiz;


    public int val = 0;
    public int valstar = 1;

    /* Timer */
    public TimerPreguntados timer;

    //Varaible para las estrellas 
    [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;
    public Transform[] starObjects;
    //Variable estrellas final
    public HorizontalLayoutGroup endMenuLayoutGroup;
    public HorizontalLayoutGroup EndStartPreguntados;
    //Variable para tomar en cuenta las respuestas correctas e incorrectas
    public int c_answer=0;
    private int f_answer=0;
    //Monedas
    public TextMeshProUGUI TextMoneyQuiz;
    public TextMeshProUGUI TextGemsQuiz;
    public TextMeshProUGUI TextMoneyPreguntados;
    public TextMeshProUGUI TextGemsPreguntados;

    //Variable del Texto de ENDMENU 
    public TextMeshProUGUI textComponentEndMenu;
    public TextMeshProUGUI textAsoMejorar;

    //Barra del tiempo
    public GameObject Bartime;


    public GameObject volume;
    public GameObject mute;
    private bool isAudioEnabled = true;
    //Nuevas variables
    private bool questionAnswered = false; // Variable para verificar si la pregunta actual ha sido respondida
    //Vista de Tests
    public GameObject testsview;
    //Pantallas de preguntas
    public GameObject Questionsview;

    //Select Reto or Classic

    public GameObject SelectRetoClassic;

    public TextMeshProUGUI JuegoTextTitle;
    //Variable del test seleccionado
    private int selectedQuizIndex = -1;
    //Variable para el numero de ronda
    public TextMeshProUGUI number_ronda;
    public Text testname1;
    public GameMode gameMode;

    // Start is called before the first frame update
    public TestListQuiz testListQuiz;

    //public TestList testsList;
    public void Start()
    {
        timer.timerOff();
        if (gameMode.currentGameMode == GameMode.SetGameModes.PREGUNTADOS){
             getActivities.LoadData();
        }
        else{
            getActivitiesQuiz.LoadData();
            //List<Dictionary<string, object>> testsList = levelsLoader.TestsList;
 
            //foreach(var item in testsList)
            {
                //testListQuiz.CreateQuestion(testsList[0]);
            }
            
        }
       
    }
    
    // Función para cuando de clic en play
    public void OnPlayButtonClicked(List<Question> questionsLista)
    { 
        //if (quizDataIndex >= 0 && quizDataIndex < quizDatas.Length)
        {



            //questions = new List<Question>(getActivities.AllQuestions);
            questions = questionsLista;



            ShuffleQuestions();
            SelectQuestion();
            addStars_inicio.Buttons(questions.Count);
            addStars.Buttons(questions.Count);
            starObjects = horizontalLayoutGroup.GetComponentsInChildren<Transform>();
            bad_answer.Stop();
            // Vista de preguntas
            OnQuestionsViews();
            // Vista test
            OffTestView();
            //Final menu
            EndMenuQuiz.SetActive(false);

            // Tiempo
            timer.timerOn();
        }

    }
    public void OnSelectRetoClassic()
    {
        SelectRetoClassic.SetActive(true);
        
    }
    public void OnTestView(int seleccionar){
        if (seleccionar == 1){
            JuegoTextTitle.text = "Juegos Desafío";
        } else{
            JuegoTextTitle.text = "Juegos Clásicos";
        }
        testsview.SetActive(true);
        SelectRetoClassic.SetActive(false);

    }
    public void OffTestView(){
        testsview.SetActive(false);
        
    }
    public void OnQuestionsViews(){
        Questionsview.SetActive(true);
    }
    public void OffQuestionsViews(){
    Questionsview.SetActive(false);
    }
    public void OnTestView(){
        testsview.SetActive(true);
        
    }
    //Funcion auxiliar para ver si la respuesta es correcta o no
    public bool IsQuestionAnswered()
    {
        return questionAnswered;
    }
    //Funcion que hace que cuando el tiempo se acabe se marque como incorrecta
    public void TimeExpired()
    {
        // Mostrar el objeto "fail"
        GameObject failObject = starObjects[valstar].transform.Find("fail").gameObject;
        failObject.SetActive(true);
        f_answer += 1;
        if (isAudioEnabled) // Verificar si el botón de sonido está activado
        {
            bad_answer.Play();
        }
        else
        {
            bad_answer.Stop();
        }
        questionAnswered = true; // Marcar la pregunta actual como respondida
        Invoke("SelectQuestion", 0.4f);
        if (gameMode.currentGameMode == GameMode.SetGameModes.PREGUNTADOS)
        {
            ReturnHome(EndMenuPreguntados);
        }
        else if (gameMode.currentGameMode == GameMode.SetGameModes.QUIZ)
        {
            ReturnHome(EndMenuQuiz);
        }
        else
        {
            Debug.LogError("Modo de juego no válido.");
        }
    }
    
    public void OffAudio()
    {
        mute.SetActive(true);
        volume.SetActive(false);
        isAudioEnabled = false;
    }
    
    public void OnAudio()
    {
        volume.SetActive(true);
        mute.SetActive(false);
        isAudioEnabled = true;
    }


    //Funcion que barajea las preguntas
    void ShuffleQuestions()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            int randomIndex = Random.Range(i, questions.Count);
            Question temp = questions[randomIndex];
            questions[randomIndex] = questions[i];
            questions[i] = temp;
        }
    }


    //Funcion para seleccionar una pregunta
    public void SelectQuestion()
    {
        if (val >= questions.Count)
        {
            ReturnHome(EndMenuQuiz);    
            // Todas las preguntas han sido respondidas
            // Realiza acciones adicionales o finaliza el juego
            return;
        }
        questionAnswered = false; // Restablecer la variable para la nueva pregunta
        selectedQuestion = questions[val];
        quizUI.SetQuestion(selectedQuestion);
        timer.newTime();
    } 


    //Funcion de opcion correcta
    public bool Answer(string answered)
    {
        bool correctAns = false;
        if (answered == selectedQuestion.correctAns)
        {
            correctAns = true;
            // Mostrar el objeto "good"
            GameObject goodObject = starObjects[valstar].transform.Find("good").gameObject;
            goodObject.SetActive(true);
            if (isAudioEnabled){
                Audio_Correct_Answer.Play();
            }
            else{Audio_Correct_Answer.Stop();}
            c_answer+=1;
        }
        else
        {
            // Mostrar el objeto "fail"
            GameObject failObject = starObjects[valstar].transform.Find("fail").gameObject;
            failObject.SetActive(true);
            f_answer+=1;
            if (isAudioEnabled){
                bad_answer.Play();
            }
            else{bad_answer.Stop();}
        }
        questionAnswered = true; // Marcar la pregunta actual como respondida
        
        //Debug.Log("VAL: "+val);
        Invoke("SelectQuestion", 0.4f);
        if (gameMode.currentGameMode == GameMode.SetGameModes.PREGUNTADOS)
        {
            ReturnHome(EndMenuPreguntados);
        }
        else if (gameMode.currentGameMode == GameMode.SetGameModes.QUIZ)
        {
            ReturnHome(EndMenuQuiz);
        }
        else
        {
            Debug.LogError("Modo de juego no válido.");
        }
        return correctAns;
    }
    public void ReturnHome( GameObject FinalGame)
    {
        //Debug.Log("Cantidad de preguntas: " + 1);
        
        val++;
        valstar ++;
        Debug.Log("Cantidad de preguntas hasta ahora es de : " + val);
        if (val==questions.Count)
        {
            textComponentEndMenu.text="¡TERMINASTE!";
            textAsoMejorar.text="¡TERMINASTE!";
            
            CopyLayoutGroup(horizontalLayoutGroup, endMenuLayoutGroup);
            CopyLayoutGroup(horizontalLayoutGroup, EndStartPreguntados);

            FinalGame.SetActive(true);
            timer.StopTimer();
            //Numero de monedas
            
            TextMoneyQuiz.text = "+" + ((ResourcesLoaderManager.Instance.Worlds.CurrentActivity.CoinsReward / questions.Count) * c_answer).ToString();
            TextGemsQuiz.text = "+" + ((ResourcesLoaderManager.Instance.Worlds.CurrentActivity.GemsReward / questions.Count) * c_answer).ToString();
            TextMoneyPreguntados.text = "+" + ((ResourcesLoaderManager.Instance.Worlds.CurrentActivity.CoinsReward / questions.Count) * c_answer).ToString();
            TextGemsPreguntados.text = "+" + ((ResourcesLoaderManager.Instance.Worlds.CurrentActivity.GemsReward / questions.Count) * c_answer).ToString();
            
            // EconomyBody body = EconomyBody.CreateCurrenciesBody(c_answer * 5, 0);
            // StartCoroutine(ResourcesLoaderManager.Instance.Economy.GetComponent<EconomyPost>().Post(body));

            if (f_answer <= 1) //ganador de la vida
            {
                ResourcesLoaderManager.Instance.Worlds.CurrentActivity.CompleteActivity();
                ResourcesLoaderManager.Instance.Worlds.CompleteCurrentActivity(c_answer / questions.Count);
            }
            CurrencyManager.Instance.AddCoins((ResourcesLoaderManager.Instance.Worlds.CurrentActivity.CoinsReward / questions.Count) * c_answer);
            CurrencyManager.Instance.AddGems((ResourcesLoaderManager.Instance.Worlds.CurrentActivity.GemsReward / questions.Count) * c_answer);
        }
    }

    //Funcion para copiar las strellas
    private void CopyLayoutGroup(HorizontalLayoutGroup sourceLayoutGroup, HorizontalLayoutGroup targetLayoutGroup)
    {
        // Borrar todos los elementos hijos del targetLayoutGroup
        foreach (Transform child in targetLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }

        // Copiar cada elemento del sourceLayoutGroup al targetLayoutGroup
        foreach (Transform child in sourceLayoutGroup.transform)
        {
            GameObject newChild = Instantiate(child.gameObject, targetLayoutGroup.transform);
            newChild.transform.SetParent(targetLayoutGroup.transform);
        }

        // Ajustar las propiedades del targetLayoutGroup para que refleje el mismo comportamiento que el sourceLayoutGroup
        targetLayoutGroup.childControlWidth = sourceLayoutGroup.childControlWidth;
        targetLayoutGroup.childControlHeight = sourceLayoutGroup.childControlHeight;
        targetLayoutGroup.childForceExpandWidth = sourceLayoutGroup.childForceExpandWidth;
        targetLayoutGroup.childForceExpandHeight = sourceLayoutGroup.childForceExpandHeight;
    }

    //Funcion para reiniciar el juego
    public void RestartGame()
    {
        {
            // Reiniciar las variables para el nuevo juego
            val = 0;
            valstar = 1;
            c_answer = 0;
            f_answer = 0;
            questionAnswered = false;

            // Reiniciar el tiempo
            //timer.newTime();
            timer.timerOn();
            timer.RestarTime();

            // Reiniciar las estrellas
            foreach (Transform child in addStars_inicio.puzzleField)
            {
                child.gameObject.SetActive(false);
                Destroy(child.gameObject);
            }
            // Reiniciar la pregunta actual
            //questions = new List<Question>(getActivities.AllQuestions);
            ShuffleQuestions();
            SelectQuestion();
            // Agregar las estrellas al inicio
            addStars_inicio.Buttons(questions.Count);
            addStars.Buttons(questions.Count);
            starObjects = horizontalLayoutGroup.GetComponentsInChildren<Transform>();

            // Mostrar la vista de preguntas y ocultar el menú finalizado
            Questionsview.SetActive(true);
            if (gameMode.currentGameMode == GameMode.SetGameModes.PREGUNTADOS)
            {
                EndMenuPreguntados.SetActive(false);
            }
            else if (gameMode.currentGameMode == GameMode.SetGameModes.QUIZ)
            {
                EndMenuQuiz.SetActive(false);
            }
            else
            {
                Debug.LogError("Modo de juego no válido.");
            }
        }
     /*    else
        {
            Debug.LogError("El índice del cuestionario seleccionado no es válido.");
        } */
    }
     
}



[System.Serializable]
public class Question
{
    public string questionInfo;

    public Sprite questionImg;
    public AudioClip questionClip;
    public VideoClip questionVideo;

    public string correctAns;
    public List<string> options;
    public QuestionType questionType;
    
}



[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE,
    VIDEO,
    AUDIO
}

