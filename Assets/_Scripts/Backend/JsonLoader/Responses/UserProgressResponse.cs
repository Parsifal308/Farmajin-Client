namespace Farmanji.Data
{
    [System.Serializable]
    public class UserProgressResponse
    {
        public string _id;
        public string userId;
        public string createdAt;
        public string updatedAt;
        public UserProgressData.ProgressData progress; 
    }
}