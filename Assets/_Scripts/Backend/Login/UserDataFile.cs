using System.Collections;
using System.Collections.Generic;
using Farmanji.Auth;
using UnityEngine;

[CreateAssetMenu]
public class UserDataFile : ScriptableObject
{
    #region FIELDS
    [Header("BASIC INFO:")]
    [SerializeField] public Farmanji.Data.UserInfo _basicInfo;
    [Header("LOGIN INFO:")]
    [SerializeField] private bool firstLogin;
    [SerializeField] private bool autoLogin;
    [SerializeField] private string user;
    [SerializeField] private string password;
    [SerializeField] private string _loginToken;
    #endregion

    #region PROPERTIES
    public bool FirstLogin {get{return firstLogin;}}
    public bool AutoLogin { get { return autoLogin; } set { autoLogin = value; } }
    public string User { get { return user; } set { user = value; } }
    public string Password { get { return password; } set { password = value; } }
    public string Token { get { return _loginToken; } set { _loginToken = value; } }
    public Farmanji.Data.UserInfo UserInfo { get { return _basicInfo; } set { _basicInfo = value; } }
    #endregion

    #region METHODS
    public void SetNewData(LoginResponse loginResponse)
    {
        _basicInfo = loginResponse.user;
        _loginToken = loginResponse.token;
        //Debug.Log("userdata token: " + _token);
    }
    public bool HasLoginData()
    {
        return (user != "" && password != "") ? true : false;
    }

    public void ClearToken()
    {
        _loginToken = "";
    }
    #endregion

}
