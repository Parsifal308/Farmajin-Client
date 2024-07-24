using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Farmanji.Managers;

namespace Farmanji.Utilities
{
    public class SettingsMenu : MonoBehaviour
    {
        [Header("Button References")]
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _muteMusicButton;
        [FormerlySerializedAs("_muteFXButton")] [SerializeField] private Button _muteSFXButton;
        [SerializeField] private Button _outPanel;

        [Header("Menu Settings")] 
        [SerializeField] private Vector2 _spacing;
        [SerializeField] private float _expandDuration = 0.7f;
        [SerializeField] private float _collapseDuration = 0.3f;

        [Header("Sound Settings")] 
        [SerializeField] private Sprite _mutedButtonSprite;
        [SerializeField] private Sprite _unMutedButtonSprite;
        
        private Button[] _menuButtons;
        private bool isExpanded;

        private Vector2 _settingsButtonPosition;
        private int _itemsCount;

        private void Start()
        {
            _itemsCount = transform.childCount - 1;
            _menuButtons = new Button[_itemsCount];
            
            for (var i = 0; i < _itemsCount; i++)
            {
                _menuButtons[i] = transform.GetChild(i + 1).GetComponent<Button>();
            }
            _settingsButton = transform.GetChild(0).GetComponent<Button>();
            _settingsButton.transform.SetAsLastSibling();
            _outPanel.onClick.AddListener(HideSettings);

            FirstBindForButtons();
            
            _settingsButtonPosition = _settingsButton.GetComponent<RectTransform>().position;

            ResetButtonsPositions();
        }

        private void FirstBindForButtons()
        {
            _settingsButton.onClick.AddListener(ShowSettings);
            _muteMusicButton.onClick.AddListener(SoundManager.Instance.MuteMusic);
            SoundManager.Instance._OnMusicMuted += MuteMusicButtonPressed;
            SoundManager.Instance._OnMusicUnMuted += UnMuteMusicButtonPressed;
            _muteSFXButton.onClick.AddListener(MuteSFXButtonPressed);
        }

        private void ShowSettings()
        {
            if (_settingsButton == null || isExpanded) return;
            _settingsButton.onClick.RemoveListener(ShowSettings);
            _settingsButton.onClick.AddListener(HideSettings);
            isExpanded = true;
            _outPanel.enabled = true;
            _outPanel.image.raycastTarget = true;
            ShowMenu(isExpanded);
        }

        private void HideSettings()
        {
            if (_settingsButton == null || !isExpanded) return;
            _settingsButton.onClick.RemoveListener(HideSettings);
            _settingsButton.onClick.AddListener(ShowSettings);
            isExpanded = false;
            _outPanel.enabled = false;
            _outPanel.image.raycastTarget = false;
            ShowMenu(isExpanded);
        }

        private void ResetButtonsPositions()
        {
            if (_menuButtons.Length <= 0) return;
            foreach (var menuButton in _menuButtons)
            {
                menuButton.transform.position = _settingsButtonPosition;
            }
        }

        private void ShowMenu(bool show)
        {
            StartCoroutine(ShowMenuAnimation(show));
        }

        private IEnumerator ShowMenuAnimation(bool show)
        {
            if (show)
            {
                for (var i = 0; i < _itemsCount; i++)
                {
                    Vector3 finalPos = _settingsButtonPosition + _spacing * (i + 1);
                    while (Vector3.Distance(_menuButtons[i].GetComponent<RectTransform>().position, finalPos) > 0.05f)
                    {
                        _menuButtons[i].GetComponent<RectTransform>().position = Vector3.Lerp(_menuButtons[i].transform.position,
                            _settingsButtonPosition + _spacing * (i + 1), _expandDuration);
                        yield return null;
                    }
                    _menuButtons[i].GetComponent<RectTransform>().position = finalPos;
                }
            }
            else
            {
                for (var i = 0; i < _itemsCount; i++)
                {
                    while (Vector3.Distance(_menuButtons[i].transform.position, _settingsButtonPosition) > 0.05f)
                    {
                        _menuButtons[i].GetComponent<RectTransform>().position = Vector3.Lerp(_menuButtons[i].transform.position,
                            _settingsButtonPosition, _collapseDuration);
                        yield return null;
                    }
                    _menuButtons[i].GetComponent<RectTransform>().position = _settingsButtonPosition;
                }
            }
        }

        private void MuteMusicButtonPressed()
        {
            if (!_muteMusicButton) return;
            _muteMusicButton.onClick.RemoveListener(SoundManager.Instance.MuteMusic);
            _muteMusicButton.onClick.AddListener(SoundManager.Instance.UnMuteMusic);
            _muteMusicButton.image.sprite = _mutedButtonSprite;
        }
        
        private void UnMuteMusicButtonPressed()
        {
            if (!_muteMusicButton) return;
            _muteMusicButton.onClick.RemoveListener(SoundManager.Instance.UnMuteMusic);
            _muteMusicButton.onClick.AddListener(SoundManager.Instance.MuteMusic);
            _muteMusicButton.image.sprite = _unMutedButtonSprite;
        }

        private void MuteSFXButtonPressed()
        {
            if (_muteSFXButton == null) return;
            _muteSFXButton.onClick.RemoveListener(MuteSFXButtonPressed);
            _muteSFXButton.onClick.AddListener(UnMuteSFXButtonPressed);
            //Sprite Change
        }
        
        private void UnMuteSFXButtonPressed()
        {
            if (_muteSFXButton == null) return;
            _muteSFXButton.onClick.RemoveListener(UnMuteSFXButtonPressed);
            _muteSFXButton.onClick.AddListener(MuteSFXButtonPressed);
            //Sprite Change
        }
    }
}