using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    [System.Serializable]
    public class CreateConversationResponse : Response
    {
        public string _id;
        public string[] participants;
        public string createdAt;

        public static CreateConversationResponse Create(string id, string[] participants, string createdAt)
        {
            CreateConversationResponse response = new CreateConversationResponse
            {
                _id = id,
                participants = participants,
                createdAt = createdAt
            };
            return response;
        }
    }
}