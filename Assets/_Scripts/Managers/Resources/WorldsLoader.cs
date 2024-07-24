using Farmanji.Game;
using Farmanji.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Farmanji.Data
{
    public class WorldsLoader : ResourceLoader
    {
        #region FIELDS
        public List<WorldData> List = new List<WorldData>();

        private Dictionary<string, WorldData> _worldData = new Dictionary<string, WorldData>(); 
        private Dictionary<string, LevelData> _levelData = new Dictionary<string, LevelData>();
        
        public Dictionary<string, WorldData> GetWorldsData { get { return _worldData; } }
        public Dictionary<string, LevelData> GetLevelsData { get { return _levelData; } }

        public ActivityData CurrentActivity;

        [Header("DEFAULT DATA:")]
        [SerializeField] private WorldData defaultData;
        #endregion

        #region METHODS
        public ActivityData GetCurrentActivity { get { return CurrentActivity; } }
        public void AddLevelToWorld(string worldName, List<LevelData> levelsData)
        {
            List.First(item => item.id == worldName).AddLevels(levelsData);
        }
        public void AddVideoToWorld(string worldName, VideoData videoData)
        {
            List.First(item => item.id == worldName).AddVideo(videoData.Level, videoData);
        }
        #endregion

        #region PROPERTIES
        public WorldData DefaultData { get { return defaultData; } }
        #endregion

        #region COROUTINES
        public override IEnumerator Load()
        {
            List.Clear();
            string queryParams = "?page=1,perPage=500";
            yield return StartCoroutine(_jsonLoader.LoadFromWeb<WorldsCollection>(queryParams, null));

            yield return StartCoroutine(CreateConcreteData());
        }

        public override IEnumerator CreateConcreteData()
        {
            WorldsCollection response = _jsonLoader.ResponseInfo as WorldsCollection;

            foreach (var worldElement in response.data)
            {
                yield return StartCoroutine(DownloadImage(worldElement.backgroundImage));
                Sprite backgroundImage = DownloadedSprite;

                yield return StartCoroutine(DownloadImage(worldElement.iconImage));
                Sprite mainImage = DownloadedSprite;

                List.Add(WorldData.Create(worldElement._id, worldElement.name, backgroundImage, mainImage, worldElement.completed));
            }
            yield return null;
        }

        public void CreateWorldsLevelsData()
        {
            foreach (var world in List)
            {
                if (!_worldData.ContainsKey(world.id)) _worldData.Add(world.id, world);
                foreach (var level in world.levels)
                {
                    if (!_levelData.ContainsKey(level.Id)) _levelData.Add(level.Id, level);
                }
            }
        }

        internal void CompleteCurrentActivity(float activityPercentage)
        {
            Debug.Log("INTENTANDO COMPLETAR");
            ActivityProgresBody body = ActivityProgresBody.Create(CurrentActivity.worldId, CurrentActivity.levelId, CurrentActivity.stageId, CurrentActivity.Id, true, "activity", activityPercentage);
            StartCoroutine(GetComponent<ActivityProgresPost>().Post(body));
        }
        
        internal void CompleteActivity(ActivityData activityData, float activityPercentage)
        {
            Debug.Log("INTENTANDO COMPLETAR");
            ActivityProgresBody body = ActivityProgresBody.Create(activityData.worldId, activityData.levelId, activityData.stageId, activityData.Id, true, "activity", activityPercentage);
            StartCoroutine(GetComponent<ActivityProgresPost>().Post(body));
        }
        #endregion



    }
}
