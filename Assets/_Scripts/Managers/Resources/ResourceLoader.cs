using Farmanji.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Farmanji.Data
{
    public abstract class ResourceLoader : MonoBehaviour
    {
        [SerializeField] protected JsonLoader _jsonLoader;
        public Sprite DownloadedSprite => downloadedSprite;
        private Sprite downloadedSprite;

        public abstract IEnumerator Load();

        public virtual IEnumerator CreateConcreteData()
        {
            yield return null;
        }

        

        public IEnumerator DownloadImage(string url)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            yield return StartCoroutine(RequestsHandler.HandleRequest(request));

            if (RequestsHandler.CheckResultStatus(request))
            {
                Texture2D myTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                downloadedSprite = Sprite.Create(myTexture, new Rect(0f, 0f, myTexture.width, myTexture.height), Vector2.one * 0.5f);
            }
        }

    }
}

