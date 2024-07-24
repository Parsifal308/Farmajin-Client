using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Farmanji.Data
{
    [System.Serializable]
    public class LifecyclePostBody : PostBody
    {
        public string type;
        public static LifecyclePostBody CreateItemBody(string type)
        {
            var lifecycleBody = new LifecyclePostBody
            {
                type = type,

            };
            return lifecycleBody;
        }
    }
}