
namespace Farmanji.Data
{
    [System.Serializable]
    public class StagesResponse
    {
        public string _id;
        public string name;
        public int activityNumber;
        public string levelId;
        public ActivitiesResponse[] activities;
        public bool completed;
        public bool disabled;
        public string updatedAt;
        public string deletedAt;
    }
}
