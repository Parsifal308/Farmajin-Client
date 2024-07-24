using System.Collections;
using System.Collections.Generic;
using Farmanji.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Farmanji.Data
{
    public class ActivityProgresPost : ResourcePost
    {
        public ActivityProgresBody body;
        public override IEnumerator Post(PostBody body)
        {
            yield return StartCoroutine(_jsonPost.TryPost<CreateConversationResponse>(null, JsonConvert.SerializeObject(body)));
        }
    }
}