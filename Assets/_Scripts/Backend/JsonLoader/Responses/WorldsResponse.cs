using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    [System.Serializable]
    public class WorldsResponse
    {
        public string _id;
        public string name;
        public string description;
        public string iconImage;
        public string backgroundImage;
        public string companyId;
        public bool completed;
        public string createdAt;
        public string updatedAt;
        public int __v;
        public int levelLimit;
        public int levelQuantity;

    }
}

