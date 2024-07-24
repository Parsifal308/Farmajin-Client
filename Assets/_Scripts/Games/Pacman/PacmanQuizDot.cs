using System.Collections;
using System.Collections.Generic;
using Farmanji.Game;
using UnityEngine;
using UnityEngine.Events;

namespace Farmanji.Pacman
{
    public class PacmanQuizDot : PacmanDot
    {
        #region FIELDS
        [SerializeField] private UnityEvent OnQuizEaten;
        private UnityAction onQuizEatenAction;
        #endregion

        #region UNITY MEHTODS
        protected override void Awake()
        {
            base.Awake();
            SubscribeEvents();
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion


        #region PRIVATE METHODS
        internal override void Eat()
        {
            Debug.Log("QUIZ DOT EATED");
            OnQuizEaten?.Invoke();
            base.Eat();
        }
        private void SubscribeEvents()
        {
            onQuizEatenAction = () =>
            {
                PacmanGameManager.Instance.QuizCanvas.GetComponent<QuizTab>().Open();
                PacmanGameManager.Instance.PauseGame();
            };
            OnQuizEaten.AddListener(onQuizEatenAction);
        }
        private void UnsubscribeEvents()
        {
            OnQuizEaten.RemoveListener(onQuizEatenAction);
        }
        #endregion
    }
}