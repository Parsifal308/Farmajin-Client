using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.UI
{
    public class PopUp : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        [SerializeField] private Button _closeButton;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        protected virtual void Start()
        {
            _closeButton.onClick.AddListener(Close);
        }

        public virtual void Open()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public virtual void Close()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}

