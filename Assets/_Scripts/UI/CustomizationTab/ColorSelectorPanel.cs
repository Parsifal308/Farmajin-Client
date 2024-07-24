using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class ColorSelectorPanel : MonoBehaviour
    {
        [Header("Expanded Selections")]
        [SerializeField] private GameObject _faceColorExpandSelection;
        [SerializeField] private GameObject _hairColorExpandSelection;
        
        [Header("Selector Buttons")]
        [SerializeField] private Button _faceColorReturnButton;
        [SerializeField] private Button _hairColorReturnButton;
        [SerializeField] private Sprite _selectButtonSprite;
        [SerializeField] private Sprite _returnButtonSprite;

        private ExpandibleWindow _faceColorExpandibleWindow;
        private ExpandibleWindow _hairColorExpandibleWindow;

        private void Start()
        {
            if (_faceColorReturnButton != null) _faceColorReturnButton.onClick.AddListener(FaceColorSelectButtonPressed);
            if (_hairColorReturnButton != null) _hairColorReturnButton.onClick.AddListener(HairColorSelectButtonPressed);
            if (_faceColorExpandSelection != null) _faceColorExpandibleWindow = _faceColorExpandSelection.GetComponent<ExpandibleWindow>();
            if (_hairColorExpandSelection != null) _hairColorExpandibleWindow = _hairColorExpandSelection.GetComponent<ExpandibleWindow>();
            if (_faceColorExpandibleWindow != null) _faceColorExpandibleWindow.OnExpandAnimationFinished += FaceExpandAnimationFinished;
            if (_faceColorExpandibleWindow != null) _faceColorExpandibleWindow.OnContractAnimationFinished += FaceContractAnimationFinished;
            if (_hairColorExpandibleWindow != null) _hairColorExpandibleWindow.OnExpandAnimationFinished += HairExpandAnimationFinished;
            if (_hairColorExpandibleWindow != null) _hairColorExpandibleWindow.OnContractAnimationFinished += HairContractAnimationFinished;
        }

        private void FaceColorSelectButtonPressed()
        {
            if (!_faceColorExpandibleWindow || !_faceColorReturnButton) return;
            
            _faceColorReturnButton.onClick.RemoveListener(FaceColorSelectButtonPressed);
            if (_faceColorReturnButton.image && _returnButtonSprite) _faceColorReturnButton.image.sprite = _returnButtonSprite;
            _faceColorExpandibleWindow.ExpandWindow(true);
        }

        private void FaceExpandAnimationFinished()
        {
            if (!_faceColorReturnButton) return;
            
            _faceColorReturnButton.onClick.AddListener(FaceColorReturnButtonPressed);
        }

        private void FaceColorReturnButtonPressed()
        {
            if (!_faceColorExpandibleWindow || !_faceColorReturnButton) return;
            
            _faceColorReturnButton.onClick.RemoveListener(FaceColorReturnButtonPressed);
            if (_faceColorReturnButton.image && _selectButtonSprite) _faceColorReturnButton.image.sprite = _selectButtonSprite;
            _faceColorExpandibleWindow.ExpandWindow(false);
        }
        
        private void FaceContractAnimationFinished()
        {
            if (!_faceColorReturnButton) return;
            _faceColorReturnButton.onClick.AddListener(FaceColorSelectButtonPressed);
        }
        
        private void HairColorSelectButtonPressed()
        {
            if (!_hairColorExpandibleWindow || !_hairColorReturnButton) return;
            
            _hairColorReturnButton.onClick.RemoveListener(HairColorSelectButtonPressed);
            if (_hairColorReturnButton.image && _returnButtonSprite) _hairColorReturnButton.image.sprite = _returnButtonSprite;
            _hairColorExpandibleWindow.ExpandWindow(true);
        }
        
        private void HairExpandAnimationFinished()
        {
            if (!_hairColorReturnButton) return;
            _hairColorReturnButton.onClick.AddListener(HairColorReturnButtonPressed);
        }

        private void HairColorReturnButtonPressed()
        {
            if (!_hairColorExpandibleWindow || !_hairColorReturnButton) return;
            _hairColorReturnButton.onClick.RemoveListener(HairColorReturnButtonPressed);
            if (_hairColorReturnButton.image && _selectButtonSprite) _hairColorReturnButton.image.sprite = _selectButtonSprite;
            _hairColorExpandibleWindow.ExpandWindow(false);
        }
        
        private void HairContractAnimationFinished()
        {
            if (!_hairColorReturnButton) return;
            _hairColorReturnButton.onClick.AddListener(HairColorSelectButtonPressed);
        }

        public void EnableFaceColorWindow(bool SetEnabled)
        {
            if (!_faceColorExpandibleWindow) return;
            _faceColorExpandSelection.SetActive(SetEnabled);
        }
        
        public void EnableHairColorWindow(bool SetEnabled)
        {
            if (!_hairColorExpandibleWindow) return;
            _hairColorExpandSelection.SetActive(SetEnabled);
        }
        
        public GameObject GetFaceColorWindow
        {
            get { return _faceColorExpandSelection; }
        }
        
        public GameObject GetHairColorWindow
        {
            get { return _hairColorExpandSelection; }
        }
    }
}