using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Farmanji.Data;
using UnityEngine;
using UnityEngine.Networking;

namespace Farmanji.Chat
{
    [CreateAssetMenu]
    public class Conversation : ScriptableObject
    {
        #region FIELDS
        [SerializeField] public string createConversationEndpoint;

        private CreateConversationResponse response;
        public CreateConversationResponse Response => response;

        public Action<long> OnCreateConversationFailed;
        public Action OnCreateConversationSuccess;
        public Action OnCreateConversationTimeout;
        #endregion
        public IEnumerator TryToCreate(string userId, string friendId)
        {
            var connectionTimer = 0f;

            UnityWebRequest request = new UnityWebRequest(createConversationEndpoint, "POST");
            request.SetRequestHeader("Content-Type", "application/json");
            byte[] bodyRaw = Encoding.UTF8.GetBytes("{\"participants\":[" + userId + "," + friendId + "]}");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            var handler = request.SendWebRequest();
            yield return handler;

            while (!handler.isDone)
            {
                connectionTimer += Time.deltaTime;
                if (connectionTimer > 10f)
                {
                    OnCreateConversationTimeout?.Invoke();
                    break;
                }
                yield return null;
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                //Debug.Log("OnLoginSuccess!");
                //Debug.Log(request.responseCode);
                if (request.responseCode == 200)
                {
                    response = JsonUtility.FromJson<CreateConversationResponse>(request.downloadHandler.text);
                    Debug.Log("login response: " + request.downloadHandler.text);
                    OnCreateConversationSuccess?.Invoke();
                }
                else
                {
                    OnCreateConversationFailed?.Invoke(request.responseCode);
                }
            }
            else
            {
                OnCreateConversationFailed?.Invoke(-1);
            }
        }
    }
}
