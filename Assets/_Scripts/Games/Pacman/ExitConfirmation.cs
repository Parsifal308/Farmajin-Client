using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Farmanji.Pacman
{
    public class ExitConfirmation : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private TextMeshProUGUI msg;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button continueButton;
        private CanvasGroup canvasGroup;
        #endregion

        #region PROPERTIES
        #endregion

        #region UNITY METHODS
        void Start ()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            continueButton.onClick.AddListener(Close);
        }
        #endregion

        #region PRIVATE METHODS

        #endregion

        #region PUBLIC METHODS
        public void Close()
        {
            PacmanGameManager.Instance.ContinueGame();
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
        }
        public void Open()
        {
            PacmanGameManager.Instance.PauseGame();
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }
        #endregion
    }
}