using Farmanji.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    [System.Serializable]
    public class UserData
    {
        [SerializeField] private UserInfo _basicInfo;
        [SerializeField] private string _loginToken;

        public UserInfo UserInfo => _basicInfo;
        public string Token => _loginToken;

        public void SetNewData(LoginResponse loginResponse)
        {
            _basicInfo = loginResponse.user;
            _loginToken = loginResponse.token;
            //Debug.Log("userdata token: " + _token);
        }

    }
    [System.Serializable]
    public class UserInfo
    {
        #region FIELDS
        [SerializeField] private string id;
        [SerializeField] private string name;
        [SerializeField] private string email;
        [SerializeField] private string role;
        [SerializeField] private string company;
        [SerializeField] private bool isFirstLogin;
        #endregion

        #region PROPERTIES
        public bool IsFirstLogin { get { return isFirstLogin; } }
        public string Id { get { return id; } }
        public string Name { get { return name; } }
        public string Email { get { return email; } }
        
        public string Role { get { return GetRole(); } }

        private string GetRole()
        {
            return role switch
            {
                "student" => "Estudiante",
                "teacher" => "Profesor",
                "admin" => "Administrador",
                _ => "Estudiante"
            };
        }
        public string Company { get { return company; } }
        #endregion
    }
}

