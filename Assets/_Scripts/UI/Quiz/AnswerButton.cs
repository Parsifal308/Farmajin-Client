using System;
using Farmanji.Data;
using Farmanji.Pacman;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class AnswerButton : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private TextMeshProUGUI _answerText;
        [SerializeField] private int _answerIndex;
        [SerializeField] private bool _isCorrect;
        [SerializeField] private string _answerID;
        [SerializeField] private event EventHandler OnQuizAnswered;
        #endregion

        #region PROPERTIES
        public EventHandler OnQuizAnsweredEvent { get { return OnQuizAnswered; } set { OnQuizAnswered = value; } }
        public bool IsCorrect { get { return _isCorrect; } }

        #endregion

        #region UNITY MEHTODS
        void Awake()
        {
        }
        private void Start()
        {
            SubscribeEvents();
            GetComponent<Button>().onClick.AddListener(AnswerButtonPressed);
        }
        private void OnDestroy()
        {
            UnsubscribeEvents();
        }
        #endregion

        #region PUBLIC METHODS
        public void SetAnswerText(Questions question)
        {
            _answerText.SetText(question.responses[_answerIndex].response);
            _isCorrect = question.responses[_answerIndex].isCorrect;
            _answerID = question.responses[_answerIndex].id;
        }
        #endregion

        #region PRIVATE METHODS
        private void AnswerButtonPressed()
        {
            OnQuizAnsweredEvent?.Invoke(this, EventArgs.Empty);
            PacmanGameManager.Instance.QuizCanvas.GetComponent<QuizTab>().Close();
        }
        private void SubscribeEvents()
        {
            OnQuizAnsweredEvent += PacmanGameManager.Instance.FinishQuiz;
        }
        private void UnsubscribeEvents()
        {
            OnQuizAnsweredEvent -= PacmanGameManager.Instance.FinishQuiz;
        }
        #endregion
    }
}