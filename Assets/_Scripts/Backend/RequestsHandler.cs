using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Farmanji.Utilities
{
    public class RequestsHandler
    {


        public static IEnumerator HandleRequest(UnityWebRequest request)
        {
            var handler = request.SendWebRequest();

            yield return handler;

            float startTime = 0;
            while (!handler.isDone)
            {
                startTime += Time.deltaTime;

                if (startTime > 10f)
                {
                    Debug.LogError("Connection to server timed out");
                    break;
                }

                yield return null;
            }

            yield return null;

        }

        public static bool CheckResultStatus(UnityWebRequest request)
        {
            if (request.result == UnityWebRequest.Result.Success)
            {
                return true;
            }

            return false;
        }
    }
}

