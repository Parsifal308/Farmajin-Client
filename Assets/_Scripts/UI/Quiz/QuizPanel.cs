using System;
using Farmanji.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class QuizPanel : MonoBehaviour
    {
        [SerializeField] private RoundPanel _roundPanel;
        [SerializeField] private QuizQuestionPanel _questionPanel;
        [SerializeField] private AnswerPanel _answerPanel;
        [SerializeField] private Button _nextButton;
        
        public event Action<Questions> OnStartQuiz;

        private void Start()
        {
            _nextButton.onClick.AddListener(NextButtonPressed);
        }

        public void StartQuiz(Questions question)
        {
            OnStartQuiz?.Invoke(question);
        }

        private void NextButtonPressed()
        {
            //TODO show next question?
        }
    }
}