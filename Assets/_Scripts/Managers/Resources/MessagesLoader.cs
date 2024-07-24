using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    public class MessagesLoader : ResourceLoader
    {
        #region 
        public List<MessageData> Messages;
        #endregion

        #region PROPERTIES
        public JsonLoader JsonLoader { get { return _jsonLoader; } }
        #endregion

        #region COROUTINES
        public override IEnumerator Load()
        {
            Messages.Clear();
            yield return StartCoroutine(_jsonLoader.LoadFromWeb<MessagesCollection>("?conversationId=64d267dd36de51f544a87877"));

            yield return StartCoroutine(CreateConcreteData());
        }
        public IEnumerator Load(string conversationId)
        {
            Messages.Clear();
            string queryParams = "?conversationId=" + conversationId;
            yield return StartCoroutine(_jsonLoader.LoadFromWeb<MessagesCollection>(queryParams));
            yield return StartCoroutine(CreateConcreteData());
        }
        public override IEnumerator CreateConcreteData()
        {
            MessagesCollection response = _jsonLoader.ResponseInfo as MessagesCollection;

            foreach (var message in response.data)
            {
                Messages.Add(MessageData.Create(message.conversationId, message.senderId, message.text, message.createdAt));
            }
            yield return null;
        }
        #endregion
    }

    [System.Serializable]
    public class MessageData : IMessageData
    {
        #region FIELDS
        [SerializeField] private string conversationId;
        [SerializeField] private string senderId;
        [SerializeField] private string text;
        [SerializeField] private string createdAt;
        #endregion

        #region PROPERTIES
        public string ConversationId { get { return conversationId; } set { conversationId = value; } }
        public string SenderId { get { return senderId; } set { senderId = value; } }
        public string Text { get { return text; } set { text = value; } }
        public string CreatedAt { get { return createdAt; } set { createdAt = value; } }
        #endregion

        public MessageData()
        {

        }
        public static MessageData Create(string conversationId, string senderId, string text, string createdAt)
        {
            MessageData message = new MessageData();
            message.ConversationId = conversationId;
            message.SenderId = senderId;
            message.Text = text;
            message.CreatedAt = createdAt;
            return message;

        }
    }
    public interface IMessageData
    {
        public string ConversationId { get; }
        public string SenderId { get; }
        public string Text { get; }
        public string CreatedAt { get; }
    }
}