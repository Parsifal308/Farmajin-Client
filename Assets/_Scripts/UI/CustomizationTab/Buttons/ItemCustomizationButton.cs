using System;
using Farmanji.Data;
using UnityEngine;

namespace Farmanji.Game
{
    public class ItemCustomizationButton : CustomizationButton
    {
        private AccesoryType _accesoryType;
        [SerializeField] private Vector2 _customPivot;
        
        public void SetItemButton(AccesoryType accesoryType)
        {
            _accesoryType = accesoryType;
        }
        protected override void SetAvatarPiece()
        {
            if (!_avatarManager || !_mainSprite) return;
            var DefaultPivot = _mainSprite.pivot;
            if (UseCustomPivot)
            {
                _mainSprite = Sprite.Create(_mainSprite.texture, _mainSprite.rect, _customPivot);
            }
            switch (_accesoryType)
            {
                case AccesoryType.Hat:
                    _avatarManager.SetAvatarPiece(_itemData, AvatarPiece.Hat, _mainSprite);
                    break;
                case AccesoryType.FrontHand:
                    _avatarManager.SetAvatarPiece(_itemData, AvatarPiece.FrontHandItem, _mainSprite);
                    break;
                case AccesoryType.BackHand:
                    _avatarManager.SetAvatarPiece(_itemData, AvatarPiece.BackHandItem, _mainSprite);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}