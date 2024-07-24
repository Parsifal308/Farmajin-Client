using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    [System.Serializable]
    public class MessagesResponse
    {
        public string conversationId;
        public string senderId;
        public string text;
        public string createdAt;
    }
}