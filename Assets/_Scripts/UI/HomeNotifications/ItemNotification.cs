using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.UI
{
    public class ItemNotification : NotificationSlot
    {
        #region FIELDS
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private Button goToButton;
        #endregion

        #region PROPERTIES
        public TextMeshProUGUI ItemNameText { get { return itemNameText; } }
        public TextMeshProUGUI MessageText { get { return messageText; } }
        public Button GoToButton { get { return goToButton; } }
        #endregion

        #region UNITY METHODS
        private void Start()
        {
            notificationType = NotificationType.NewItem;
        }
        #endregion
    }
}