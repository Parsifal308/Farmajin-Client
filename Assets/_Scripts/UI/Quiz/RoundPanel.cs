using Farmanji.Data;
using TMPro;
using UnityEngine;

namespace Farmanji.Game
{
    public class RoundPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _roundText;

        private void Start()
        {
            GetComponentInParent<QuizPanel>().OnStartQuiz += SetRoundText;
        }

        private void SetRoundText(Questions question)
        {
            _roundText.SetText("Round 1"); //Change this to round
        }
    } 
}