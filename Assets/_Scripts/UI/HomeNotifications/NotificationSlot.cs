using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.UI
{
    public abstract class NotificationSlot : MonoBehaviour
    {
        [SerializeField] protected Image icon;
        [SerializeField] protected NotificationType notificationType = NotificationType.None;

        public Image Icon { get { return icon; } }
        protected enum NotificationType
        {
            None,
            Msg,
            Achievement,
            FriendChallenge,
            NewItem,
            NewProduct
        }
    }
}