using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    [System.Serializable]
    public class VideosResponse : Response
    {
        public string Title;
        public string Description;
        public string Url;
        public string Thumbnail;
        public string World;
        public string Level;
        public int Stage;

    }
}
