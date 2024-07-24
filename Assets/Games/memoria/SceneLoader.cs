using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadscreen;
    public Slider loadingBar;
    public GameObject pauseMenu;
    public GameObject Timer;

    public void LoadScene(int levelIndex)
    {
        StartCoroutine(LoadSceneAsynchronously(levelIndex));
    }

    IEnumerator LoadSceneAsynchronously(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        loadscreen.SetActive(true);
        pauseMenu.SetActive(false);
        Timer.SetActive(false);
        while (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            Debug.Log(operation.progress);
            yield return null;
        }
    }
}
