using Farmanji.UI;
using Farmanji.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Farmanji.Managers
{
    public class ScenesManager : SingletonPersistent<ScenesManager>
    {
        [SerializeField] private LoadingScreen _loadingScreenData;
        private AsyncOperation _currentOperation;
        private static GameObject HomeRootObject;

        public Action<float> OnLoadingTick;
        public Action OnLoadingFinish;

        #region PUBLIC METHODS
        public void LoadLogin()
        {
            OnLoadingFinish += OnFinishLoadingLogin;
            StartCoroutine(LoadSceneAsynchronously("Login", LoadSceneMode.Single));
        }
        public void LoadHome()
        {
            OnLoadingFinish += OnFinishLoadingHome;
            StartCoroutine(LoadSceneAsynchronously("Home", LoadSceneMode.Single));
        }

        public void LoadGame(string gameName)
        {
            OnLoadingFinish += OnFinishLoadingGame;
            StartCoroutine(LoadSceneAsynchronously(gameName + "Game", LoadSceneMode.Additive));
        }

        public void ReturnToHome()
        {
            StartCoroutine(BackToHome());
            SoundManager.Instance.SetMusicVolume(1f);
            SoundManager.Instance.SetMusic(SoundManager.Instance.Theme);
            SoundManager.Instance.PlayMusic();

            StartCoroutine(ResourcesLoaderManager.Instance.Economy.Load());
        }

        public void ReturnToLogin()
        {
            SceneManager.LoadScene(0);

        }

        public void ReloadScene()
        {
            StartCoroutine(ReloadCurrentScene());
        }
        #endregion

        #region PRIVATE METHODS
        private IEnumerator LoadSceneAsynchronously(string levelName, LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            GameObject loadingObj = _loadingScreenData.InstantiatePreview();
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadSceneAsync(_loadingScreenData.Scene.Name, LoadSceneMode.Additive);

            yield return new WaitForSeconds(0.5f);
            //loadingObj.SetActive(false);
            Destroy(loadingObj);
            _currentOperation = SceneManager.LoadSceneAsync(levelName, loadMode);
            _currentOperation.allowSceneActivation = false;
            do
            {
                float correctedProgress = _currentOperation.progress / 0.9f;
                OnLoadingTick?.Invoke(correctedProgress);
                //Debug.Log(correctedProgress);
                yield return null;

            } while (_currentOperation.progress < 0.9f);
            OnLoadingTick?.Invoke(1f);
            yield return new WaitForSeconds(0.5f);
            OnLoadingFinish?.Invoke();
        }

        private IEnumerator BackToHome()
        {
            Time.timeScale = 1.0f;
            GameObject loadingObj = _loadingScreenData.InstantiatePreview();

            if (SceneManager.sceneCount <= 1) { SceneManager.LoadScene("Home", LoadSceneMode.Single); yield break; }

            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadSceneAsync(_loadingScreenData.Scene.Name, LoadSceneMode.Additive);
            //SceneManager.UnloadSceneAsync(_loadingScreenData.Scene.Name);
            yield return new WaitForSeconds(0.5f);
            Destroy(loadingObj);
            //loadingObj.SetActive(false);
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
            SceneManager.UnloadSceneAsync(_loadingScreenData.Scene.Name);
            Debug.Log(HomeRootObject);
            HomeRootObject.SetActive(true);
        }

        private IEnumerator ReloadCurrentScene()
        {
            Time.timeScale = 1.0f;
            GameObject loadingObj = _loadingScreenData.InstantiatePreview();
            if (SceneManager.sceneCount <= 1) { SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single); yield break; }

            string currentSceneName = SceneManager.GetSceneAt(1).name;
            yield return new WaitForSeconds(1.0f);

            _currentOperation = SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));

            while (!_currentOperation.isDone)
            {
                yield return null;
            }

            _currentOperation = SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive);

            while (!_currentOperation.isDone)
            {
                float correctedProgress = _currentOperation.progress / 0.9f;
                OnLoadingTick?.Invoke(correctedProgress);
                yield return null;
            }

            loadingObj.SetActive(false);
            Destroy(loadingObj);

        }
        #endregion

        #region EVENTS
        private void OnFinishLoadingHome()
        {
            StartCoroutine(PrepareHomeScene());
            OnLoadingFinish -= OnFinishLoadingHome;
        }
        private void OnFinishLoadingLogin()
        {
            OnLoadingFinish -= OnFinishLoadingLogin;
        }

        private void OnFinishLoadingGame()
        {
            _currentOperation.allowSceneActivation = true;
            SceneManager.UnloadSceneAsync(_loadingScreenData.Scene.Name);
            HomeRootObject.SetActive(false);
            OnLoadingFinish -= OnFinishLoadingGame;
            SoundManager.Instance.MuteMusic();
        }

        private IEnumerator PrepareHomeScene()
        {
            yield return new WaitForSeconds(1.1f);
            yield return StartCoroutine(ResourcesLoaderManager.Instance.LoadAll());
            _currentOperation.allowSceneActivation = true;
            SceneManager.UnloadSceneAsync(_loadingScreenData.Scene.Name);
            yield return new WaitForSeconds(0.1f);
            do
            {
                HomeRootObject = GameObject.Find("RootObject");
                yield return new WaitForSeconds(0.1f);
            } while (HomeRootObject == null);

            ResourcesLoaderManager.Instance.OnFinishLoad?.Invoke();
        }

        #endregion
    }
}

