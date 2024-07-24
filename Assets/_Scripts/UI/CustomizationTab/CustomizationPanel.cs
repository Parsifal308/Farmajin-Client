using System;
using System.Collections.Generic;
using Farmanji.Data;
using Farmanji.Managers;
using Farmanji.UI;
using UnityEngine;

namespace Farmanji.Game
{
    public class CustomizationPanel : MonoBehaviour
    {
        [SerializeField] private NotificationPanel _notificationPanel;
        
        [Header("Items Content Lists")] 
        [SerializeField] private ShopTab _shopTab;
        [SerializeField] private InventoryPanel _inventoryPanel;
        [SerializeField] private GameObject facesListContent;
        [SerializeField] private GameObject hairsListContent;
        [SerializeField] private GameObject clothsListContent;
        [SerializeField] private GameObject pantsListContent;
        [SerializeField] private GameObject accesoriesListContent;
        [SerializeField] private GameObject petsListContent;
        [SerializeField] private GameObject backgroundsListContent;

        [Header("Customization Button Prefabs")]
        [SerializeField] private CustomizationButton CustomizationButtonPrefab;
        [SerializeField] private ClothsCustomizationButton ClothsCustomizationButtonPrefab;
        [SerializeField] private PantsCustomizationButton PantsCustomizationButtonPrefab;
        [SerializeField] private PetCustomizationButton PetCustomizationButtonPrefab;
        //[SerializeField] private BannerCustomizationButton BannerCustomizationButtonPrefab;
        
        private List<ItemData> _itemsData;
        private AvatarManager _avatarManager;

        private void Start()
        {
            if (_shopTab == null)
            {
                _shopTab = TabsManager.Instance.Shop.GetComponent<ShopTab>();
                if(_shopTab) _shopTab.OnItemUnlocked.AddListener(CreateCustomizationButton);
            }
            if (_avatarManager == null) _avatarManager = AvatarManager.Instance;
            _itemsData = ResourcesLoaderManager.Instance.Items.UserItems;
            CreateCustomizationButtons(_itemsData);
            if (_notificationPanel) _notificationPanel.OnItemBuyed += CreateCustomizationButton;
        }

        private void CreateCustomizationButtons(List<ItemData> itemsData) //Buttons created on Data Loaded from sv
        {
            if (itemsData.Count <= 0) return;
            foreach (var item in itemsData)
            {
                var customizationButton = item.AvatarPiece switch
                {
                    AvatarPiece.Cloths => Instantiate(ClothsCustomizationButtonPrefab,
                        GetCategoryPanel(item.AvatarPiece).transform),
                    AvatarPiece.Pants => Instantiate(PantsCustomizationButtonPrefab,
                        GetCategoryPanel(item.AvatarPiece).transform),
                    AvatarPiece.Pet => Instantiate(PetCustomizationButtonPrefab,
                        GetCategoryPanel(item.AvatarPiece).transform),
                    _ => Instantiate(CustomizationButtonPrefab, GetCategoryPanel(item.AvatarPiece).transform)
                };
                customizationButton.InitializeCustomizationButton(item, _avatarManager);
                if (_inventoryPanel) _inventoryPanel.CreateInventoryButton(item.PreviewSprite);
            }
        }

        public void CreateCustomizationButton(ItemData item) //Button created when buying or unlocking it in the shop
        {
            if (!(item is { Obtained: true })) return;
            var customizationButton = item.AvatarPiece switch
            {
                AvatarPiece.Cloths => Instantiate(ClothsCustomizationButtonPrefab,
                    GetCategoryPanel(item.AvatarPiece).transform),
                AvatarPiece.Pants => Instantiate(PantsCustomizationButtonPrefab,
                    GetCategoryPanel(item.AvatarPiece).transform),
                AvatarPiece.Pet => Instantiate(PetCustomizationButtonPrefab,
                    GetCategoryPanel(item.AvatarPiece).transform),
                _ => Instantiate(CustomizationButtonPrefab, GetCategoryPanel(item.AvatarPiece).transform)
            };
            customizationButton.InitializeCustomizationButton(item, _avatarManager);
            if (_inventoryPanel) _inventoryPanel.CreateInventoryButton(item.PreviewSprite);
        }
        
        public GameObject GetCategoryPanel(AvatarPiece avatarPiece) //Getting the panel according to the avatar piece 
        {
            return avatarPiece switch
            {
                AvatarPiece.Face => facesListContent,
                AvatarPiece.Hair => hairsListContent,
                AvatarPiece.Cloths => clothsListContent,
                AvatarPiece.Pants => pantsListContent,
                AvatarPiece.Accessory => accesoriesListContent,
                AvatarPiece.Pet => petsListContent,
                AvatarPiece.Background => backgroundsListContent,
                AvatarPiece.FrontHand => null,
                AvatarPiece.BackHand => null,
                AvatarPiece.BackLeg => null,
                AvatarPiece.PetHead => null,
                AvatarPiece.PetBody => null,
                AvatarPiece.FrontHandItem => accesoriesListContent,
                AvatarPiece.BackHandItem => accesoriesListContent,
                AvatarPiece.Hat => accesoriesListContent,
                _ => throw new ArgumentOutOfRangeException(nameof(avatarPiece), avatarPiece, null)
            };
        }
    }
}