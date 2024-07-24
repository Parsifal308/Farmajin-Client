using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Pacman
{
    public class PacmanTeleport : MonoBehaviour
    {
        #region FIELDS
        [Header("DEVELOPMENT:")]
        [SerializeField] private bool debug;
        [Header("SETTINGS")]
        [SerializeField] private PacmanTeleport destination;
        [SerializeField] private bool canUse;
        [SerializeField] private float cooldown = 0.25f;
        [SerializeField] private event EventHandler OnTeleport;
        #endregion

        #region PROPERTIES
        public bool CanUse { get { return canUse; } set { canUse = value; } }
        public float CooldownTime { get { return cooldown; } set { cooldown = value; } }
        #endregion

        #region UNITY METHODS
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
            {
                if (debug) Debug.Log(gameObject.name + " eated");
                if (canUse)
                {
                    other.transform.position = destination.transform.position;
                    StartCoroutine(Cooldown(cooldown));
                    StartCoroutine(destination.Cooldown(destination.CooldownTime));
                    OnTeleport?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        IEnumerator Cooldown(float time)
        {
            canUse = false;
            float timer = 0f;
            while (canUse == false)
            {
                timer += Time.deltaTime;
                if (timer >= time)
                {
                    canUse = true;

                }
                yield return null;
            }

        }
        #endregion
    }
}