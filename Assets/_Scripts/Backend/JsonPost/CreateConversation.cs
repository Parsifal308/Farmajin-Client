using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using Farmanji.Managers;

namespace Farmanji.Data
{
    public class CreateConversation : ResourcePost
    {
        public CreateConversationResponse response;
        public CreateConversationBody body;
        public override IEnumerator Post(PostBody body)
        {
            yield return StartCoroutine(_jsonPost.TryPost<CreateConversationResponse>(null, JsonConvert.SerializeObject(body)));

            int index = _jsonPost.Response.IndexOf("{\"participants\":");

            if (index != -1)
            {
                string a = _jsonPost.Response.Substring(index);
                string b = a.Substring(0, a.Length - 1);
                response = JsonConvert.DeserializeObject<CreateConversationResponse>(b);
            }
            yield return StartCoroutine(ResourcesLoaderManager.Instance.Conversations.Load());
        }
        // void Start()
        // {
        //     string a = _jsonPost.Response.Substring(_jsonPost.Response.IndexOf("{\"participants\":"));
        //     string b = a.Substring(0, a.Length - 1);
        //     response = JsonConvert.DeserializeObject<CreateConversationResponse>(b);
        // }
    }
}