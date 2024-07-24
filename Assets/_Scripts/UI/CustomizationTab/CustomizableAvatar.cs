using System;
using Farmanji.Data;
using UnityEngine;

namespace Farmanji.Game
{
    public class CustomizableAvatar : MonoBehaviour
    {
        [Header("Head References")]
        [SerializeField] private SpriteRenderer avatarHeadRenderer;
        [SerializeField] private SpriteRenderer avatarFaceRenderer;
        [SerializeField] private SpriteRenderer avatarBeardRenderer;
        [SerializeField] private SpriteRenderer avatarHairRenderer;
        [SerializeField] private SpriteRenderer avatarHatRenderer;
        
        [Header("Body References")]
        [SerializeField] private SpriteRenderer avatarBodyRenderer;
        
        [Header("Hand References")]
        [SerializeField] private SpriteRenderer avatarFrontHandRenderer;
        [SerializeField] private SpriteRenderer avatarBackHandRenderer;
        
        [Header("Legs References")]
        [SerializeField] private SpriteRenderer avatarFrontLegRenderer; 
        [SerializeField] private SpriteRenderer avatarBackLegRenderer; 
        
        [Header("Items References")]
        [SerializeField] private SpriteRenderer avatarFrontItemRenderer;
        [SerializeField] private SpriteRenderer avatarBackItemRenderer;
        
        [Header("Pet References")]
        [SerializeField] private SpriteRenderer avatarPetHeadRenderer;
        [SerializeField] private SpriteRenderer avatarPetBodyRenderer;
        
        [Header("Background References")]
        [SerializeField] private SpriteRenderer avatarBackgroundRenderer;
        
        [Header("Current Customization")]
        [SerializeField] private UserCustomization _currentCustomization;
        public UserCustomization GetCurrentCustomization{ get { return _currentCustomization; }}

        public void SetPiece(ItemData itemData, AvatarPiece avatarPiece, Sprite spriteToSet) //Method called on CustomButton to change an avatar piece
        {
            switch (avatarPiece)
            {
                case AvatarPiece.Face:
                    _currentCustomization.Face = itemData;
                    SetAvatarFace(spriteToSet);
                    break;
                case AvatarPiece.Hair:
                    _currentCustomization.Hair = itemData;
                    SetAvatarHair(spriteToSet);
                    break;
                case AvatarPiece.Cloths:
                    _currentCustomization.Trunk = itemData;
                    SetAvatarCloths(spriteToSet);
                    break;
                case AvatarPiece.FrontHand:
                    SetAvatarFrontHand(spriteToSet);
                    break;
                case AvatarPiece.BackHand:
                    SetAvatarBackHand(spriteToSet);
                    break;
                case AvatarPiece.Pants:
                    _currentCustomization.Legs = itemData;
                    SetAvatarPants(spriteToSet);
                    break;
                case AvatarPiece.BackLeg:
                    SetAvatarBackLeg(spriteToSet);
                    break;
                case AvatarPiece.FrontHandItem:
                    SetAvatarFrontItem(spriteToSet);
                    break;
                case AvatarPiece.BackHandItem:
                    SetAvatarBackItem(spriteToSet);
                    break;
                case AvatarPiece.Hat:
                    _currentCustomization.Hat = itemData;
                    SetAvatarHat(spriteToSet);
                    break;
                case AvatarPiece.Background:
                    SetAvatarBackground(spriteToSet);
                    break;
                case AvatarPiece.PetHead:
                    _currentCustomization.Pet = itemData;
                    SetAvatarPetHead(spriteToSet);
                    break;
                case AvatarPiece.PetBody:
                    SetAvatarPetBody(spriteToSet);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(avatarPiece), avatarPiece, null);
            }
        }
        
        private void SetAvatarFace(Sprite sprite) // Setting Avatar Face Sprite
        {
            if (!sprite || !avatarFaceRenderer) return;
            avatarFaceRenderer.sprite = sprite;
        }
        
        private void SetAvatarHair(Sprite sprite) // Setting Avatar Hair Sprite
        {
            if (!sprite || !avatarHairRenderer) return;
            avatarHairRenderer.sprite = sprite;
        }
        
        private void SetAvatarCloths(Sprite sprite) // Setting Avatar Cloths Sprite
        {
            if (!sprite || !avatarBodyRenderer) return;
            avatarBodyRenderer.sprite = sprite;
        }

