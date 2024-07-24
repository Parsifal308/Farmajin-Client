using System;
using System.Collections;
using System.Collections.Generic;
using Farmanji.Data;
using UnityEngine;

namespace Farmaji.Data
{
    public class ChallengesLoader : ResourceLoader
    {
        public List<ChallengeData> Challenges = new List<ChallengeData>();
        public override IEnumerator Load()
        {
            Challenges.Clear();
            yield return StartCoroutine(_jsonLoader.LoadFromWeb<ChallengesCollection>());
            yield return StartCoroutine(CreateConcreteData());
        }
        public IEnumerator Load(string conversationId)
        {
            Challenges.Clear();
            string queryParams = "?conversationId=" + conversationId;
            yield return StartCoroutine(_jsonLoader.LoadFromWeb<ChallengesCollection>(queryParams));
            yield return StartCoroutine(CreateConcreteData());
        }
        public override IEnumerator CreateConcreteData()
        {
            ChallengesCollection response = _jsonLoader.ResponseInfo as ChallengesCollection;

            foreach (var challenge in response.data)
            {
                Challenges.Add(ChallengeData.Create(
                    challenge._id,
                    challenge.participants,
                    challenge.gameId,
                    challenge.challenger,
                    challenge.playersScore,
                    challenge.playerCompletion,
                    challenge.playerCompletionTime,
                    challenge.status,
                    challenge.gemsReward,
                    challenge.coinsReward)
                );
            }
            yield return null;
        }

        internal ChallengeData FindChallengesByID(string id)
        {
            foreach (var challenge in Challenges)
            {
                if (challenge.Id == id)
                {
                    return challenge;
                }
            }
            Debug.Log("NO SE ENCONTRO RETO CON DICHO ID");
            return null;
        }
    }
    [System.Serializable]
    public class ChallengeData
    {
        public string Id;
        public List<string> Participants = new List<string>();
        public string GameId;
        public string Challenger;
        public List<string> PlayersScore = new List<string>();
        public List<string> PlayerCompletion = new List<string>();
        public List<string> PlayerCompletionTime = new List<string>();
        public string Status;
        public int GemsReward;
        public int CoinsReward;

        internal static ChallengeData Create(string id, List<string> participants, string gameId, string challenger, List<string> playersScore, List<string> playerCompletion, List<string> playerCompletionTime, string status, int gemsReward, int coinsReward)
        {
            ChallengeData challenge = new ChallengeData();
            challenge.Participants = participants;
            challenge.GameId = gameId;
            challenge.Challenger = challenger;
            challenge.PlayersScore = playersScore;
            challenge.PlayerCompletion = playerCompletion;
            challenge.PlayerCompletionTime = playerCompletionTime;
            challenge.Status = status;
            challenge.GemsReward = gemsReward;
            challenge.CoinsReward = coinsReward;
            return challenge;
        }
    }
}