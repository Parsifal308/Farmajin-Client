using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Game
{
    public class FriendsTab : BaseTab
    {
        #region FIELDS
        [SerializeField] private ContactsPanel contacts;
        #endregion

        #region PROPERTIES
        public ContactsPanel Contacts { get { return contacts; } }
        #endregion

        #region UNITY METHODS

        #endregion

        #region PUBLIC METHODS
        public void InitializeFriends()
        {

        }
        #endregion

        #region PRIVATE METHODS
        #endregion
    }
}