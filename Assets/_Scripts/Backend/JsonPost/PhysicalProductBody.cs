namespace Farmanji.Data
{
    [System.Serializable]
    public class PhysicalProductBody : PostBody
    {
        public string _id;

        public static PhysicalProductBody Create(string id)
        {
            var body = new PhysicalProductBody()
            {
                _id = id
            };
            return body;
        }
    }
}