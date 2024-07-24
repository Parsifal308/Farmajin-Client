using Farmanji.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class AchievementSlot : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private Image itemIcon;
        private AchievementData _achievementData;
        
        public AchievementData AchievementData
        {
            get { return _achievementData; }
        }
        #endregion

        #region METHODS
        public void InitializeSlot(AchievementData data)
        {
            _achievementData = data;
            itemIcon.sprite = data.Image;
            UnityAction action = () => GetComponentInParent<AchievementsTab>().InitializeDetail(data);
            GetComponent<Button>().onClick.AddListener(action);
        }
        #endregion
    }
}