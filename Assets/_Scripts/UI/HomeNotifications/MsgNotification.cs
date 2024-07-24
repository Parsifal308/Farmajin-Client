using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Farmanji.UI
{
    public class MsgNotification : NotificationSlot
    {
        #region FIELDS
        [SerializeField] private TextMeshProUGUI friendNameText;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private Button goToButton;
        #endregion

        #region PROPERTIES
        public TextMeshProUGUI FriendNameText { get { return friendNameText; } }
        public TextMeshProUGUI MessageText { get { return messageText; } }
        public Button GoToButton { get { return goToButton; } }
        #endregion

        #region UNITY METHODS
        private void Start()
        {
            notificationType = NotificationType.Msg;
        }
        #endregion

    }
}