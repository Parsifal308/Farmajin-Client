using Farmanji.Data;
using Farmanji.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class CustomizationButton : MonoBehaviour
    {
        [Header("Button Settings")]
        [SerializeField] private Sprite availableSprite;
        [SerializeField] private Sprite unavailableSprite;
        [SerializeField] protected bool UseCustomPivot;
        [Header("Preview Settings")]
        [SerializeField] protected Image previewImage;
        
        private bool _startAvailable = true;
        private AvatarPiece _avatarPiece;

        protected ItemData _itemData;
        
        private float _previewImageWidth;
        private float _previewImageHeight;
        
        private Button button;
        protected AvatarManager _avatarManager;
        
        private Sprite _previewSprite;
        protected Sprite _mainSprite;
        protected Sprite _secondarySprite;
        protected Sprite _terciarySprite;

        public void InitializeCustomizationButton(ItemData itemData, AvatarManager avatarManager)
        {
            _itemData = itemData;
            _previewSprite = itemData.PreviewSprite;
            _mainSprite = itemData.MainSprite;
            _secondarySprite = itemData.SecondarySprite;
            _terciarySprite = itemData.TerciarySprite;
            
            _startAvailable = itemData.Obtained;
            _avatarPiece = itemData.AvatarPiece;

            _avatarManager = avatarManager;
            
            _previewImageWidth = 280f;
            _previewImageHeight = 280f;
        }

        private void Start()
        {
            if (button == null) button = GetComponent<Button>();
            if (button != null) button.onClick.AddListener(SetAvatarPiece);
            if (previewImage == null) previewImage.GetComponentInChildren<Image>();
            
            SetPreviewPiece();
            
            if (_startAvailable) UnlockCustomizationButton();
            else LockCustomizationButton();
        }
        protected virtual void SetAvatarPiece()
        {
            if (!_avatarManager || !_mainSprite) return;
            _avatarManager.SetAvatarPiece(_itemData, _avatarPiece, _mainSprite);
        }

        private void SetPreviewPiece()
        {
            if (!previewImage) return;
            previewImage.sprite = _previewSprite;
            previewImage.rectTransform.sizeDelta = new Vector2(_previewImageWidth, _previewImageHeight);
        }

        protected virtual void UnlockCustomizationButton()
        {
            if (_startAvailable || !button|| !availableSprite|| !previewImage) return;
            _startAvailable = true;
            button.enabled = true;
            button.image.sprite = availableSprite;
            previewImage.enabled = true;
        }

        protected virtual void LockCustomizationButton()
        {
            if (!_startAvailable || !button || !unavailableSprite || !previewImage) return;
            _startAvailable = false;
            button.enabled = false;
            button.image.sprite = unavailableSprite;
            previewImage.enabled = false;
        }
    }
}