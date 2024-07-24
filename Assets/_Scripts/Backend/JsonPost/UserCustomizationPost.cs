using System.Collections;
using Newtonsoft.Json;
using UnityEngine;

namespace Farmanji.Data
{
    public class UserCustomizationPost : ResourcePost
    {
        public PostUserCustomizationResponse Response;
        public override IEnumerator Post(PostBody body)
        {
            yield return StartCoroutine(_jsonPost.TryPost<PostUserCustomizationResponse>("", JsonConvert.SerializeObject(body)));
        }
    }
}