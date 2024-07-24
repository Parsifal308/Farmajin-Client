using Farmanji.Data;
using Farmanji.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEditorInternal;

namespace Farmanji.UI
{
    public class CardGame : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private ActivityType activityType;
        [SerializeField] private string _gameScene;
        [SerializeField] private Button _playBtn;
        [SerializeField] private ActivityData _activityData;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private TextMeshProUGUI _gemsText;
        [SerializeField] private TextMeshProUGUI _activityText;



        public event Action OnActivityCompleted;
        #endregion

        #region PROPERTIEs
        public TextMeshProUGUI CoinsText{get{return _coinsText; } }
        public TextMeshProUGUI GemsText { get { return _gemsText; } }
        public TextMeshProUGUI ActivityText { get { return _activityText; } }
        public Button PlayButton { get { return _playBtn; } }
        public ActivityType ActivityType { get { return activityType; } }
        public ActivityData ActivityData { get { return _activityData; } }
        #endregion

        #region UNITY METHODS

        #endregion

        #region PRIVATE METHODS
        public void PlayGame()
        {
            //OnActivityCompleted?.Invoke();

            if (_activityData != null)
            {
                Farmanji.Managers.ResourcesLoaderManager.Instance.Worlds.CurrentActivity = _activityData;
            }
            //return; //Temporary
            if (_gameScene != "")
            {
                ScenesManager.Instance.LoadGame(_gameScene);
            }
            else
            {
                Debug.LogWarning("↳ gameScene field is empty in " + this.GetType() + "component of " + this.gameObject.name + " ↲");
            }
        }
        #endregion

        #region PUBLIC METHODS
        public void LoadGameData(ActivityData data)
        {
            _activityData = data;
            
            /*switch (data._ActivityType)
            {
                case ActivityData.ActivityTypeEnum.Game:

                    _gameScene = "PacmanLevel1"; //ESTO DEBERA CAMBIARSE CUANDO AGREGUEMOS NUEVOS NIVELES DEL PACMAN
                    break;
                case ActivityData.ActivityTypeEnum.Quiz:
                    _gameScene = "Quiz";
                    break;
            }*/
            switch (data.ActivityType)
            {
                case "quiz":
                    _gameScene = "Quiz";
                    break;
                case "game":
                    switch (data.GameType)
                    {
                        case "quizitalla":
                            _gameScene = "Preguntados";
                            break;
                        case "pacman":
                            _gameScene = "PacmanLevel1";
                            break;
                        case "memory":
                            _gameScene = "memoria";
                            break;
                        case "complement":
                            _gameScene = "";
                            break;
                        case "pair":
                            _gameScene = "";
                            break;
                        case "tapcolor":
                            _gameScene = "TapColor";
                            break;
                    }
                    break;
            }

            if (_coinsText) _coinsText.text = data.CoinsReward.ToString();
            if (_gemsText) _gemsText.text = data.GemsReward.ToString();
            if (_activityText) _activityText.text = data.Name;
            _playBtn.onClick.AddListener(PlayGame);

            //OnActivityCompleted += data.CompleteActivity;
            //OnActivityCompleted += ResourcesLoaderManager.Instance.Worlds.CompleteCurrentActivity;

            /*if(data.Completed)
            {
                _playBtn.interactable = false;
            }*/

            Debug.Log("Loading Game Data...");
        }
        #endregion
    }
    public enum ActivityType
    {
        Quiz,
        Game,
        Video
    }

}
