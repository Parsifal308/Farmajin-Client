namespace Farmanji.Game
{
    public class PantsCustomizationButton : CustomizationButton
    {
        protected override void SetAvatarPiece()
        {
            base.SetAvatarPiece();
            if (_secondarySprite != null) _avatarManager.SetAvatarPiece(_itemData, AvatarPiece.BackLeg, _secondarySprite);
        }
    }
}