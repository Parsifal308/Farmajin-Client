using System;
using System.Collections;
using System.Collections.Generic;
using Farmanji.Data;
using UnityEngine;

namespace Farmanji.Data
{
    public class UserAchievementsLoader : ResourceLoader
    {
        #region FIELDS 
        public List<UserAchievementData> Achievements = new List<UserAchievementData>();
        #endregion
        public override IEnumerator Load()
        {
            yield return StartCoroutine(_jsonLoader.LoadFromWeb<UserAchievementsCollection>());

            yield return StartCoroutine(CreateConcreteData());
        }
        public override IEnumerator CreateConcreteData()
        {
            UserAchievementsCollection response = _jsonLoader.ResponseInfo as UserAchievementsCollection;
            if (response.data != null && response.data.Count > 0)
            { 
                Achievements.Add(new UserAchievementData(response.data[0]));
                for (int i = 0; i < response.data[0].badges.Count; i++)
                {
                    yield return StartCoroutine(DownloadImage(response.data[0].badges[i].imageUri));
                    Sprite achievementSprite = DownloadedSprite;
                    //Debug.Log("achievementSprite: " + achievementSprite);
                    //Debug.Log("response.data[0].badges[" + i + "]: " + response.data[0].badges[i].name);

                    Achievements[0].Badges.Add(new AchievementData(response.data[0].badges[i], achievementSprite));
                    // Achievements.Badges[i].Image = achievementSprite;
                }
            }
            yield return null;
        }
    }

    [Serializable]
    public class UserAchievementData
    {
        public string Id;
        public string UserId;
        public List<AchievementData> Badges = new List<AchievementData>();

        public UserAchievementData(UserAchievementResponse response)
        {
            Id = response._id;
            UserId = response.userId;
            //Badges = response.badges;
        }
    }
    [Serializable]
    public class UserAchievementResponse
    {
        public string _id;
        public string userId;
        public List<AchievementResponse> badges;
    }
}