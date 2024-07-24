using Farmanji.Data;
using UnityEngine;

namespace Farmanji.Game
{
    public class AnswerPanel : MonoBehaviour
    {
        [SerializeField] private AnswerButton _answer1Button;
        [SerializeField] private AnswerButton _answer2Button;
        [SerializeField] private AnswerButton _answer3Button;
        [SerializeField] private AnswerButton _answer4Button;

        private void Start()
        {
            GetComponentInParent<QuizPanel>().OnStartQuiz += SetAnswerTexts;
        }

        private void SetAnswerTexts(Questions question)
        {
            _answer1Button.SetAnswerText(question);
            _answer2Button.SetAnswerText(question);
            _answer3Button.SetAnswerText(question);
            _answer4Button.SetAnswerText(question);
        }
    }
}