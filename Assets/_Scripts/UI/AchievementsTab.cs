using System;
using System.Collections.Generic;
using Farmanji.Data;
using Farmanji.Managers;
using Farmanji.Ws;
using Pathfinding.Examples;
using UnityEngine;

namespace Farmanji.Game
{
    public class AchievementsTab : BaseTab
    {
        #region FIELSD
        [Header("CONTAINERS:")]
        [SerializeField] private Transform achievementSlotsContainer;
        [SerializeField] private Transform detailsSlotsContainer;
        [Header("PREFABS:")]
        [SerializeField] private GameObject achievementSlotPrefab;
        [SerializeField] private GameObject detailSlotPrefab;
        [Header("CONFIGS:")]
        private bool showLocked = true;
        [SerializeField] private bool onlyShowUnlocked = true;
        [SerializeField] private event EventHandler OnBadgeUnlocked;
        #endregion

        #region PROPERTIES
        public Transform AchievementsSlotContainer { get { return achievementSlotsContainer; } }
        public Transform DetailsSlotsContainer { get { return detailsSlotsContainer; } }
        public GameObject AchievementSlotPrefab { get { return achievementSlotPrefab; } }
        public GameObject DetailSlotPrefab { get { return detailSlotPrefab; } }
        public EventHandler OnBadgeUnlockedEvent { get { return OnBadgeUnlocked; } set { OnBadgeUnlocked = value; } }
        #endregion

        #region UNITY METHODS

        private void Start()
        {
            if (onlyShowUnlocked)
            {
                InitializeUnlockedAchievementSlots(ResourcesLoaderManager.Instance.UserAchievements.Achievements);
            }
            else
            {

                InitializeAchievementSlots(ResourcesLoaderManager.Instance.Achievements.Achievements);
            }
            WebSocketMsgsHandler.Instance.OnBadgeUnlockedMsgReceivedEvent += SetBadgeInfo;
        }
        
        public override void Open()
        {
            GetComponent<Animator>().SetTrigger("Show");
            if (achievementSlotsContainer.childCount > 0)
            {
                var achievementSlot = achievementSlotsContainer.GetChild(0).GetComponent<AchievementSlot>();
                if (achievementSlot && achievementSlot.AchievementData != null)
                {
                    InitializeDetail(achievementSlot.AchievementData);
                }
            }
            
        }

        private void SetBadgeInfo(object sender, EventArgs e)
        {
            OnBadgeUnlocked?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region METHODS


        public void InitializeAchievementSlots(List<AchievementData> achievements)
        {
            foreach (var achievement in achievements)
            {
                if (showLocked)
                {
                    var slot = Instantiate(achievementSlotPrefab, achievementSlotsContainer.transform).GetComponent<AchievementSlot>();
                    slot.InitializeSlot(achievement);
                }
                else
                {
                    if (achievement.Unlocked)
                    {
                        var slot = Instantiate(achievementSlotPrefab, achievementSlotsContainer.transform).GetComponent<AchievementSlot>();
                        slot.InitializeSlot(achievement);
                    }
                }
            }
            if (achievements.Count > 0) InitializeDetail(achievements[0]);
        }
        public void InitializeUnlockedAchievementSlots(List<UserAchievementData> unlockedAchievements)
        {
            if (unlockedAchievements == null || unlockedAchievements.Count <= 0) return;
            
            foreach (var achievement in unlockedAchievements[0].Badges)
            {
                var slot = Instantiate(achievementSlotPrefab, achievementSlotsContainer.transform).GetComponent<AchievementSlot>();
                slot.InitializeSlot(achievement);
            }

        }
        #endregion

        public void InitializeDetail(AchievementData data)
        {
            CleanChildren(detailsSlotsContainer.transform);
            var detail = Instantiate(detailSlotPrefab, detailsSlotsContainer.transform).GetComponent<AchievementDetailSlot>();
            detail.InitializeDetail(data);
        }
        private void CleanChildren(Transform parent)
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}

