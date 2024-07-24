using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    [System.Serializable]
    public class ChallengesCollection : Response
    {
        public List<ChallengesResponse> data;
    }
}