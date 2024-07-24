namespace Farmanji.Data
{
    [System.Serializable]
    public class ItemBody : PostBody
    {
        public string productId;
        public string _id;

        public static ItemBody CreateItemBody(string productId, string userId)
        {
            var itemBody = new ItemBody
            {
                productId = productId,
                _id = userId
            };
            return itemBody;
        }
    }

    [System.Serializable]
    public class RequestBody
    {
        public string productId;
    }
    
    [System.Serializable]
    public class UserItemBody
    {
        public string _id;
    }
}