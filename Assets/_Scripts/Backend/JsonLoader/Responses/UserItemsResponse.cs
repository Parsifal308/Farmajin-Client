namespace Farmanji.Data
{
    [System.Serializable]
    public class UserItemsResponse
    {
        public string _id;
        public string userId;
        public ItemsResponse[] items;
        public string createdAt;
        public string updatedAt;
    }
}