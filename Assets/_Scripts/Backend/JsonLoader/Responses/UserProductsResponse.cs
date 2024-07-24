namespace Farmanji.Data
{
    [System.Serializable]
    public class UserProductsResponse
    {
        public string _id;
        public string userId;
        public ProductsResponse[] products;
        public string createdAt;
        public string updatedAt;
    }
}