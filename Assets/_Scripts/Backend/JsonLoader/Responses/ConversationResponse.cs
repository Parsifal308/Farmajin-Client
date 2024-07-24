using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Farmanji.Data
{
    [System.Serializable]
    public class ConversationResponse
    {
        public string _id;
        public string[] participants;
        public MessagesResponse[] messages;
    }
}