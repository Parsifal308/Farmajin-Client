using Farmanji.Data;

namespace Farmanji.Auth
{
    [System.Serializable]
    public class LoginResponse
    {
        public Data.UserInfo user;
        public string token;
        public bool isFirstLogin;
    }

}
