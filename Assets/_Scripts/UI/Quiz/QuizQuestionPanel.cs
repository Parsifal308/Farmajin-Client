using Farmanji.Data;
using TMPro;
using UnityEngine;

namespace Farmanji.Game
{
    public class QuizQuestionPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _questionText;

        private void Start()
        {
            GetComponentInParent<QuizPanel>().OnStartQuiz += SetQuestionText;
        }

        private void SetQuestionText(Questions question)
        {
            _questionText.SetText(question.question);
        }
    }
}