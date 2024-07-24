namespace Farmanji.Data
{
    [System.Serializable]
    public class UserCustomizationResponse
    {
        public string _id;
        public string userId;
        public ItemsResponse hat;
        public ItemsResponse hair;
        public ItemsResponse face;
        public ItemsResponse trunk;
        public ItemsResponse legs;
        public ItemsResponse pet;
        public string createdAt;
        public string updatedAt;
    }
}