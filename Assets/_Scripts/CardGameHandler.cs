using Farmanji.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.UI
{
    public class CardGameHandler : MonoBehaviour
    {
        [SerializeField] private Button _playBtn;
        [SerializeField] private string _sceneName;

        private void Start()
        {
            _playBtn.onClick.AddListener(PlayButton);
        }

        private void PlayButton()
        {
            ScenesManager.Instance.LoadGame(_sceneName);
        }
    }
}

