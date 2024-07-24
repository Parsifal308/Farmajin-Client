namespace Farmanji.Data
{
    public class UserProgressBody : PostBody
    {
        public string worldId;
        public string levelId;
        public string stageId;
        public string activityId;
        public string progress; //  indicates the progress status, which can be either "completed" or "in-progress".
        public string progressType; //  specifies the type of progress being updated ("world", "level", "stage", or "activity", “tiptap”). Tiptap does not require worldId, levelId, stageId.
        public float activityPercentage; //Number between 0 to 1, refers to activity completion percentage (Obstáculos Completados / Total Obstáculos)

        public static UserProgressBody CreateUserProgressBody(string _worldId, string _levelId, string _stageId, string _activityId, string _progress, 
            string _progressType, float _activityPercentage)
        {
            var newBody = new UserProgressBody
            {
                worldId = _worldId,
                levelId = _levelId,
                stageId = _stageId,
                activityId = _activityId,
                progress = _progress,
                progressType = _progressType,
                activityPercentage = _activityPercentage
            };
            return newBody;
        }
    }
}