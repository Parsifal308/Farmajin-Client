using Farmanji.Game;
using UnityEngine;

namespace Farmanji.Data
{
    [System.Serializable]
    public class ItemData
    {
        public string Name;
        public string Id;
        public string ProductId;
        public ItemUnlockMode UnlockMode;
        public float Price;
        public bool Obtained;
        public AvatarPiece AvatarPiece;
        public int AssetsCount; //DEBUG ONLY
        public int CoinsPrice;
        public int GemsPrice;
        public string ItemType;
        
        public string LevelId;
        public string WorldId;
        

        public Sprite PreviewSprite;
        public Sprite MainSprite;
        public Sprite SecondarySprite;
        public Sprite TerciarySprite;

        public ItemData(ItemsResponse response, Sprite previewSprite, Sprite mainSprite, Sprite secondarySprite, Sprite terciarySprite)
        {
            Name = response.name;
            Id = response._id;
            ProductId = response.productId;
            Price = response.price;
            Obtained = !response.disabled;
            ItemType = response.itemType;
            UnlockMode = GetUnlockMode(response.unlockMode);
            AvatarPiece = GetAvatarPiece(response.itemType);
            CoinsPrice = response.coinsReward;
            GemsPrice = response.gemsReward;

            PreviewSprite = previewSprite;
            MainSprite = mainSprite;
            SecondarySprite = secondarySprite;
            TerciarySprite = terciarySprite;

            LevelId = response.levelId;
            WorldId = response.worldId;
        }

        private AvatarPiece GetAvatarPiece(string avatarPiece)
        {
            return avatarPiece switch
            {
                "face" => AvatarPiece.Face,
                "hair" => AvatarPiece.Hair,
                "hat" => AvatarPiece.Hat,
                "trunk" => AvatarPiece.Cloths,
                "legs" => AvatarPiece.Pants,
                "frontHandItem" => AvatarPiece.FrontHandItem,
                "backHandItem" => AvatarPiece.BackHandItem,
                "pet" => AvatarPiece.Pet,
                _ => AvatarPiece.Face
            };
        }

        private ItemUnlockMode GetUnlockMode(string unlockMode)
        {
            return unlockMode switch
            {
                "default" => ItemUnlockMode.Default,
                "progress" => ItemUnlockMode.Progress,
                "buy" => ItemUnlockMode.Buy,
                _ => ItemUnlockMode.Default
            };
        }

    }

    public enum ItemUnlockMode
    {
        Default, Progress, Buy
    }
}