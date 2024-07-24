using System.Collections;
using System.Collections.Generic;
using Farmanji.Auth;
using Farmanji.Data;
using Farmanji.Managers;
using Farmanji.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Farmanji.Game
{
    public class ShopTab : BaseTab
    {
        #region FIELSD
        [Header("CONTAINERS:")]
        [SerializeField] private Transform rewardsSlotsContainer;
        [SerializeField] private Transform unlockeablesSlotsContainer;
        [Header("PREFABS:")]
        [SerializeField] private GameObject rewardsStoreSlotPrefab;
        [SerializeField] private GameObject unlockeableSlotPrefab;
        [SerializeField] private GameObject toClaimSlotPrefab;
        [Header("REFERENCES:")]
        [SerializeField] private ShopPopUp _shopPopUp;
        [SerializeField] private ShopPopUp _claimPopUp;
        [Header("CONFIGS:")]
        [SerializeField] private ItemPost _itemPost;
        [SerializeField] private PhysicalProductPost _physicalProductPost;
        [SerializeField] private bool showObtained;

        [SerializeField] private NotificationPanel _notificationPanel;
        [SerializeField] private List<StoreSlot> _storeSlots;

        public UnityEvent<ItemData> OnItemUnlocked;
        public UnityEvent<ProductData> OnProductUnlocked;
        #endregion

        #region PROPERTIES
        public GameObject UnlockeableSlotPrefab { get { return unlockeableSlotPrefab; } }
        public Transform UnlockeablesSlotsContainer { get { return unlockeablesSlotsContainer; } }
        #endregion

        #region UNITY METHODS
        void Start()
        {
            //InitializeItemSlots(ResourcesLoaderManager.Instance.Items.Items);
            InitializeProductsSlots(ResourcesLoaderManager.Instance.Products.Products);
            InitializeClaimSlots(ResourcesLoaderManager.Instance.Products.UserProducts);
            InitializeClaimSlots(ResourcesLoaderManager.Instance.Products.UserOrders); 
            UpdateToClaimList();
            _itemPost = GetComponent<ItemPost>();
            _physicalProductPost = GetComponent<PhysicalProductPost>();
            _notificationPanel.OnProductUnlocked += UnlockSlotByID;
        }
        #endregion

        #region METHODS

        private void InitializeItemSlots(List<ItemData> items)
        {
            foreach (var item in items)
            {
                if (showObtained)
                {
                    var slot = Instantiate(rewardsStoreSlotPrefab, rewardsSlotsContainer.transform).GetComponent<StoreSlot>();
                    slot.InitializeSlot(item);
                }
                else
                {
                    var slot = Instantiate(rewardsStoreSlotPrefab, rewardsSlotsContainer.transform).GetComponent<StoreSlot>();
                    slot.InitializeSlot(item);
                }
            }
        }

        public void InitializeProductsSlots(List<ProductData> products)
        {
            bool hasAnyElement = false;
            foreach (var product in products)
            {
                var slot = Instantiate(unlockeableSlotPrefab, rewardsSlotsContainer.transform).GetComponent<StoreSlot>();
                slot.InitializeSlot(product);
                _storeSlots.Add(slot);
            }
        }

        public void InitializeClaimSlots(List<ProductData> products)
        {
            bool hasAnyElement = false;
            
            if (products.Count <= 0) return;
            foreach (var product in products)
            {
                var slot = Instantiate(toClaimSlotPrefab, unlockeablesSlotsContainer.transform).GetComponent<StoreSlot>();
                slot.InitializeSlot(product);
                hasAnyElement = true;
            }
            
            if(hasAnyElement)
            {
                unlockeablesSlotsContainer.transform.parent.parent.parent.gameObject.SetActive(true);
            }
        }


        public void UpdateToClaimList()
        {
            Debug.Log("UpdatingToClaimList");
            foreach(Transform t in unlockeablesSlotsContainer.transform)
            {
                Destroy(t.gameObject);
            }

            var hasAnyElement = false;
            foreach (var product in ResourcesLoaderManager.Instance.Products.UserOrders)
            {
                hasAnyElement = true;

                var slotForClaim = Instantiate(toClaimSlotPrefab, unlockeablesSlotsContainer.transform).GetComponent<StoreSlot>();
                slotForClaim.InitializeSlot(product);
            }

            if (hasAnyElement)
            {
                unlockeablesSlotsContainer.transform.parent.parent.parent.gameObject.SetActive(true);
            }
        }

        public void ShowClaimPopUp(ItemData itemData)
        {
            if (_shopPopUp == null || _shopPopUp.gameObject.activeSelf) return;
            
            switch (itemData.UnlockMode)
            {
                case ItemUnlockMode.Progress:
                    _claimPopUp.ShowPopUp(itemData);
                    break;
                case ItemUnlockMode.Buy:
                    _shopPopUp.ShowPopUp(itemData);
                    break;
            }
        }
        
        public void ShowClaimPopUp(ProductData productData)
        {
            if (_shopPopUp != null && !_shopPopUp.gameObject.activeSelf)
            {
                _shopPopUp.ShowPopUp(productData);
            }
        }

        public void PostItemData(ItemBody itemBody, ItemData itemData)
        {
            OnItemUnlocked?.Invoke(itemData);
            StartCoroutine(_itemPost.Post(itemBody));
        }

        public void PostProductData(ProductData productData)
        {
            StartCoroutine(PostProduct(productData));
        }
        
        private void UnlockSlotByID(ProductData data)
        {
            foreach (var storeSlot in _storeSlots)
            {
                if (storeSlot.GetSlotId == data.Id)
                {
                    storeSlot.GetClaimButton.interactable = true;
                    storeSlot.UnlockPriceText();
                    return;
                }
            }
            Debug.Log("NO SE ENCONTRO PRODUCTO CON DICHO ID");
        }

        private IEnumerator PostProduct(ProductData productData)
        {
            OnProductUnlocked?.Invoke(productData);
            if (productData.Category == "physical")
            {
                var physicalBody = PhysicalProductBody.Create(productData.Id);
                yield return StartCoroutine(_physicalProductPost.Post(physicalBody));
                productData.Code = _physicalProductPost.Response.order.code;
                ResourcesLoaderManager.Instance.Products.UserOrders.Add(productData);
                UpdateToClaimList();
            }
            else
            {
                var itemBody = ItemBody.CreateItemBody(productData.Id, SessionManager.Instance.UserData._basicInfo.Id);
                yield return StartCoroutine(_itemPost.Post(itemBody));
                if (_itemPost.GetPost.Request.responseCode == 200)
                {
                    TabsManager.Instance.Customization.GetComponentInChildren<CustomizationPanel>().
                        CreateCustomizationButton(ResourcesLoaderManager.Instance.Items.FindItemByProductID(productData.Id));
                }
            }
        }
        #endregion
    }
}