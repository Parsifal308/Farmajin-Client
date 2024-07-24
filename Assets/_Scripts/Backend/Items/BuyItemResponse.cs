namespace Farmanji.Data
{
    [System.Serializable]
    public class BuyItemResponse : Response
    {
        public string message;
        public ProductResponse product;
    }

    [System.Serializable]
    public class ProductResponse
    {
        public string _id;
        public string name;
        public string description;
        public string category;
        public string rewardType;
        public string imageUrl;
        public string stock;
        public string priceType;
        public string price;
        public int coinsReward;
        public int gemsReward;
        public bool coins;
        public bool gems;
        public int unblockAfter;
        public AssetsResponse assets;
        public bool disabled;
        public bool published;
        public string sku;
        public string companyId;
        public string createdAt;
        public string updatedAt;
        public string deletedAt;
    }
}