using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    [System.Serializable]
    public class ProductData
    {
        public string Id;
        public string Name;
        public Sprite Image;
        public string Category;
        public string RewardType;
        public string PriceType;
        public float Price;
        public int CoinsPrice;
        public int GemsPrice;
        public Sprite SecondImage;
        public string Sku;
        public int Stock;
        public string Code;
        public bool Unlocked;
        public bool Owned;
        public List<string> imagesUrls;
        public List<Sprite> images;

        public ProductData(ProductsResponse response, List<Sprite> images, string code = "")
        {
            Id = response._id;
            Name = response.name;
            Category = response.category;
            RewardType = response.rewardType;
            PriceType = response.priceType;
            Price = response.price;
            Sku = response.sku;
            CoinsPrice = response.coinsReward;
            GemsPrice = response.gemsReward;
            Stock = response.stock;
            Unlocked = response.unlocked;
            Owned = response.owned;
            Code = code;
            imagesUrls = response.assets;
            this.images = images;
        }
    }
}