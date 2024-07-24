using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Farmanji.Auth;
using UnityEngine;
using UnityEngine.Networking;

namespace Farmanji.Data
{
    [CreateAssetMenu]
    public class JsonPost : ScriptableObject
    {
        #region FIELDS
        [Header("DEVELOPMENT:")]
        [SerializeField] private bool debug;
        [Header("SETTINGS:")]
        [SerializeField] public string postEndPoint;

        [SerializeField] private string response;
        private UnityWebRequest request;
        public string Response => response;

        public Action OnSuccess;
        public Action OnTimeout;
        public Action<long> OnFailed;
        public UnityWebRequest Request { get { return request; } }
        #endregion

        #region PROPERTIES
        public string GetResponse { get { return response; } }
        #endregion

        #region METHODS
        public IEnumerator TryPost<T>(string queryParams = null, string body = null) where T : Response
        {
            var connectionTimer = 0f;

            request = new UnityWebRequest(postEndPoint + queryParams, "POST");
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", SessionManager.Instance.UserData.Token);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            var handler = request.SendWebRequest();
            yield return handler;

            while (!handler.isDone)
            {
                connectionTimer += Time.deltaTime;

                if (connectionTimer > 10f)
                {
                    if (debug) Debug.Log("[" + this.GetType() + "] -> Connection Timeout");
                    OnTimeout?.Invoke();
                    break;
                }
                yield return null;
            }
            if (request.result == UnityWebRequest.Result.Success)
            {
                if (request.responseCode == 200)
                {
                    response = request.downloadHandler.text;
                    if (debug) Debug.Log("[" + this.GetType() + "] -> Post response: " + request.downloadHandler.text);
                    OnSuccess?.Invoke();
                }
                else
                {
                    response = request.downloadHandler.text;
                    OnFailed?.Invoke(request.responseCode);
                }
            }
            else
            {
                response = request.downloadHandler.text;
                if (debug) Debug.Log("[" + this.GetType() + "] -> Connection Failed");
                OnFailed?.Invoke(-1);
            }
        }
        #endregion
    }
}