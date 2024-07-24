using System;
using System.Collections;
using Farmanji.Game;
using Farmanji.Managers;
using Newtonsoft.Json;
using UnityEngine;

namespace Farmanji.Data
{
    public class ItemPost : ResourcePost
    {
        public BuyItemResponse BuyItemResponse;
        
        public JsonPost GetPost
        {
            get { return _jsonPost; }
        }
        public override IEnumerator Post(PostBody body)
        {
            yield return StartCoroutine(_jsonPost.TryPost<BuyItemResponse>("", JsonConvert.SerializeObject(body)));
            yield return new WaitForEndOfFrame();
            yield return StartCoroutine(ResourcesLoaderManager.Instance.Economy.Load());
        }
    }
}