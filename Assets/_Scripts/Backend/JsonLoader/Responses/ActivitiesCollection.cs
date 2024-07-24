using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    [System.Serializable]
    public class ActivitiesCollection : Response
    {
        public List<ActivitiesResponse> data;
    }
}