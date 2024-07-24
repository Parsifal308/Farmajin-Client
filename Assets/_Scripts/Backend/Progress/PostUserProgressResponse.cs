namespace Farmanji.Data
{
    [System.Serializable]
    public class PostUserProgressResponse : Response
    {
        public string _id;
        public string userId;
        public string worldId;
        public string levelId;
        public string activityId;
        public string progress;
        public string progressType;
        public string createdAt;
        public string updatedAt;
    }
}