using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Farmanji.Data
{
    [System.Serializable]
    public class StageData
    {
        #region FIELDS
        public string Id;
        public string Name;
        //public int activityNumber;
        public string LevelId;
        public List<ActivityData> Activities = new List<ActivityData>();
        public bool Disabled;
        public VideoData video;
        public List<GameData> games = new List<GameData>();
        public float StageProgress;
        public bool StageCompleted;
        public event Action OnStageCompleted;
        #endregion

        #region PUBLIC METHODS
        public StageData(StagesResponse response, List<ActivityData> activities, float stageProgress)
        {
            Id = response._id;
            Name = response.name;
            LevelId = response.levelId;
            Disabled = response.disabled;
            StageProgress = stageProgress;

            Activities = new List<ActivityData>(activities);
            //OnStageCompleted = delegate { };
            StageCompleted = response.completed;

            /*for (int i = 0; i < response.activities.Length; i++)
            {
                Activities.Add(new ActivityData(response.activities[i]));
            }*/
        }

        public void UpdateStageProgress()
        {
            if (Activities == null || Activities.Count <= 0) return;
            var activitiesCompleted = Activities.Count(activity => activity.Completed);
            StageProgress = 100 / Activities.Count * activitiesCompleted;
            
            if (StageCompleted) return;
            if (Math.Abs(StageProgress - 100) < 1.1f)
            {
                StageCompleted = true;
                StageProgress = 100;
                OnStageCompleted?.Invoke();
            }
        }

        public float GetStageProgress()
        {
            //UpdateStageProgress();
            return StageProgress;
        }

        #endregion
    }
}

