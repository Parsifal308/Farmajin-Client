using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private bool isAudioEnabled = true;

    public Sprite bgImage;
    public GameObject Carta2;
    public GameObject Carta1;
    public GameObject Carta3;
    public int monedas = 1;
    public int reward = 0;
    public AudioSource Audio_Match;
    public AudioSource Audio_Completed;
    public GameObject Text_Activity;
    public Button pauseButton;
    public GameObject playButton;
    public GameObject GameOverMenu;
    public int numCards = 2;
    public int ronda;
    public ProgressBar progressBar;
    public LayoutSize layoutSize;
    public Cambiar cambiar;
    public Money money;
    public Timer timer;
    public AddButtons addButtons;
    public GameObject pauseMenu;
    public GameObject yes_audio;
    public GameObject no_audio;
    public PauseAcces pauseAcces;
    public Sprite[] puzzles;

    public List<Sprite> gamePuzzles = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    private bool firstGuest, secondGuest;

    public int countGuesses;
    public int countCorrectGuesses;     //count correct card pairs 
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;
    private bool firstGuess, secondGuess;


    public void Start()
    {
        InitializeGame();    
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;   
        }
    }

void AddgamePuzzles(int numCards)
{
    int totalPuzzles = puzzles.Length;
    List<Sprite> availablePuzzles = new List<Sprite>(puzzles);

    // Duplicar las imágenes de los rompecabezas
    List<Sprite> duplicatedPuzzles = new List<Sprite>(puzzles);
    duplicatedPuzzles.AddRange(puzzles);

    for (int i = 0; i < numCards / 2; i++)
    {
        int randomIndex = Random.Range(0, availablePuzzles.Count);
        Sprite randomPuzzle = availablePuzzles[randomIndex];
        gamePuzzles.Add(randomPuzzle);
        gamePuzzles.Add(randomPuzzle);
        availablePuzzles.RemoveAt(randomIndex);

        // Si se agotan las imágenes disponibles, reiniciar la lista
        if (availablePuzzles.Count == 0)
        {
            availablePuzzles = new List<Sprite>(puzzles);
        }
    }

    // Barajar las imágenes de los rompecabezas en cada ronda
    for (int i = 0; i < gamePuzzles.Count; i++)
    {
        int randomIndex = Random.Range(i, gamePuzzles.Count);
        Sprite temp = gamePuzzles[i];
        gamePuzzles[i] = gamePuzzles[randomIndex];
        gamePuzzles[randomIndex] = temp;
        //Debug.Log("gamePuzzles Count: " + gamePuzzles.Count);


    }
}

    void AddListeners()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }
    public void OnnoAudio()
    {
        no_audio.SetActive(true);
        yes_audio.SetActive(false);
        isAudioEnabled = false;
    }
    
    public void OnyesAudio()
    {
        yes_audio.SetActive(true);
        no_audio.SetActive(false);
        isAudioEnabled = true;
    }

    public void PickAPuzzle()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("Picked Puzzle: " + name); // Agregar mensaje de depuración
        //Debug.Log(name);
        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
            //Debug.Log("First Guess Index: " + firstGuessIndex+"gggggg"+firstGuessPuzzle);

        }
        else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            //Debug.Log("Second Guess Index: " + secondGuessIndex +"fffff" + secondGuessPuzzle);
            countGuesses++;
            StartCoroutine(CheckIfThePuzzleMatch());
        }
    }

 IEnumerator CheckIfThePuzzleMatch()
{
    yield return new WaitForSeconds(0f);
    Debug.Log("gamePuzzles Count: " + gamePuzzles.Count);
    Debug.Log("First Guess Index: " + firstGuessIndex);
    Debug.Log("Second Guess Index: " + secondGuessIndex);

    if (firstGuessIndex >= 0 && firstGuessIndex < gamePuzzles.Count &&
        secondGuessIndex >= 0 && secondGuessIndex < gamePuzzles.Count)
    {
        firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
        secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

        btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
        btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
    }
    else
    {
        if(firstGuessIndex>=gamePuzzles.Count){
            int aux1 = firstGuessIndex - gamePuzzles.Count;
            firstGuessIndex = firstGuessIndex - (aux1+1);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
        }
        else if(secondGuessIndex>=gamePuzzles.Count){
            int aux2 = secondGuessIndex - gamePuzzles.Count;
            secondGuessIndex = secondGuessIndex - (aux2+1);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

        }
        
/*         Debug.LogError("Index out of range!");
        yield break; */
    }

    if (firstGuessIndex != secondGuessIndex && firstGuessPuzzle == secondGuessPuzzle)
    {
        yield return new WaitForSeconds(0.2f);

        btns[firstGuessIndex].interactable = false;
        btns[secondGuessIndex].interactable = false;

        btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
        btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

        CheckIfTheGameIsFinished();

        if (isAudioEnabled)
            Audio_Match.Play();
        else
            Audio_Match.Stop();
    }
    else
    {
        yield return new WaitForSeconds(0.2f);

        Handheld.Vibrate();
        yield return new WaitForSeconds(0.1f);
        Handheld.Vibrate();

        btns[firstGuessIndex].image.sprite = bgImage;
        btns[secondGuessIndex].image.sprite = bgImage;
    }

    firstGuess = secondGuess = false;
}


   
    void CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;
        //Debug.Log(countCorrectGuesses);
        
        if (countCorrectGuesses == gameGuesses)
        {
            timer.timeRemaining=timer.NuevoTime+5;
            timer.NuevoTime+=5;
            timer.newTime();
            //progressBar.Add();

            gameGuesses = 0;
            countCorrectGuesses=0;
            ronda++;
            numCards = ronda * 2;
            reward+=monedas;
            Text_Activity.SetActive(false);
            if(numCards > 8){
                layoutSize.ModificarGridLayoutProperties3();
            }
            if(numCards>12){
                layoutSize.ModificarGridLayoutProperties4();
            }
            //Debug.Log("Starting round " + ronda + " with " + numCards + " cards per round");

            if(ronda<=progressBar.TotalCards)
            {

                //timer.Start();
                progressBar.Add();
                IniciarRonda(numCards);
            }
            else{
              
                money.Reward();
                timer.StopTimer();
                Debug.Log("Game Finished:" + countGuesses);
                //tabsScreen.timerOff();
                cambiar.Complete();
                pauseMenu.SetActive(true);
                pauseButton.interactable = false;
                pauseAcces.FinalMenu();
                Audio_Match.Stop();


                if (no_audio.activeSelf)
                {
                Audio_Completed.Stop();
                }
                else{Audio_Completed.Play();}
            }

        }
    }

    public void ResetData()
    {
        gamePuzzles.Clear();
       
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
            Destroy(objects[i]);
        }
        btns.Clear();
    }

    void IniciarRonda(int numCartasRonda)
    {
        ResetData();
        pauseButton.interactable = true;
        playButton.SetActive(false);
        addButtons.Buttons(numCartasRonda);
        GetButtons();
        AddgamePuzzles(numCartasRonda);
        AddListeners();
        gameGuesses = gamePuzzles.Count / 2;
    }

