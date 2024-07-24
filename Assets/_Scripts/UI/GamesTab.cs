using System;
using System.Collections;
using System.Collections.Generic;
using Farmanji.Data;
using Farmanji.Managers;
using Farmanji.UI;
using UnityEngine;

namespace Farmanji.Game
{
    /*     public GameObject SelectClassicReto;
     */
    public class GamesTab : BaseTab
    {
        [SerializeField] private List<ActivityData> _activityDatas = new List<ActivityData>();
        [SerializeField] private CardGame _cardGamePrefab;
        [SerializeField] private Transform _cardGameView;

        [Header("HARDCODED SHITS: (delete later)")]
        [SerializeField] private ActivityData quizitallaData;
        [SerializeField] private ActivityData memoriaData;
        [SerializeField] private ActivityData tapColorData;

        private void Start()
        {
            //LoadGameData();
            //CreateGameCards();
            CreateBaseGamesCards();//only for show purpose
        }

        private void CreateBaseGamesCards()
        {
            var quizitallaCardGame = Instantiate(_cardGamePrefab, _cardGameView);
            var quizitallaUI = quizitallaCardGame.GetComponent<UiMainGameCard>();

            quizitallaUI.BackgroundSprite = quizitallaData.CoverImage;
            quizitallaUI.Title = quizitallaData.Name;
            quizitallaUI.Description = quizitallaData.Description;

            quizitallaCardGame.LoadGameData(quizitallaData);
            quizitallaCardGame.CoinsText.text = quizitallaData.CoinsReward.ToString();
            quizitallaCardGame.GemsText.text = quizitallaData.GemsReward.ToString();
            quizitallaCardGame.ActivityText.text = quizitallaData.Name;



            var memoriaCardGame = Instantiate(_cardGamePrefab, _cardGameView);
            var memoriaUI = memoriaCardGame.GetComponent<UiMainGameCard>();

            memoriaUI.BackgroundSprite = memoriaData.CoverImage;
            memoriaUI.Title = memoriaData.Name;
            memoriaUI.Description = memoriaData.Description;

            memoriaCardGame.LoadGameData(memoriaData);
            memoriaCardGame.CoinsText.text = memoriaData.CoinsReward.ToString();
            memoriaCardGame.GemsText.text = memoriaData.GemsReward.ToString();
            memoriaCardGame.ActivityText.text = memoriaData.Name;



            var tapColorCardGame = Instantiate(_cardGamePrefab, _cardGameView);
            var tapColorUI = tapColorCardGame.GetComponent<UiMainGameCard>();

            tapColorUI.BackgroundSprite = tapColorData.CoverImage;
            tapColorUI.Title = tapColorData.Name;
            tapColorUI.Description = tapColorData.Description;

            tapColorCardGame.LoadGameData(tapColorData);
            tapColorCardGame.CoinsText.text = tapColorData.CoinsReward.ToString();
            tapColorCardGame.GemsText.text = tapColorData.GemsReward.ToString();
            tapColorCardGame.ActivityText.text = tapColorData.Name;
        }

        private void LoadGameData()
        {
            _activityDatas.Clear();


            foreach (var worldData in ResourcesLoaderManager.Instance.Worlds.List)
            {
                foreach (var levelData in worldData.levels)
                {
                    foreach (var stageData in levelData.stages)
                    {
                        foreach (var activityData in stageData.Activities)
                        {
                            _activityDatas.Add(activityData);
                        }
                    }
                }
            }

        }

        private void CreateGameCards()
        {
            foreach (var activityData in ResourcesLoaderManager.Instance.Activities.List)
            {
                if (activityData.ActivityType == "game" && ActivitiesLoader.HarcodedGames(activityData.GameType))
                {
                    var cardGame = Instantiate(_cardGamePrefab, _cardGameView);
                    cardGame.LoadGameData(activityData);
                }
            }
        }
    }
    /* 
        public void OnSelect(){
            SelectClassicReto.SetActive();
        } */
}

