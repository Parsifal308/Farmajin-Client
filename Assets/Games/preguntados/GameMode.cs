using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
public enum SetGameModes
{
    PREGUNTADOS,
    QUIZ
}

public SetGameModes currentGameMode = SetGameModes.PREGUNTADOS; // Por defecto, inicia en el modo QUIZ

public void SetGameModeToPreguntados()
{
    currentGameMode = SetGameModes.PREGUNTADOS;
}

public void SetGameModeToQuiz()
{
    currentGameMode = SetGameModes.QUIZ;
}
}
