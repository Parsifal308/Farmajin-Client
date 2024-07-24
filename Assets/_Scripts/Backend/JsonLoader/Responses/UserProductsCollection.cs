namespace Farmanji.Data
{
    [System.Serializable]
    public class UserProductsCollection : Response
    {
        public UserProductsResponse[] data;
    }
    
    [System.Serializable]
    public class UserOrdersCollection : Response
    {
        public UserProductsResponse[] orders;
    }
}