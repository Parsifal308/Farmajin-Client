using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    public class AchievementsLoader : ResourceLoader
    {
        #region FIELDS 
        public List<AchievementData> Achievements = new List<AchievementData>();
        #endregion

        #region METHODS
        
        public override IEnumerator Load()
        {
            yield return StartCoroutine(_jsonLoader.LoadFromWeb<AchievementsCollection>("?page=1&perPage=250"));

            yield return StartCoroutine(CreateConcreteData());
        }

        public override IEnumerator CreateConcreteData()
        {
            Achievements.Clear();
            
            AchievementsCollection response = _jsonLoader.ResponseInfo as AchievementsCollection;

            foreach (var achievementElement in response.data)
            {
                yield return StartCoroutine(DownloadImage(achievementElement.imageUri));
                Sprite achievementSprite = DownloadedSprite;
                
                Achievements.Add(new AchievementData(achievementElement, achievementSprite));
            }
            yield return null;
        }
        public AchievementData FindBadgeByID(string id)
        {
            foreach (var badge in Achievements)
            {
                if (badge.Id == id)
                {
                    return badge;
                }
            }
            Debug.Log("NO SE ENCONTRO INSIGNIA CON DICHO ID");
            return null;
        }
        #endregion
    }
}