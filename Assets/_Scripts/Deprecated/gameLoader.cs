using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameLoader : MonoBehaviour
{
    public void gameTap()
    {
        SceneManager.LoadScene("colorGame");
    }

    public void gamePreguntados()
    {
        SceneManager.LoadScene("preguntados");
    }

    public void gameMemoria()
    {
        SceneManager.LoadScene("memoriaGame");
    }
}
