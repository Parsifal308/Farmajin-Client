using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    public class ChallengeCreateBody : PostBody
    {
        public List<string> participants;
        public string gameId;
    }
}