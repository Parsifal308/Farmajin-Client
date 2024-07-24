using System.Collections;
using System.Collections.Generic;
using Farmanji.Managers;
using Farmanji.Pacman;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GenericLoadingScreen : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private AudioClip theme;
    [Header("UI SETTINGS")]
    [SerializeField] private Scrollbar loadingBar;

    [Header("SCENE SETTINGS")]
    [SerializeField] private string sceneName;
    #endregion

    #region UNITY METHODS
    private void Awake()
    {
        SoundManager.Instance.SetMusic(theme);
        SoundManager.Instance.PlayMusic();
        SoundManager.Instance.UnMuteMusic();
    }
    void Start()
    {
        StartCoroutine(Wait());
    }
    #endregion

    #region COROUTINES
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3f);
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneName);
        if (loadingBar != null)
        {
            while (!loadScene.isDone)
            {
                loadingBar.size = Mathf.Clamp01(loadScene.progress / 0.9f);
                yield return null;
            }
        }
        yield return null;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        GetComponent<CanvasGroup>().alpha = 0;
        PacmanGameManager.Instance.GameStateCanvas.GetComponent<GameStateCanvas>().SetStart();
    }
    #endregion
}
