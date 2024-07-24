using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class SystemsTab : MonoBehaviour
    {
        [Header("Components:")]
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI _errorMessage;
        private ErrorType _errorType;
        
        public SystemsTab SetErrorType(ErrorType errorType)
        {
            button.onClick.RemoveAllListeners();
            _errorType = errorType;
            switch (_errorType)
            {
                case ErrorType.Login:
                    _errorMessage.SetText("Si estas viendo esto, fallo el login xd, atte. Parsi");
                    button.onClick.AddListener(BackToLogin);
                    break;
                case ErrorType.Chat:
                    _errorMessage.SetText("Si estas viendo esto, se rompio el chat xd, atte. Parsi");
                    break;
                case ErrorType.Game:
                    _errorMessage.SetText("Si estas viendo esto, se rompio el minijuego xd, atte. Parsi");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            button.onClick.AddListener(DisableTab);
            return this;
        }

        private void DisableTab()
        {
            gameObject.SetActive(false);
        }

        private void BackToLogin()
        {
            SceneManager.LoadSceneAsync("Login");
        }
        
    }
}