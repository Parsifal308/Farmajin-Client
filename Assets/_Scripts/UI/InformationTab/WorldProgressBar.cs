using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.UI
{
    public class WorldProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _progressBarImage;
        [SerializeField] private TextMeshProUGUI _progressBarText;
        [SerializeField] private float _fillTime = 0.1f;
        [SerializeField] private float _maxProgress = 100;

        private float _progress;

        private void OnEnable()
        {
            StartCoroutine(FillAnimation(_progress));
        }

        public void SetWorldProgress(float progress)
        {
            // World Progress has to be updated the first frame in Home Scene and every time we complete an activity or change the world
            _progress = progress;
            if (gameObject.activeInHierarchy) StartCoroutine(FillAnimation(progress));
        }

        private IEnumerator FillAnimation(float progress)
        {
            progress = Mathf.Clamp(progress,0, 100);
            _maxProgress = 100;
        
            float elapsedTime = 0;
            while (elapsedTime < _fillTime)
            {
                elapsedTime += Time.deltaTime;
                _progressBarImage.fillAmount = Mathf.Lerp(_progressBarImage.fillAmount, progress / _maxProgress, elapsedTime / _fillTime);
                var progressBarText = Mathf.RoundToInt(_progressBarImage.fillAmount * 100);
                _progressBarText.SetText(progressBarText + "% Completado");
                yield return null;
            }
        }
    }
}