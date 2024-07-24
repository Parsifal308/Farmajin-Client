using Farmaji.Game;
using Farmanji.Data;
using Farmanji.Managers;
using Farmanji.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Farmanji.Game
{
    public class StagesTab : BaseTab
    {
        #region FIELDS
        [SerializeField] private bool dev=false;
        [SerializeField] private TextMeshProUGUI stageName;
        [Header("PREFABS:")]
        [SerializeField] private GameObject gameCardPrefab;
        [SerializeField] private GameObject quizCardPrefab;
        [SerializeField] private GameObject spacer;

        [Header("CONTAINERS:")]       
        [SerializeField] private Transform _videoPlayerContent;
        [SerializeField] private Transform quizesContent;
        [SerializeField] private Transform gamesContent;

        private GameObject _videoContainer;
        private GameObject _quizesContainer;
        private GameObject _gamesContainer;

        private List<GameObject> gameCards = new List<GameObject>();
        private List<GameObject> quizsCards = new List<GameObject>();
        #endregion

        #region UNITY METHODS
        private void Start()
        {
            _videoContainer = _videoPlayerContent.gameObject;
            _quizesContainer = quizesContent.parent.parent.gameObject;
            _gamesContainer = gamesContent.parent.parent.gameObject;
        }
        #endregion

        #region PUBLIC METHODS
        public override void Open()
        {
            base.Open();
        }
        public override void Close()
        {
            base.Close();
            VideoManager.Instance.StopVideoPlayer();
            VideoManager.Instance.SetRefreshVideo();
            SoundManager.Instance.UnMuteMusic();
        }
        public void InitializeStage(StageData data)
        {
            stageName.text = data.Name;
            _videoContainer.SetActive(false);
            CleanGameCards();
            CleanQuizCards();
            CardGame gameCard;
            foreach (ActivityData activity in data.Activities)
            {
                switch (activity._ActivityType)
                {
                    case ActivityData.ActivityTypeEnum.Game:
                        if (!_gamesContainer.activeSelf) _gamesContainer.SetActive(true); 
                        gameCards.Add(Instantiate(gameCardPrefab, gamesContent.transform));
                        gameCard = gameCards.Last().GetComponent<CardGame>();
                        gameCard.LoadGameData(activity);
                        
                        //gameCard.PlayButton.onClick.AddListener(gameCard.PlayGame);
                    break;
                    case ActivityData.ActivityTypeEnum.Quiz:
                        if (!_quizesContainer.activeSelf) _quizesContainer.SetActive(true);
                        quizsCards.Add(Instantiate(quizCardPrefab, quizesContent.transform));
                        gameCard = quizsCards.Last().GetComponent<CardGame>();
                        gameCard.LoadGameData(activity);
                       
                        //gameCard.PlayButton.onClick.AddListener(gameCard.PlayGame);
                        
                        /*if (activity.Questions.Length > 0)
                       {

                           List<Question> AllQuestions = new List<Question>();
                           foreach(Questions item in activity.Questions)
                           {
                               string questionText = item.question;
                               //Debug.Log("Pregunta: "+questionText);
                               Responses []responses = item.responses;

                               Question newQuestion = new Question();
                               newQuestion.questionInfo = questionText;
                               newQuestion.options = new List<string>();
                               newQuestion.questionImg = null;
                               newQuestion.questionClip = null;
                               newQuestion.questionVideo = null;
                               newQuestion.questionType = QuestionType.TEXT;
                               foreach (Responses answer in responses)
                               {
                                   string option = answer.response;
                                   //Debug.Log ("OPTION: "+option );
                                   newQuestion.options.Add(option);

                                   if (answer.isCorrect)
                                   {
                                       newQuestion.correctAns = option;
                                   }
                               }
                               AllQuestions.Add(newQuestion);
                               //Questions.Add(newQuestion);
                               //quizManager.OnPlayButtonClicked(newQuestion);
                           }
                           Dictionary<string, object> Tests = new Dictionary<string, object>();
                           Tests.Add("Test", activity.Name);
                           Tests.Add("Questions", AllQuestions);

                           QuestionsSingleton.Instance.TestsList = Tests;
                           //TestsList.Add(Tests);
                           //testListQuiz.CreateQuestion(AllQuestions);
                           //testListQuiz.CreateQuestion(TestsList[0]);

                       }*/
                        break;
                    case ActivityData.ActivityTypeEnum.Video:
                        if (!_videoContainer.gameObject.activeSelf) _videoContainer.SetActive(true);
                        if (activity.VideoUrl != "")
                        {
                            GameObject playerObj = VideoManager.Instance.CreateVideoPlayer(_videoPlayerContent.transform, activity);
                            VideoManager.Instance.PreLoadVideo(activity.VideoUrl);
                            VideoManager.Instance.AddOnVideoFinishedEvent(activity.CompleteActivity);
                        }
                        break;
                }
            }
            //gamesContent.GetComponentInChildren<ActivitiesLayout>().Initialize();
        }
#endregion

        #region PRIVATE METHODS
        private void CleanGameCards()
        {
            for (int i = 0; i < gameCards.Count; i++)
            {
                Destroy(gameCards[i].gameObject);
            }
            gameCards.Clear();
            _gamesContainer.SetActive(false);
        }
        private void CleanQuizCards()
        {
            for (int i = 0; i < quizsCards.Count; i++)
            {
                Destroy(quizsCards[i].gameObject);
            }
            quizsCards.Clear();
            _quizesContainer.SetActive(false);
        }
        #endregion
    }
}