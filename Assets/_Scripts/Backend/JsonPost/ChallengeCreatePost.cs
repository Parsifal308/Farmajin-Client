using System.Collections;
using System.Collections.Generic;
using Farmanji.Managers;
using Newtonsoft.Json;
using UnityEngine;

namespace Farmanji.Data
{
    public class ChallengeCreatePost : ResourcePost
    {
        public ChallengeCreateBody body;
        public override IEnumerator Post(PostBody body)
        {
            yield return StartCoroutine(_jsonPost.TryPost<ChallengesCollection>(null, JsonConvert.SerializeObject(body)));
            yield return StartCoroutine(ResourcesLoaderManager.Instance.Challenges.Load());
        }
    }
}