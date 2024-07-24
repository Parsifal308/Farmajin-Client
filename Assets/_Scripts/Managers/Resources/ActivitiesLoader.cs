using Farmanji.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace Farmanji.Data
{
    public class ActivitiesLoader : ResourceLoader
    {
        #region FIELDS
        public List<ActivityData> List = new List<ActivityData>();
        #endregion
        public override IEnumerator Load()
        {
            List.Clear();
            string queryParams = "?page=1,perPage=500";
            yield return StartCoroutine(_jsonLoader.LoadFromWeb<ActivitiesCollection>("?page=1&perPage=500"));
            yield return StartCoroutine(CreateConcreteData());
        }
        public override IEnumerator CreateConcreteData()
        {
            ActivitiesCollection response = _jsonLoader.ResponseInfo as ActivitiesCollection;

            foreach (var activityElement in response.data)
            {
                yield return StartCoroutine(DownloadImage(activityElement.coverImage));
                Sprite coverImage = DownloadedSprite;
                List.Add(new ActivityData(activityElement, coverImage, -1f, "", ""));
            }
            yield return null;
        }

        public static bool HarcodedGames(string gameType)
        {
            switch (gameType)
            {
                case "quizitalla":
                    return true;
                case "memory":
                    return true;
                case "tapcolor":
                    return true;
            }
            return false;
        }
    }
}