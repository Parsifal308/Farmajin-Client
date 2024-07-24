using System.Collections;
using Farmanji.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class DoubleClickButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] public float _doubleClickThreshold = 0.3f;

        private float _firstClickTime;
        private bool _doubleClickAllowed;
        [SerializeField] private int _clickNum = 0;

        private void Start()
        {
            //Register events
            _button.onClick.AddListener(CheckDoubleClick);
        }

        private void CheckDoubleClick()
        {
            if (_doubleClickAllowed && _clickNum == 1)
            {
                VideoManager.Instance.ToogleFullScreen();
                _doubleClickAllowed = false;
                _clickNum = 0;
                StopAllCoroutines();
            }
            else
            {
                StopAllCoroutines();
                _firstClickTime = Time.time;
                _clickNum = 1;
                StartCoroutine(DetectDoubleClick());
            }
        }

        private IEnumerator DetectDoubleClick()
        {
            _doubleClickAllowed = true;
            while (Time.time < _firstClickTime + _doubleClickThreshold)
            {
                yield return new WaitForEndOfFrame();
            }
            _doubleClickAllowed = false;
            _clickNum = 0;
            VideoManager.Instance.ShowFullScreenButtons();
        }
    }
}