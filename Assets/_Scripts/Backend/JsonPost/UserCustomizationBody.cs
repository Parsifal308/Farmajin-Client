namespace Farmanji.Data
{
    [System.Serializable]
    public class UserCustomizationBody : PostBody
    {
        public string itemId;
        public string itemType;

        public static UserCustomizationBody Create(string _itemId, string _itemType)
        {
            var userCustomizationBody = new UserCustomizationBody
            {
                itemId = _itemId,
                itemType = _itemType
            };
            return userCustomizationBody;
        }
    }
}