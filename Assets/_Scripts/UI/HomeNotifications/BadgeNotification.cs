using System.Collections;
using System.Collections.Generic;
using Farmanji.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.UI
{
    public class BadgeNotification : NotificationSlot
    {
        #region FIELDS
        [SerializeField] private TextMeshProUGUI badgeNameText;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private Button goToButton;
        #endregion

        #region PROPERTIES
        public TextMeshProUGUI BadgeNameText { get { return badgeNameText; } }
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