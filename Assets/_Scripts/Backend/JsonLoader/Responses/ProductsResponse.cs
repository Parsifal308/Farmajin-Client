using System.Collections.Generic;

namespace Farmanji.Data
{
    [System.Serializable]
    public class ProductsResponse
    {
        public string _id;
        public string name;
        public string description;
        public string category;
        public string rewardType;
        public string imageUrl;
        public int stock;
        public string priceType;
        public float price;
        public int coinsReward;
        public int gemsReward;
        public bool coins;
        public bool gems;
        public string sku;
        public int unblockAfter;
        //public AssetsResponse[] assets;
        public List<string> assets;//image urls
        public bool disabled;
        public bool published;
        public bool unlocked;
        public bool owned;
    }
}