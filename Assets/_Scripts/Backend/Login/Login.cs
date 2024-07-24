using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

namespace Farmanji.Auth
{
    [CreateAssetMenu]
    public class Login : ScriptableObject
    {
        [Header("DEVELOPMENT:")]
        [SerializeField] private bool debug;
        Regex validateEmailRegex = new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
        [Header("SETTINGS:")]
        [SerializeField] public string loginEndPoint;
        [SerializeField] public string TimeOutMessage;
        [SerializeField] public bool isFirstLogin;

        public bool IsFirstLoginComplete { get { return isFirstLogin; } set { isFirstLogin = value; } }
        public Action OnFirstLoginCompleted;

        private LoginResponse response;
        public LoginResponse Response => response;

        public Action<long> OnLoginFailed;
        public Action OnLoginSuccess;
        
        public Action OnLoginTimeout;
        
        public Action<bool> OnFirstLogin;

        public IEnumerator TryToLogin(string email, string password)
        {
            if (validateEmailRegex.IsMatch(email))
            {
                var connectionTimer = 0f;

                UnityWebRequest request = new UnityWebRequest(loginEndPoint, "POST");
                request.SetRequestHeader("Content-Type", "application/json");
                byte[] bodyRaw = Encoding.UTF8.GetBytes(
                    "{ \"email\": \"" + email +
                    "\", \"password\": \"" + password + "\" }");
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();

                var handler = request.SendWebRequest();
                yield return handler;

                while (!handler.isDone)
                {
                    connectionTimer += Time.deltaTime;

                    if (connectionTimer > 10f)
                    {
                        OnLoginTimeout?.Invoke();
                        break;
                    }

                    yield return null;
                }

                if (request.result == UnityWebRequest.Result.Success)
                {

                    //Debug.Log("OnLoginSuccess!");
                    //if (debug) Debug.Log(request.responseCode);

                    if (request.responseCode == 200)
                    {
                        response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);
                        Debug.Log("login response: " + request.downloadHandler.text);
                        Debug.Log("response.isFirstLogin: " + response.user.IsFirstLogin);

                        if (response.user.IsFirstLogin) { 
                            isFirstLogin = response.user.IsFirstLogin;
                            OnFirstLogin?.Invoke(isFirstLogin);
                        }
                        Debug.Log("cached isFirstLogin: " + isFirstLogin);
                        OnLoginSuccess?.Invoke();
                    }
                    else
                    {
                        OnLoginFailed?.Invoke(request.responseCode);
                    }

                }
                else
                {
                    if (debug) Debug.Log(request.responseCode);
                    OnLoginFailed?.Invoke(-1);
                }
            }
            else
            {
                if (debug) Debug.Log("Bad Email");
                OnLoginFailed?.Invoke(-2);
            }
        }
    }
}

