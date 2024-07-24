using System;
using System.Collections.Generic;
using Farmanji.Managers;
using UnityEngine;
using System.Linq;
using UnityEditor.Profiling.Memory.Experimental;

namespace Farmanji.Data
{
    [System.Serializable]
    public class ActivityData
    {
        #region FIELDS
        public string Id;
        public string Name;
        public string Description;

        public Sprite CoverImage;

        public string RewardType;
        public int Reward;
        public int GemsReward;
        public int CoinsReward;
        public bool Disabled;

        public float ProgressReward;
        public bool Completed;
        public float ProgressPercent;

        public string ActivityType;
        public string VideoUrl;

        public Questions[] Questions;
        public List<string> Questions_1;
        public string[] Responses;

        public bool UsersWithProgress;

        public string worldId;
        public string levelId;
        public string stageId;

        public string GameType;

        public event Action OnActivityCompleted;

        public AssociatedQuiz AssociatedQuiz;

        public static List<Questions> allQuestions = new List<Questions>();

        [SerializeField] private ActivityTypeEnum _activityType = ActivityTypeEnum.None;
        #endregion

        #region PROPERTIEs
        public ActivityTypeEnum _ActivityType { get { return _activityType; } }
        #endregion

        #region CONSTRUCTOR
        public ActivityData(string name,string description,int gemReward,int coinsRewards,string activityType, string gameType) {
            Name = name; 
            Description=description;
            GemsReward=gemReward;
            CoinsReward=coinsRewards;
            ActivityType = activityType;
            GameType = gameType;
        }
        public ActivityData(ActivitiesResponse response, Sprite coverImage, float progressReward, string levelId, string stageId)
        {

            Name = response.name;
            Id = response._id;
            Description = response.description;
            CoverImage = coverImage;

            RewardType = response.rewardType;
            CoinsReward = response.coinsReward;
            GemsReward = response.gemsReward;

            Disabled = response.disabled;

            ActivityType = response.activityType;
            SetActivityType();
            VideoUrl = response.videoUrl;

            worldId = response.worldId;
            this.levelId = levelId;
            this.stageId = stageId;

            Completed = response.completed;
            Questions = response.questions;
            Responses = response.responses;

            GameType = response.gameType;

            if (_activityType == ActivityTypeEnum.Game)
            {
                AssociatedQuiz = response.associatedQuiz;


                Farmanji.Managers.ResourcesLoaderManager.Instance.OnFinishLoad += FillQuestions;

            }
            else
            {
                foreach (Questions q in Questions)
                {
                    allQuestions.Add(q);
                }

            }

            ProgressReward = progressReward;

            // if (PlayerPrefs.HasKey(string.Concat(Name, Description)))
            // {
            //     Completed = PlayerPrefs.GetString(string.Concat(Name, Description)) == "completed";
            // }

            // foreach (var worldProgressData in ResourcesLoaderManager.Instance.UserProgress.UserProgressData.worlds)
            // {
            //     if (worldId == worldProgressData.worldId)
            //     {
            //         foreach (var levelProgressData in worldProgressData.levels)
            //         {
            //             if (levelId == levelProgressData.levelId)
            //             {
            //                 foreach (var stageProgressData in levelProgressData.stages)
            //                 {
            //                     if (stageId == stageProgressData.stageId)
            //                     {
            //                         foreach (var activitiesProgressData in stageProgressData.activities)
            //                         {
            //                             if (Id == activitiesProgressData.activityId)
            //                             {
            //                                 Completed = activitiesProgressData.progress == "completed";
            //                             }
            //                         }
            //                     }
            //                 }
            //             }
            //         }
            //     }
            // }

            OnActivityCompleted = delegate { };
        }

        private void FillQuestions()
        {
            int v = allQuestions.Count >= 10 ? 10 : allQuestions.Count;

            List<Questions> myPickedQuestions = new List<Questions>();

            for (int i = 0; i < v; i++)
            {
                UnityEngine.Random.InitState(i);

                int rangeValue = UnityEngine.Random.Range(0, allQuestions.Count - 1);
                myPickedQuestions.Add(allQuestions[rangeValue]);

                allQuestions.RemoveAt(rangeValue);
                allQuestions = allQuestions.OrderBy(x => UnityEngine.Random.value).ToList();
            }

            Questions = myPickedQuestions.ToArray();

            ResourcesLoaderManager.Instance.OnFinishLoad -= FillQuestions;
        }
        #endregion

        #region METHODS

        public void CompleteActivity()
        {
            if (Completed) return;
            Completed = true;
            //CurrencyManager.Instance.AddCoins(100); //Placeholder

            // Completed = true;//POSTEAR

            //PlayerPrefs.SetString(string.Concat(Name, Description), "completed");
            Debug.Log("Completed: " + Completed);
            OnActivityCompleted?.Invoke();

            // var body = UserProgressBody.CreateUserProgressBody(worldId, levelId, stageId, Id, "completed", "activity", 1);
            // ResourcesLoaderManager.Instance.UserProgress.GetComponent<UserProgressPost>().PostTest(body);
        }
        private void SetActivityType()
        {
            switch (ActivityType)
            {
                case "game":
                    _activityType = ActivityTypeEnum.Game;
                    break;
                case "video":
                    _activityType = ActivityTypeEnum.Video;
                    break;
                case "quiz":
                    _activityType = ActivityTypeEnum.Quiz;
                    break;
            }
        }
        #endregion

        #region ENUMS
        public enum ActivityTypeEnum
        {
            None,
            Game,
            Video,
            Quiz
        }
        #endregion
    }
}