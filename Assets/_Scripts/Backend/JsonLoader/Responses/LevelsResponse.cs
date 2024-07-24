using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    [System.Serializable]
    public class LevelsResponse
    {
        public string _id;
        public string name;
        public string backgroundImg;
        public string worldId;
        public string worldImage;
        public bool completed;
        public StagesResponse[] stages;

    }
}
