namespace Farmanji.Data
{
    [System.Serializable]
    public class OrdersResponse 
    {
        public string _id;
        public string code;
        public string userId;
        public ProductsResponse productId;
        public string userName;
        public string productName;
        public string requestDate;
        public string deliveryDate;
        public int quantity;
        public int coinsReward;
        public int gemsReward;
        public string status;
        public string createdAt;
        public string updatedAt;
        public string deletedAt;
    }
}