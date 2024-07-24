using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Farmanji.Managers;
using System;
using UnityEngine.Events;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

/*
 * It manages username and password for login to webserver
*/
namespace Farmanji.Auth
{
    public class LoginHandler : MonoBehaviour
    {
        #region FIELDS
        private string user;
        private string pass;
        [SerializeField] private Login _loginData;
        [Header("REFERENCES")]
        [SerializeField] private TMP_InputField _userField;
        [SerializeField] private TMP_InputField _passwordField;
        [SerializeField] private Button loginButton;
        [Header("EVENTS:")]
        [Space(25), SerializeField] private UnityEvent OnSuccessLogin;
        [SerializeField] private UnityEvent OnFailedLogin;
        #endregion

        #region UNITY METHODS
        private void Start()
        {
            _loginData.OnLoginFailed += OnLoginFailed;
            _loginData.OnLoginSuccess += OnLoginSuccess;
            if (SessionManager.Instance.UserData.AutoLogin)
            {
                _userField.DeactivateInputField();
                _passwordField.DeactivateInputField();
                if (IsTokenValid())
                {
                    StartCoroutine(_loginData.TryToLogin(SessionManager.Instance.UserData.User, SessionManager.Instance.UserData.Password));
                }
                else
                {
                    _userField.ActivateInputField();
                    _passwordField.ActivateInputField();
                    ClearLogInfo();
                }
            }
            else
            {
                _userField.ActivateInputField();
                _passwordField.ActivateInputField();
            }
        }

        private void OnDisable()
        {
            _loginData.OnLoginFailed -= OnLoginFailed;
            _loginData.OnLoginSuccess -= OnLoginSuccess;
        }
        #endregion

        #region PUBLIC METHODS
        public void OnLoginClick()
        {
            user = _userField.text;
            pass = _passwordField.text;
            loginButton.interactable = false;
            StartCoroutine(_loginData.TryToLogin(_userField.text, _passwordField.text));
        }
        public void OnLoginFailed(long codeResponse)
        {
            ClearLogInfo();
            OnFailedLogin?.Invoke();
            loginButton.interactable = true;
        }
        public void OnLoginSuccess()
        {
            SessionManager.Instance.UserData.AutoLogin = true;
            if (SessionManager.Instance.UserData.AutoLogin && !SessionManager.Instance.UserData.HasLoginData())
            {
                SaveLoginInfo(user, pass, true);
            }
            OnSuccessLogin?.Invoke();
            SessionManager.Instance.CreateUserData(_loginData.Response);
            ScenesManager.Instance.LoadHome();
        }
        #endregion

        #region PRIVATE METHODS
        private bool IsTokenValid()
        {
            if (SessionManager.Instance.UserData.Token != "")
            {
                JwtSecurityToken token = new JwtSecurityToken(jwtEncodedString: SessionManager.Instance.UserData.Token);
                long expirationDate = long.Parse(token.Claims.First(c => c.Type == "exp").Value);

                return expirationDate - DateTimeOffset.UtcNow.ToUnixTimeSeconds() <= 0 ? false : true;
            }
            return false;
        }
        private void SaveLoginInfo(string user, string pass, bool autoLogin)
        {
            SessionManager.Instance.UserData.User = user;
            SessionManager.Instance.UserData.Password = pass;
            SessionManager.Instance.UserData.AutoLogin = autoLogin;
        }
        private void ClearLogInfo()
        {
            SessionManager.Instance.UserData.User = "";
            SessionManager.Instance.UserData.Password = "";
            SessionManager.Instance.UserData.AutoLogin = false;
            SessionManager.Instance.UserData.Token = "";
        }
        #endregion
    }
}

