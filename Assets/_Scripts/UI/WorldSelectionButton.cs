using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class WorldSelectionButton : MonoBehaviour
    {
        #region UNITY METHODS
        CanvasGroup informationBlurredPanel;
        void Start()
        {
            informationBlurredPanel = TabsManager.Instance.Home.GetComponent<HomeTab>().InformationBlurredPanel;
            UnityAction closeAction = () => { CloseMenu(); };
            GetComponent<Button>().onClick.AddListener(closeAction);
        }
        #endregion

        #region METHODS
        public void CloseMenu()
        {
            GetComponentInParent<Animator>().SetTrigger("Hide");
            informationBlurredPanel.interactable = false;
            informationBlurredPanel.blocksRaycasts = false;
        }
        #endregion
    }
}