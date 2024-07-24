using System.Collections.Generic;
using System.Linq;
using Farmanji.Data;
using Farmanji.Managers;
using UnityEngine;

namespace Farmanji.Game
{
    public class WeeklyChallengePanel : MonoBehaviour
    {
        [SerializeField] private Transform _challengeList;
        [SerializeField] private int _challengeListMaxCount;
        [SerializeField] private ChallengeSlot _challengeSlotPrefab;
        
        private List<ChallengeSlot> _challengeSlots;
        private WorldsLoader _worldsLoader;

        [SerializeField] private int challengesCompleted;

        private void Start()
        {
            InitializePanel();
        }

        private void InitializePanel()
        {
            _worldsLoader = ResourcesLoaderManager.Instance.Worlds;
            _challengeSlots = new List<ChallengeSlot>();
            var challengeCount = 0;
            foreach (var worldData in _worldsLoader.List)
            {
                foreach (var levelData in worldData.levels)
                {
                    foreach (var stageData in levelData.stages)
                    {
                        foreach (var activityData in stageData.Activities)
                        {
                            if (activityData.Completed) continue;
                            
                            var challengeSlot = Instantiate(_challengeSlotPrefab, _challengeList);
                            challengeSlot.InitializeSlot(activityData);

                            activityData.OnActivityCompleted += challengeSlot.CompleteChallenge;
                            activityData.OnActivityCompleted += GiveRewards;
                                
                            challengeCount++;
                            _challengeSlots.Add(challengeSlot);
                                
                            if (challengeCount >= _challengeListMaxCount) return;
                        }
                    }
                }
            }
        }

        private void GiveRewards()
        {
            Debug.Log("Weekly Activity Completed");
            
            if (_challengeSlots == null || _challengeSlots.Count <= 0) return;
            
            challengesCompleted = _challengeSlots.Count(challengeSlot => challengeSlot.IsCompleted);

            if (challengesCompleted < _challengeSlots.Count) return;
            
            Debug.Log("Weekly Challenges Completed, Adding Gems");
                
            CurrencyManager.Instance.AddGems(1000);
            ResourcesLoaderManager.Instance.Economy.PostWeeklyChellengeEconomy();
        }
    }
}