using Farmanji.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class ChallengeSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _challengeNameText;
        [SerializeField] private TextMeshProUGUI _challengeDescriptionText;
        [SerializeField] private Image _challengeStateImage;
        [SerializeField] private Sprite _challengeCompletedSprite;
        [SerializeField] private Sprite _challengeIncompletedSprite;

        public bool IsCompleted { get; private set; }

        public void InitializeSlot(ActivityData activityData)
        {
            _challengeNameText.SetText(activityData.Name);
            _challengeDescriptionText.SetText(activityData.Description);
            _challengeStateImage.sprite = _challengeIncompletedSprite;
        }

        public void CompleteChallenge()
        {
            _challengeStateImage.sprite = _challengeCompletedSprite;
            IsCompleted = true;
        }
    }
}