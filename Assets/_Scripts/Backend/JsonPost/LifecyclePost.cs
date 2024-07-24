using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    public class LifecyclePost : ResourcePost
    {
        private void Start()
        {
            Debug.Log("Application is Starting....");
            StartCoroutine(Post(LifecyclePostBody.CreateItemBody("start")));
        }
        //void OnApplicationQuit()
        //{
        //    Debug.Log("Application is closing....");
        //    StartCoroutine(Post(LifecyclePostBody.CreateItemBody("end")));
        //    StartCoroutine(Wait(3f));
        //}
        public override IEnumerator Post(PostBody body)
        {
            yield return StartCoroutine(_jsonPost.TryPost<Response>("", JsonConvert.SerializeObject(body)));
        }
        IEnumerator Wait(float t)
        {
            yield return new WaitForSeconds(t);
        }
    }

}