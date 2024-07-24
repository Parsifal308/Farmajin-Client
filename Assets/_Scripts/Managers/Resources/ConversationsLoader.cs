using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    public class ConversationsLoader : ResourceLoader
    {
        #region FIELDS
        public List<ConversationData> Conversations;
        #endregion

        #region COROUTINES
        public override IEnumerator Load()
        {
            Conversations.Clear();
            yield return StartCoroutine(_jsonLoader.LoadFromWeb<ConversationCollection>());

            yield return StartCoroutine(CreateConcreteData());
        }
        public override IEnumerator CreateConcreteData()
        {
            ConversationCollection response = _jsonLoader.ResponseInfo as ConversationCollection;

            foreach (var conversation in response.data)
            {
                Conversations.Add(ConversationData.Create(conversation._id, conversation.participants));
            }
            yield return null;
        }
        #endregion

        #region METHODS
        public bool HasConversation(string user1, string user2)
        {
            for (int i = 0; i < Conversations.Count; i++)
            {
                if ((Conversations[i].Participants[0] == user1 && Conversations[i].Participants[1] == user2) || (Conversations[i].Participants[0] == user2 && Conversations[i].Participants[1] == user1))
                {
                    return true;
                }
            }
            return false;
        }
        public int GetConversationIndex(string user1, string user2)
        {
            for (int i = 0; i < Conversations.Count; i++)
            {
                if ((Conversations[i].Participants[0] == user1 && Conversations[i].Participants[1] == user2) || (Conversations[i].Participants[0] == user2 && Conversations[i].Participants[1] == user1))
                {
                    return i;
                }
            }
            return -1;
        }
        public string GetConversationId(string user1, string user2)
        {
            for (int i = 0; i < Conversations.Count; i++)
            {
                if ((Conversations[i].Participants[0] == user1 && Conversations[i].Participants[1] == user2) || (Conversations[i].Participants[0] == user2 && Conversations[i].Participants[1] == user1))
                {
                    return Conversations[i].Id;
                }
            }
            return "";
        }
        #endregion
    }
    [System.Serializable]
    public class ConversationData
    {
        #region FIELDS
        [SerializeField] private string id;
        [SerializeField] private string[] participants;
        #endregion

        #region PROPERTIES
        public string Id { get { return id; } set { id = value; } }
        public string[] Participants { get { return participants; } set { participants = value; } }
        #endregion

        public static ConversationData Create(string id, string[] participants)
        {
            ConversationData conversation = new ConversationData { id = id, participants = participants };
            return conversation;
        }
    }
}