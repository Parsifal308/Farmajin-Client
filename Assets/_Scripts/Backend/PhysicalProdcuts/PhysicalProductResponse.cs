namespace Farmanji.Data
{
    [System.Serializable]
    public class PhysicalProductResponse : Response
    {
        public string message;
        public OrderResponse order;
    }

    [System.Serializable]
    public class OrderResponse
    {
        public string _id;
        public string userId;
        public string productId;
        public string code;
    }
}