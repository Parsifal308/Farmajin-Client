using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.UI
{
    public class ChallengeNotification : NotificationSlot
    {
        #region FIELDS
        [SerializeField] private TextMeshProUGUI challengeNameText;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private Button goToButton;
        #endregion

        #region PROPERTIES
        public TextMeshProUGUI ChallengeNameText { get { return challengeNameText; } }
        public TextMeshProUGUI MessageText { get { return messageText; } }
        public Button GoToButton { get { return goToButton; } }
        #endregion

        #region UNITY METHODS
        private void Start()
        {
            notificationType = NotificationType.Achievement;
        }
        #endregion
    }
}