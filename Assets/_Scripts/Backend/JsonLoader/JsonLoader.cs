using Farmanji.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Farmanji.Data
{
    [CreateAssetMenu]
    public class JsonLoader : ScriptableObject
    {
        [SerializeField] public string resourceEndPoint;
        [SerializeField] private TextAsset _localJson;

        private Response lastResponse;

        private Response responseInfo;
        public Response ResponseInfo { get => responseInfo; }

        public Action OnSuccess;
        public Action OnTimeout;
        public Action<long> OnFailed;

        public IEnumerator LoadFromWeb<T>(string queryParams = null, string rootField = null) where T : Response
        {
            if (resourceEndPoint == "")
            {
                LoadJsonDirectly<T>();
                yield break;
            }

            WWWForm form = new WWWForm();
            form.AddField("page", "1");
            form.AddField("perPage", "100");
            form.AddField("worldId", "myData");

            string currentToken = SessionManager.Instance.UserData.Token;
            UnityWebRequest request = new UnityWebRequest(resourceEndPoint + queryParams, "GET"); //queryParams == null ? new UnityWebRequest(resourceEndPoint, "GET")  : UnityWebRequest.Post(resourceEndPoint, form);
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", currentToken);

            /* byte[] bodyRaw = Encoding.UTF8.GetBytes(
                  "{ \"data\": \"" + currentToken + "\" }") +
                  "{ \"token\": \"" + token +
                 "{ \"apisecret\": \"" + apisecret +
                  "\", \"password\": \"" + password + "\" }");
             request.uploadHandler = new UploadHandlerRaw(bodyRaw);*/
            request.downloadHandler = new DownloadHandlerBuffer();
            var handler = request.SendWebRequest();
            yield return handler;

            float startTime = 0;
            while (!handler.isDone)
            {
                startTime += Time.deltaTime;

                if (startTime > 10f)
                {
                    OnTimeout?.Invoke();
                    yield break;
                }

                yield return null;
            }

            if (request.result == UnityWebRequest.Result.Success)
            {


                if (request.responseCode == 200)
                {
                    string jsonText = rootField != null ? "{" + "\"" + rootField + "\"" + ":" + request.downloadHandler.text + "}" : request.downloadHandler.text;
                    Debug.Log(jsonText);
                    responseInfo = JsonUtility.FromJson<T>(jsonText);
                }
                else
                {
                    OnFailed?.Invoke(request.responseCode);
                }
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }

        private void LoadJsonDirectly<T>() where T : Response
        {
            Debug.Log("Loading JSON Direcly");
            responseInfo = JsonUtility.FromJson<T>(_localJson.text);
        }
    }
}

