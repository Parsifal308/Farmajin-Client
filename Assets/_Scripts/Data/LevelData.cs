using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Farmanji.Data
{
    [System.Serializable]
    public class LevelData
    {
        public string Name;
        public string Id;
        public float LevelProgress;

        public Sprite BackgroundImage;
        public string World;
        public bool LevelCompleted;
        public bool Disabled;

        public List<StageData> stages;

        public event Action OnLevelCompleted;

        public LevelData(string id, string levelName, string worldName, Sprite backgroundImage, List<StageData> stagesList, float levelProgress, bool completed)
        {
            Id = id;
            Name = levelName;
            World = worldName;
            BackgroundImage = backgroundImage;
            LevelProgress = levelProgress;
            stages = new List<StageData>(stagesList);
            LevelCompleted = completed;
            OnLevelCompleted = delegate { };
        }

        public void UpdateLevelProgress()
        {
            var levelsProgress = stages.Sum(stage => stage.StageProgress) / stages.Count;
            LevelProgress = levelsProgress;
            
            if (LevelCompleted) return;
            if (Math.Abs(LevelProgress - 100) <= 1)
            {
                LevelCompleted = true;
                LevelProgress = 100;
                OnLevelCompleted?.Invoke();
            }
        }

        public float GetLevelProgress()
        {
            UpdateLevelProgress(); //Just in case
            return LevelProgress;
        }
    }
}

