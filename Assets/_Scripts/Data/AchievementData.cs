using UnityEngine;

namespace Farmanji.Data
{
    [System.Serializable]
    public class AchievementData
    {
        public string Name;
        public string Id;
        public string Description;
        public Sprite Image;
        public string WorldId;
        public string LevelId;
        public bool Unlocked = true;

        public AchievementData (AchievementResponse response, Sprite AchivementSprite)
        { 
            Name = response.name;
            Id = response._id;
            Description = response.description;
            Image = AchivementSprite;
            WorldId = response.worldId;
            LevelId = response.levelId;
        }
    }
}