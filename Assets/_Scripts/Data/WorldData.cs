using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

namespace Farmanji.Data
{
    //[CreateAssetMenu]
    [System.Serializable]
    public class WorldData// : ScriptableObject
    {
        public string name;
        public string id;

        public Sprite background;
        public Sprite platformImage;
        public int level;
        public float WorldProgress;
        public bool Completed;

        public List<LevelData> levels = new List<LevelData>();

        public event UnityAction<float> OnWorldProgressUpdated;
        public event Action OnWorldCompleted;

        public static WorldData Create(string newId, string worldName, Sprite backgroundSprite, Sprite platformSprite, bool completed)
        {

            WorldData newWorldData = new WorldData();//CreateInstance<WorldData>();
            //newWorldData.name = worldName;
            newWorldData.id = newId;
            newWorldData.name = worldName;
            newWorldData.background = backgroundSprite;
            newWorldData.platformImage = platformSprite;
            newWorldData.Completed = completed;
            return newWorldData;
        }

        public void AddLevels(List<LevelData> levelsData)
        {
            //levels.Add(levelData);

            levels = new List<LevelData>(levelsData);
        }

        public void AddVideo(string levelName, VideoData videoData)
        {
            //levels.First(item => item.Name == levelName).AddVideoOnStage(videoData.StageIndex-1, videoData);
        }

        public void UpdateWorldProgress()
        {
            var levelsProgress = levels.Sum(_level => _level.LevelProgress) / levels.Count;
            WorldProgress = levelsProgress;
            OnWorldProgressUpdated?.Invoke(WorldProgress);
        }

        public float GetWorldProgress()
        {
            //UpdateWorldProgress(); //Just in case 
            return WorldProgress;
        }
    }
}

