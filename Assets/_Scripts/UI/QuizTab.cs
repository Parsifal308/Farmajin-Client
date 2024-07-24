using System;
using Farmanji.Data;
using Farmanji.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Farmanji.Game
{
    public class QuizTab : BaseTab
    {
        #region FIELDS
        [SerializeField] private int quizIndex = 0;
        [SerializeField] private ActivityData _currentActivity;
        [SerializeField] private QuizPanel _quizPanel;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODs
        public override void Open()
        {
            base.Open();
            StartQuiz(); //Descomentar esto cuando este listo el sistema
        }

        private void Start()
        {
            if (!_quizPanel) _quizPanel = GetComponentInChildren<QuizPanel>(_quizPanel);
        }

        private void StartQuiz()
        {
            _currentActivity = ResourcesLoaderManager.Instance.Worlds.GetCurrentActivity;


            if (_currentActivity == null || _currentActivity.Questions == null ||
                _currentActivity.Questions.Length <= 1) return;

            var question = _currentActivity.AssociatedQuiz.questions[quizIndex];
            quizIndex++;
            //var question = _currentActivity.AssociatedQuiz.questions[Random.Range(0, _currentActivity.AssociatedQuiz.questions.Length - 1)];
            if (_quizPanel) _quizPanel.StartQuiz(question);
        }
        #endregion
    }
}