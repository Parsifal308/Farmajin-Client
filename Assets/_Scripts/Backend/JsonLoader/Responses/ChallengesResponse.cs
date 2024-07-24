using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    [System.Serializable]
    public class ChallengesResponse
    {
        public string _id;
        public List<string> participants = new List<string>();
        public string gameId;
        public string challenger;
        public List<string> playersScore = new List<string>();
        public List<string> playerCompletion = new List<string>();
        public List<string> playerCompletionTime = new List<string>();
        public string status;
        public int gemsReward;
        public int coinsReward;
    }
}