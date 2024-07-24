using UnityEngine;

namespace Farmanji.Game
{
    public class BannerCustomizationButton : CustomizationButton
    {
        [SerializeField] private Sprite lockedPreviewSprite;

        protected override void SetAvatarPiece()
        {
            if (!_avatarManager) return;
            //_customizationSystem.SetAvatarBackground(_spriteToSet);
        }

        protected override void UnlockCustomizationButton()
        {
            base.UnlockCustomizationButton();
            previewImage.sprite = _mainSprite;
        }

        protected override void LockCustomizationButton()
        {
            base.LockCustomizationButton();
            previewImage.sprite = lockedPreviewSprite;
            previewImage.enabled = true;
        }
    }
}