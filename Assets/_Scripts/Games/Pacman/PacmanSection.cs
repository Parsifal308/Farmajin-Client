using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Pacman
{
    public class PacmanSection : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private event EventHandler OnPacmanEntered, OnGhostEntered;
        public GameObject lastTriggered;
        #endregion

        #region PROPERTIES
        public GameObject LastTriggered { get { return lastTriggered; } }
        public EventHandler OnPacmanEnteredEvent { get { return OnPacmanEntered; } set { OnPacmanEntered = value; } }
        public EventHandler OnGhostEnteredEvent { get { return OnGhostEntered; } set { OnGhostEntered = value; } }
        #endregion

        #region UNITY METHODs
        private void OnTriggerEnter(Collider other)
        {
            lastTriggered = other.gameObject;
            if (other.gameObject.tag == "Player")
            {
                OnPacmanEntered?.Invoke(this, EventArgs.Empty);
                return;
            }
            if (other.gameObject.tag == "Enemy")
            {
                OnGhostEntered?.Invoke(this, EventArgs.Empty);
                return;
            }
        }
        #endregion
    }
}