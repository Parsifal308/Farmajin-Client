using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Farmanji.Data
{
    [System.Serializable]
    public class VideoData
    {
        public string Name;
        public string Description;
        public string clip;

        public string World;
        public string Level;
        public int StageIndex;

        public VideoData(string theName, string description, string clipUrl, string worldName, string levelName, int stageIndex)
        {
            Name = theName;
            Description = description;
            clip = clipUrl;

            World = worldName;
            Level = levelName;
            StageIndex = stageIndex;
        }

    }
}

