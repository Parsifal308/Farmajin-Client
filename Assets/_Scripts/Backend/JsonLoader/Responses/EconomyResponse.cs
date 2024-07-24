namespace Farmanji.Data
{
    [System.Serializable]
    public class EconomyResponse : Response
    {
        public string _id;
        public string userId;
        public int coins;
        public int gems;
        public string createdAt;
        public string updatedAt;
    }
}