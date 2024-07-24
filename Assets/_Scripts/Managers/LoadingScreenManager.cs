using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Manejo de la UI de la Loading Screen

namespace Farmanji.Managers
{
    public class LoadingScreenManager : MonoBehaviour
    {
        //REFERENCES

        [SerializeField] private Image _fillerBar;
        [SerializeField] private TextMeshProUGUI _labelText;

        private float _targetProgress;

        #region UNITY METHODS
        void OnEnable()
        {
            ScenesManager.Instance.OnLoadingTick += Tick;

            UpdateLabel("Loading...");
            _fillerBar.fillAmount = 0;
        }

        private void Start()
        {
            if (ResourcesLoaderManager.Instance != null) ResourcesLoaderManager.Instance.OnLoadingAll += LoadingResources;
        }

        private void OnDisable()
        {
            ScenesManager.Instance.OnLoadingTick -= Tick;
            ResourcesLoaderManager.Instance.OnLoadingAll -= LoadingResources;
        }

        private void Update()
        {
            float currentAmount = _fillerBar.fillAmount;
            _fillerBar.fillAmount = Mathf.MoveTowards(currentAmount, _targetProgress, Time.deltaTime * 2f);
        }
        #endregion

        #region PUBLIC METHODS
        public void Tick(float progress)
        {
            _targetProgress = progress;

        }

        private void LoadingResources()
        {
            UpdateLabel("Loading Resources...");
        }

        private void UpdateLabel(string newText)
        {
            _labelText.text = newText;
        }
        #endregion


    }

}
