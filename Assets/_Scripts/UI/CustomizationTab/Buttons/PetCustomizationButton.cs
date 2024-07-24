namespace Farmanji.Game
{
    public class PetCustomizationButton : CustomizationButton
    {
        protected override void SetAvatarPiece()
        {
            if (!_avatarManager || !_secondarySprite) return;
            _avatarManager.SetAvatarPiece(_itemData, AvatarPiece.PetHead, _mainSprite);
            _avatarManager.SetAvatarPiece(_itemData, AvatarPiece.PetBody, _secondarySprite);
        }
    }
}