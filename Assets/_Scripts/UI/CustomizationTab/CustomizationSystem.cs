using System.Collections;
using Farmanji.Data;
using Farmanji.Managers;
using Farmanji.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class CustomizationSystem : MonoBehaviour
    {
        private AvatarManager _avatarManager;
        [SerializeField] private CustomizationData _customizationData;
        [SerializeField] private CustomizationPanel _customizationPanel;
        [SerializeField] private InventoryPanel _inventoryPanel; //This has to be changed later no not take references outside the canvas 
        [SerializeField] private PopUp _confirmationPopUp;
        
        [Header("Customization Button Prefabs")]
        [SerializeField] private CustomizationButton _customizationButtonPrefab;
        [SerializeField] private CustomizationButton _customizationBannerButtonPrefab;
        [SerializeField] private CustomizationButton _customizationClothsButtonPrefab;
        [SerializeField] private CustomizationButton _customizationPantsButtonPrefab;
        [SerializeField] private CustomizationButton _customizationPetButton;
        [SerializeField] private CustomizationButton _customizationAccesoryButton;
        
        [Header("Button References")]
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _quitButton;

        [Header("Backgrounds")]
        [SerializeField] private Image _customBackground;
        [SerializeField] private Image _profileCustomBackground;

        private void Start()
        {
            if (_avatarManager == null) _avatarManager = AvatarManager.Instance;
            if (_customizationData != null) CreateButtons();
            if (_saveButton != null && _avatarManager != null) _saveButton.onClick.AddListener(OnSaved);
            if (_quitButton != null && _avatarManager != null) _quitButton.onClick.AddListener(OnNotSaved);
        }
        
        private void CreateButtons()
        {
            foreach (var pieceData in _customizationData.PiecesData)
            {
                var PreviewImageWidth = pieceData.AvatarPiece == AvatarPiece.Background ? 750f : 280f;
                var PreviewImageHeight = pieceData.AvatarPiece == AvatarPiece.Background ? 435f : 280f;
                
                if(_customizationBannerButtonPrefab == null || _customizationClothsButtonPrefab == null || 
                   _customizationButtonPrefab == null || _customizationPantsButtonPrefab == null) return;
                
                var customizationButtonPrefab = pieceData.AvatarPiece switch
                {
                    AvatarPiece.Background => _customizationBannerButtonPrefab,
                    AvatarPiece.Cloths => _customizationClothsButtonPrefab,
                    AvatarPiece.Pants=> _customizationPantsButtonPrefab,
                    _ => _customizationButtonPrefab
                };

                var ButtonsCount = 0;
                foreach (var piece in pieceData.Pieces)
                {
                    if (piece.Sprite == null) continue;
                    
                    var customizationButton = CreateButton(customizationButtonPrefab, pieceData.AvatarPiece, piece.Sprite, piece.StartAvailable, 
                        PreviewImageWidth, PreviewImageHeight, piece.PreviewSprite);
                    
                    if (_inventoryPanel != null && piece.StartAvailable) _inventoryPanel.CreateInventoryButton(piece.Sprite);
                    ButtonsCount++;
                         
                    if (customizationButton == null) continue;

                    if (pieceData.AvatarPiece == AvatarPiece.Cloths)
                    {
                        var clothsCustomizationButton = customizationButton.GetComponent<ClothsCustomizationButton>();
                        if (clothsCustomizationButton)
                        {
                            //clothsCustomizationButton.SetHandSprites(piece.SecondarySprite,piece.TerciarySprite);
                        }
                    }
                    else if (pieceData.AvatarPiece == AvatarPiece.Pants)
                    {
                        var pantsCustomizationButton = customizationButton.GetComponent<PantsCustomizationButton>();
                        if (pantsCustomizationButton)
                        {
                            //pantsCustomizationButton.SetLegSprites(piece.SecondarySprite);
                        }
                    }
                }
            }

            foreach (var accesory in _customizationData.AccesoriesData)
            {
                var button = CreateButton(_customizationAccesoryButton, AvatarPiece.Accessory, accesory.PieceData.PreviewSprite, accesory.PieceData.StartAvailable);
                var accesoryButton = button.GetComponent<ItemCustomizationButton>();
                if (accesoryButton) accesoryButton.SetItemButton(accesory.AccesoryType);
                if (_inventoryPanel != null && accesory.PieceData.StartAvailable) _inventoryPanel.CreateInventoryButton(accesory.PieceData.PreviewSprite);
            }
            
            foreach (var pet in _customizationData.PetsData)
            {
                var button = CreateButton(_customizationPetButton, AvatarPiece.Pet, pet.PieceData.PreviewSprite, pet.PieceData.StartAvailable);
                var petButton = button.GetComponent<PetCustomizationButton>();
                //if (petButton) petButton.SetPetSprites(pet.PetHeadSprite, pet.PetBodySprite);
                if (_inventoryPanel != null && pet.PieceData.StartAvailable) _inventoryPanel.CreateInventoryButton(pet.PieceData.PreviewSprite);
            }
        }

        private CustomizationButton CreateButton(CustomizationButton CustomizationButtonPrefab, AvatarPiece AvatarPiece, Sprite SpriteToSet, bool StartAvailable,
            float ImageWidth = 280f, float ImageHeight = 280f, Sprite PreviewSprite = null)
        {
            // Get category of where to create the button using avatarPiece
            var PanelTransform = _customizationPanel.GetCategoryPanel(AvatarPiece).transform;
            // Creating the button and setting it
            var customizationButton = Instantiate(CustomizationButtonPrefab, PanelTransform);
            //customizationButton.SetCustomizationButton(this, AvatarPiece, SpriteToSet, StartAvailable, ImageWidth, ImageHeight, PreviewSprite);
            return customizationButton;
        }
        

        public void TintAvatarPiece(AvatarPiece avatarPiece, Color color) //Method called on ColorCustomButton to tint an avatar piece
        {
            _avatarManager.TintAvatarPiece(avatarPiece,color);
        }

        public void SetAvatarBackground(Sprite backgroundToSet)
        {
            if (_customBackground == null || _profileCustomBackground == null) return;
            if (backgroundToSet == null)
            {
                if (_customBackground.enabled) _customBackground.enabled = false;
                if (_profileCustomBackground.enabled) _profileCustomBackground.enabled = false;
            }
            else
            {
                if (!_customBackground.enabled) _customBackground.enabled = true;
                _customBackground.sprite = backgroundToSet;
                if (!_profileCustomBackground.enabled) _profileCustomBackground.enabled = true;
                _profileCustomBackground.sprite = backgroundToSet;
            }
        }

        private void OnSaved() // If we save we will popu confirmation
        {
            _avatarManager.SaveAvatarData();
            _confirmationPopUp.Open();
        }

        private void OnNotSaved() // If we didnt save we will lost the last changes 
        {
            StartCoroutine(ResetAvatarPiecesCoroutine());
        }

        private IEnumerator ResetAvatarPiecesCoroutine() // Last changes will be changed to the saved settings
        {
            yield return new WaitForSeconds(.35f);
            if (_avatarManager) _avatarManager.LoadAvatarData();
        }
    }
}