using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Farmanji.UI
{
    public class WelcomeTab : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private Slider slider;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button backButton;
        [SerializeField] private Button startButton;
        [SerializeField] private Image panelBackground1;
        [SerializeField] private Image panelBackground2;
        [SerializeField] private int currentInstructionIndex = 0;
        [SerializeField] private List<CanvasGroup> tabs;

        #endregion

        #region UNITY METHODs
        private void Start()
        {

            if (PlayerPrefs.GetInt("FirstTimeOpening", 1) == 1)
            {
                Debug.Log("First Time Opening");
                InitializeWelcomeTab();
                PlayerPrefs.SetInt("FirstTimeOpening", 0);
            }
        }
        #endregion

        #region METHODS
        private void InitializeWelcomeTab()
        {
            GetComponent<CanvasGroup>().interactable = true;
            GetComponent<CanvasGroup>().alpha = 1;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            GetComponent<Animator>().SetTrigger("In");
            for (int i = 0; i < tabs.Count; i++)
            {
                tabs[i].GetComponent<CanvasGroup>().alpha = 0f;

            }
            tabs[0].GetComponent<Animator>().SetTrigger("In");

            UnityAction nextButtonAction = () =>
            {
                SetNextTabACtion();
            };
            nextButton.onClick.AddListener(nextButtonAction);
            startButton.onClick.AddListener(LastTabButton);
        }
        private void SetNextTabACtion()
        {

            Debug.Log("UNITY ACTION");
            if (currentInstructionIndex < tabs.Count - 1)
                tabs[currentInstructionIndex].GetComponent<Animator>().SetTrigger("Out");
            if (currentInstructionIndex + 1 <= tabs.Count - 1)
                tabs[currentInstructionIndex + 1].GetComponent<Animator>().SetTrigger("In");
            currentInstructionIndex++;
            if (currentInstructionIndex >= tabs.Count - 1)
            {
                panelBackground1.gameObject.SetActive(false);
                panelBackground2.gameObject.SetActive(true);
                startButton.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(false);
                slider.value = 1;
            }
            else
            {
                nextButton.interactable = false;
                nextButton.onClick.RemoveAllListeners();
                StartCoroutine(RefreshListener());
            }
            slider.value = currentInstructionIndex;

            //nextButton.onClick.AddListener(nextButtonAction);
            /*.onClick.RemoveAllListeners();
            nextButton.interactable = false;*/

        }

        IEnumerator RefreshListener()
        {
            UnityAction nextButtonAction = () =>
            {
                SetNextTabACtion();
            };
            nextButton.onClick.AddListener(nextButtonAction);
            yield return new WaitForSeconds(2f);
            nextButton.interactable = true;
        }
        private void LastTabButton()
        {
            GetComponent<Animator>().SetTrigger("Out");
        }
        #endregion

        #region COROUTINES
        #endregion
    }
}