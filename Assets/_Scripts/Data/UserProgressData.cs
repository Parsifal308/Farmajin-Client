using System.Collections.Generic;

namespace Farmanji.Data
{
    [System.Serializable] 
    public class UserProgressData
    {
        public string _id;
        public string userId;
        public string createdAt;
        public string updatedAt;
        public ProgressData progress;

        [System.Serializable]
        public class ProgressData
        {
            public List<WorldProgressData> worlds = new List<WorldProgressData>();
        }

        [System.Serializable]
        public class WorldProgressData
        {
            public string progress; //"in-progress" //or "completed",
            public string worldId;
            public List<LevelProgressData> levels = new List<LevelProgressData>();
        }
    
        [System.Serializable]
        public class LevelProgressData
        {
            public string progress; //"in-progress" //or "completed",
            public string levelId;
            public List<StageProgressData> stages = new List<StageProgressData>();
        }
        
        [System.Serializable]
        public class StageProgressData
        {
            public string progress; //"in-progress" //or "completed",
            public string stageId;
            public List<ActivitiesProgressData> activities = new List<ActivitiesProgressData>();
        }
        
        [System.Serializable]
        public class ActivitiesProgressData
        {
            public string progress; //"in-progress" //or "completed",
            public string activityId;
        }

        public UserProgressData(UserProgressResponse response)
        {
            _id = response._id;
            userId = response.userId;
            createdAt = response.createdAt;
            updatedAt = response.updatedAt;
            progress = response.progress;
        }
    }
}