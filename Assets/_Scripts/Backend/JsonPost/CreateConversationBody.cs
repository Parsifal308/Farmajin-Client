using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    [System.Serializable]
    public class CreateConversationBody : PostBody
    {
        public string[] participants = new string[2];

        public static CreateConversationBody Create(string userId, string friendId)
        {
            CreateConversationBody body = new CreateConversationBody();
            body.participants[0] = userId;
            body.participants[1] = friendId;
            return body;
        }
    }
}