public void RestartLevel()
{
    layoutSize.ModificarGridLayoutProperties2();
    gameGuesses = 0;
    countCorrectGuesses=0;
    timer.NuevoTime = 5;
    timer.timeRemaining = 5;
    reward = 0;
    ronda = 1;
    
    numCards = ronda * 2;
    ResetData();
    GameOverMenu.SetActive(false);
    //pauseMenu.SetActive(false);
    timer.newTime();
    timer.RestarTime();
    Start();
    progressBar.Start();
}

    private void InitializeGame()
    {
        layoutSize.ModificarGridLayoutProperties2();
        pauseButton.interactable = true;
        playButton.SetActive(false);
        addButtons.Buttons(numCards);
        GetButtons();
        AddgamePuzzles(numCards);
        AddListeners();
        gameGuesses = gamePuzzles.Count / 2;
    }
//------------------------------------------------------
public void Oncomodin()
{
    StartCoroutine(ComodinCoroutine());
}

IEnumerator ComodinCoroutine()
{
    // Desactivar las cartas
    Carta1.SetActive(false);
    Carta2.SetActive(true);

    // Dar la vuelta a todas las cartas
    for (int i = 0; i < btns.Count; i++)
    {
        btns[i].image.sprite = gamePuzzles[i];
    }

    // Esperar un tiempo antes de regresar a la posición original
    yield return new WaitForSeconds(0.3f);

    // Regresar las cartas a la posición original
    for (int i = 0; i < btns.Count; i++)
    {
        btns[i].image.sprite = bgImage;
    }

    // Activar las cartas nuevamente
    Carta1.SetActive(true);
}

public void Oncomodin2()
{
    StartCoroutine(MatchFirstCardCoroutine());
}
IEnumerator MatchFirstCardCoroutine()
{
    // Esperar un tiempo antes de mostrar la segunda carta
    yield return new WaitForSeconds(0.5f);

    // Mostrar la segunda carta que coincide con la primera carta seleccionada
    for (int i = 0; i <btns.Count; i++)
    {
        if (gamePuzzles[i].name == firstGuessPuzzle)
        {
            btns[i].image.sprite = gamePuzzles[i];
        }
    }

    // Esperar un tiempo antes de regresar a la posición original
    yield return new WaitForSeconds(0.5f);

    // Regresar las cartas a la posición original

    for (int i = 0; i < btns.Count; i++)
    {

        btns[i].image.sprite = bgImage;
    }

}

}
//--------------------- Audio-----------------