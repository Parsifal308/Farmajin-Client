using Farmanji.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    public class VideosLoader : ResourceLoader
    {
        public List<VideoData> List = new List<VideoData>();

        public override IEnumerator Load()
        {
            yield return StartCoroutine(_jsonLoader.LoadFromWeb<VideosCollection>());

            yield return StartCoroutine(CreateConcreteData());
            
        }

        public override IEnumerator CreateConcreteData()
        {
            VideosCollection response = _jsonLoader.ResponseInfo as VideosCollection;
            Debug.Log(response.videos.Length);
            foreach(var videoElement in response.videos)
            {
                List.Add(new VideoData(videoElement.Title, videoElement.Description, videoElement.Url,
                    videoElement.World, videoElement.Level, videoElement.Stage));
            }

            yield return null;
            
        }
    }

}
