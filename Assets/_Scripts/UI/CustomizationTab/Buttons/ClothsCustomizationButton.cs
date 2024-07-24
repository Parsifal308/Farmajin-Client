namespace Farmanji.Game
{
    public class ClothsCustomizationButton : CustomizationButton
    {
        protected override void SetAvatarPiece()
        {
            base.SetAvatarPiece();
            if (_secondarySprite != null) _avatarManager.SetAvatarPiece(_itemData, AvatarPiece.FrontHand, _secondarySprite);
            if (_terciarySprite != null) _avatarManager.SetAvatarPiece(_itemData, AvatarPiece.BackHand, _terciarySprite);
        }
    }
}