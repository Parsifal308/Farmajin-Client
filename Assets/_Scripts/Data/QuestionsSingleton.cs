using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    public class QuestionsSingleton : MonoBehaviour
    {
        private static QuestionsSingleton instance;
        public static QuestionsSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<QuestionsSingleton>();
                    if (instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<QuestionsSingleton>();
                        singletonObject.name = "QuestionsSingleton";
                    }
                }
                return instance;
            }
        }

        public Dictionary<string, object> TestsList = new Dictionary<string, object>();

        private void Awake()
        {
          /*   if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            } */
        }
    }
}
