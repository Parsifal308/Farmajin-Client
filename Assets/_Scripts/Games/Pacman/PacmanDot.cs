using System;
using System.Collections;
using System.Collections.Generic;
using Farmanji.Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Farmanji.Pacman
{
    public class PacmanDot : MonoBehaviour
    {
        #region FIELDS
        [Header("DEVELOPMENT:")]
        [SerializeField] private bool debug;
        [SerializeField] private PacDotData data;
        private SpriteRenderer sprite;

        [SerializeField] private UnityEvent OnEaten;

        #endregion

        #region PROPERTIES
        public UnityEvent OnEatenEvent { get { return OnEaten; } set { OnEaten = value; } }
        public PacDotData Data { get { return data; } }


        #endregion

        #region UNITY METHODS
        protected virtual void Awake()
        {
            sprite = GetComponentInChildren<SpriteRenderer>();
            sprite.sprite = data.Sprite;
        }
        #endregion

        #region PUBLIC METHODS
        #endregion

        #region PRIVATE METHODS
        internal virtual void Eat()
        {
            OnEaten?.Invoke();
            SoundManager.Instance.SetSFX(data.Sfx);
            SoundManager.Instance.PlaySFX();
            this.gameObject.SetActive(false);
        }
        #endregion

    }
}