namespace Farmanji.Data
{
    [System.Serializable]
    public class ItemsResponse
    {
        public string _id;
        public string name;
        public string type;
        public string itemType;
        public string unlockMode;
        public string worldId;
        public string levelId;
        public bool coins;
        public bool gems;
        public int coinsReward;
        public int gemsReward;
        public float price;
        public string itemUrl;
        public bool usersWithProgress;
        public string productId;
        public bool disabled;
        public string companyId;
        public string createdAt;
        public string updatedAt;
        public AssetsResponse[] assets;
    }

    [System.Serializable]
    public class AssetsResponse
    {
        public string id;
        public string name;
        public string url;
    }
}