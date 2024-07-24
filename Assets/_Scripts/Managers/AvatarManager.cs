using Farmanji.Data;
using Farmanji.Game;
using UnityEngine;

namespace Farmanji.Managers
{
    public class AvatarManager : SingletonPersistent<AvatarManager>
    {
        [SerializeField] private CustomizableAvatar _customizableAvatar;
        private UserCustomizationPost _userCustomizationPost;

        private void Start()
        {
            if (_userCustomizationPost == null) _userCustomizationPost = GetComponent<UserCustomizationPost>();
            if (_customizableAvatar == null) _customizableAvatar = GetComponent<CustomizableAvatar>();
        }

        public void LoadAvatarData() //TODO change this to load the items from the server
        {
            // Loading the saved data
            var LoadedData = ResourcesLoaderManager.Instance.Items.UserCustomization;
            
            // Changing the customization of the avatar to the Loaded data
            _customizableAvatar.SetPiece(LoadedData.Face, AvatarPiece.Face, LoadedData.Face.MainSprite);
            _customizableAvatar.SetPiece(LoadedData.Hair, AvatarPiece.Hair, LoadedData.Hair.MainSprite);
            _customizableAvatar.SetPiece(LoadedData.Trunk, AvatarPiece.Cloths, LoadedData.Trunk.MainSprite);
            _customizableAvatar.SetPiece(LoadedData.Trunk, AvatarPiece.FrontHand, LoadedData.Trunk.SecondarySprite);
            _customizableAvatar.SetPiece(LoadedData.Trunk, AvatarPiece.BackHand, LoadedData.Trunk.TerciarySprite);
            _customizableAvatar.SetPiece(LoadedData.Legs, AvatarPiece.Pants, LoadedData.Legs.MainSprite);
            _customizableAvatar.SetPiece(LoadedData.Legs, AvatarPiece.BackLeg, LoadedData.Legs.SecondarySprite);
            _customizableAvatar.SetPiece(LoadedData.Hat, AvatarPiece.Hat, LoadedData.Hat.MainSprite);
            _customizableAvatar.SetPiece(LoadedData.Pet, AvatarPiece.PetHead, LoadedData.Pet.MainSprite);
            _customizableAvatar.SetPiece(LoadedData.Pet, AvatarPiece.PetBody, LoadedData.Pet.SecondarySprite);
            //_customizableAvatar.TintPiece(AvatarPiece.Face, LoadedData.FaceColor);
            //_customizableAvatar.TintPiece(AvatarPiece.Hair, LoadedData.HairColor);
            
            //Debug.Log("Avatar Data Loaded");
        }

        public void SaveAvatarData() //TODO change this to save the items to the server data
        {
            // Saving current avatar data

            var currentCustomization = _customizableAvatar.GetCurrentCustomization;
            var hat = UserCustomizationBody.Create(currentCustomization.Hat.Id, currentCustomization.Hat.ItemType);
            var hair = UserCustomizationBody.Create(currentCustomization.Hair.Id, currentCustomization.Hair.ItemType);
            var face = UserCustomizationBody.Create(currentCustomization.Face.Id, currentCustomization.Face.ItemType);
            var trunk = UserCustomizationBody.Create(currentCustomization.Trunk.Id, currentCustomization.Trunk.ItemType);
            var legs = UserCustomizationBody.Create(currentCustomization.Legs.Id, currentCustomization.Legs.ItemType);
            var pet = UserCustomizationBody.Create(currentCustomization.Pet.Id, currentCustomization.Pet.ItemType);
            
            StartCoroutine(_userCustomizationPost.Post(hat));
            StartCoroutine(_userCustomizationPost.Post(hair));
            StartCoroutine(_userCustomizationPost.Post(face));
            StartCoroutine(_userCustomizationPost.Post(trunk));
            StartCoroutine(_userCustomizationPost.Post(legs));
            StartCoroutine(_userCustomizationPost.Post(pet));
            
            Debug.Log(hat.itemId + " " + hat.itemType);

            ResourcesLoaderManager.Instance.Items.UserCustomization = currentCustomization;
            
            Debug.Log("Avatar Data Saved");
        }
        
        public void SetAvatarPiece(ItemData itemData , AvatarPiece avatarPiece, Sprite spriteToSet) //Method called on CustomButton to change an avatar piece sprite
        {
            _customizableAvatar.SetPiece(itemData, avatarPiece,spriteToSet);
        }

        public void TintAvatarPiece(AvatarPiece avatarPiece, Color color) //Method calld on ColorSelectorButton to tint the sprite of a piece
        {
            _customizableAvatar.TintPiece(avatarPiece,color);
        }
    }
}