        private void SetAvatarFrontHand(Sprite sprite) // Setting Avatar Front Hand Sprite
        {
            if (!sprite || !avatarFrontHandRenderer) return;
            avatarFrontHandRenderer.sprite = sprite;
        }
        
        private void SetAvatarBackHand(Sprite sprite) // Setting Avatar Back Hand Sprite
        {
            if (!sprite || !avatarBackHandRenderer) return;
            avatarBackHandRenderer.sprite = sprite;
        }
        
        private void SetAvatarPants(Sprite sprite) // Setting Avatar Pants Sprite
        {
            if (!sprite || !avatarFrontLegRenderer) return;
            avatarFrontLegRenderer.sprite = sprite;
        }
        
        private void SetAvatarBackLeg(Sprite sprite) // Setting Avatar Back Hand Sprite
        {
            if (!sprite || !avatarBackLegRenderer) return;
            avatarBackLegRenderer.sprite = sprite;
        }

        private void SetAvatarFrontItem(Sprite sprite)
        {
            if (!sprite || !avatarFrontItemRenderer) return;
            avatarFrontItemRenderer.sprite = sprite;
        }
        
        private void SetAvatarBackItem(Sprite sprite)
        {
            if (!sprite || !avatarBackItemRenderer) return;
            avatarBackItemRenderer.sprite = sprite;
        }
        
        private void SetAvatarHat(Sprite sprite)
        {
            if (!sprite || !avatarHatRenderer) return;
            avatarHatRenderer.sprite = sprite;
        }
        
        private void SetAvatarPetHead(Sprite sprite) 
        {
            if (!sprite|| !avatarPetHeadRenderer) return;
            avatarPetHeadRenderer.sprite = sprite;
        }
        
        private void SetAvatarPetBody(Sprite sprite)
        {
            if (!sprite || !avatarPetBodyRenderer) return;
            avatarPetBodyRenderer.sprite = sprite;
        }
        
        private void SetAvatarBackground(Sprite sprite) //Setting Avatar Background Sprite
        {
            if (!sprite || !avatarBackgroundRenderer) return;
            avatarBackgroundRenderer.sprite = sprite;
        }
        
        public void TintPiece(AvatarPiece pieceToTint, Color tintColor)
        {
            switch (pieceToTint)
            {
                case AvatarPiece.Face:
                    avatarHeadRenderer.color = tintColor;
                    break;
                case AvatarPiece.Hair:
                    avatarHairRenderer.material.color = tintColor;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(pieceToTint), pieceToTint, null);
            }
        }
        // public Sprite GetCurrentCustomization(AvatarPiece piece)
        // {
        //     return piece switch
        //     {
        //         AvatarPiece.Face => avatarFaceRenderer.sprite,
        //         AvatarPiece.Hair => avatarHairRenderer.sprite,
        //         AvatarPiece.Cloths => avatarBodyRenderer.sprite,
        //         AvatarPiece.FrontHand => avatarFrontHandRenderer.sprite,
        //         AvatarPiece.BackHand => avatarBackHandRenderer.sprite,
        //         AvatarPiece.Pants => avatarFrontLegRenderer.sprite,
        //         AvatarPiece.BackLeg => avatarBackLegRenderer.sprite,
        //         AvatarPiece.Background => avatarBackgroundRenderer.sprite,
        //         AvatarPiece.FrontHandItem => avatarFrontItemRenderer.sprite,
        //         AvatarPiece.BackHandItem => avatarBackItemRenderer.sprite,
        //         AvatarPiece.Hat => avatarHatRenderer.sprite,
        //         AvatarPiece.PetHead => avatarPetHeadRenderer.sprite,
        //         AvatarPiece.PetBody => avatarPetBodyRenderer.sprite,
        //         _ => throw new ArgumentOutOfRangeException(nameof(piece), piece, null)
        //     };
        // }
        public Color GetCurrentFaceColor()
        {
            return avatarHeadRenderer.color;
        }
        
        public Color GetCurrentHairColor()
        {
            return avatarHairRenderer.material.color;
        }
    }
    
    public enum AvatarPiece // Types of avatar pieces to change - Check to add more pieces
    {
        Face,
        Hair,
        Cloths,
        FrontHand,
        BackHand,
        Pants,
        BackLeg,
        Accessory,
        Pet,
        PetHead,
        PetBody,
        Background,
        FrontHandItem,
        BackHandItem,
        Hat
    }
}