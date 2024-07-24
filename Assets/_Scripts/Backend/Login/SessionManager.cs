using Farmanji.Data;
using Farmanji.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Farmanji.Auth
{
    public class SessionManager : SingletonPersistent<SessionManager>
    {
        #region FIELDS
        [SerializeField] private UserDataFile userData;
        #endregion

        #region PROPERTIES
        public UserDataFile UserData { get { return userData; } }
        #endregion

        #region METHODS

        public void CreateUserData(LoginResponse loginResponse)
        {
            userData.SetNewData(loginResponse);
        }

        public void LogOut()
        {
            SessionManager.Instance.UserData.AutoLogin = false;
            Farmanji.Ws.WebSocketClient.Instance.Close();
            userData.ClearToken();
            ScenesManager.Instance.ReturnToLogin();

            //PlayerPrefs.DeleteAll();
        }
        #endregion

    }

}

