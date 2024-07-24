using Farmanji.Auth;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Farmanji.Data;
using Farmanji.Managers;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Farmanji.Game
{
    public class StoreSlot : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Button claimButton;
        [SerializeField] private string id;
        [SerializeField] private TextMeshProUGUI coinsPriceText;
        [SerializeField] private TextMeshProUGUI gemsPriceText;
        [SerializeField] private TextMeshProUGUI codeClaimText;
        [SerializeField] private ItemUnlockMode _itemUnlockMode;
        [SerializeField] private TextMeshProUGUI buttonText;
        
        public string GetSlotId
        {
            get { return id; }
        }

        public Button GetClaimButton { get { return claimButton; } }
        #endregion

        #region METHODS
        public void InitializeSlot(ItemData data)
        {
            itemIcon.sprite = data.MainSprite;
            id = data.Id;
            
            if (coinsPriceText != null) coinsPriceText.text = data.CoinsPrice.ToString();
            if (gemsPriceText != null) gemsPriceText.text = data.GemsPrice.ToString();

            _itemUnlockMode = data.UnlockMode;
            if (ResourcesLoaderManager.Instance.Worlds.GetLevelsData.TryGetValue(data.LevelId, out var value))
            {
                if (!value.LevelCompleted) claimButton.interactable = false;
            }

            UnityAction showPopUpAction = () => GetComponentInParent<ShopTab>().ShowClaimPopUp(data);
            UnityAction destroyAction = () => GetComponentInParent<ShopTab>().OnItemUnlocked.AddListener(DestroySlot);
            
            claimButton.onClick.AddListener(showPopUpAction);
            claimButton.onClick.AddListener(destroyAction);
        }

        public void InitializeSlot(ProductData data)
        {
            if(buttonText != null) buttonText.text = "CANJEAR";
            if(data.images[0]) itemIcon.sprite = data.images[0];
            
            if (codeClaimText != null && data.Code != null && data.Code != "") 
                codeClaimText.text = data.Code;
            
            if (gemsPriceText != null) gemsPriceText.text = data.GemsPrice.ToString();
            if (coinsPriceText != null) coinsPriceText.text = data.CoinsPrice.ToString();

            if (claimButton == null) return;
            //Si es del tipo comprable setear texto del precio 
            if (data.Stock <= 0 & data.Category != "item") {
                claimButton.interactable = false;
                if (buttonText != null) buttonText.text = "SIN STOCK";
            }
            if (!data.Unlocked)
            {
                claimButton.interactable = false;
                if (buttonText != null) buttonText.text = "BLOQUEADO";
            }
            
            id = data.Id;
            
            UnityAction showPopUpAction = () => BuyProduct(data);
            UnityAction destroyAction = () => GetComponentInParent<ShopTab>().OnProductUnlocked.AddListener(DestroySlot);
            claimButton.onClick.AddListener(showPopUpAction);
            claimButton.onClick.AddListener(destroyAction);
        }

        public void UnlockPriceText()
        {
            if (buttonText != null) buttonText.text = "CANJEAR";
        }

        private void DestroySlot(ItemData itemData)
        {
            if (itemData.Id == id) Destroy(gameObject);
            else GetComponentInParent<ShopTab>().OnItemUnlocked.RemoveListener(DestroySlot);
        }

        private void DestroySlot(ProductData productData)
        {
            if (productData.Id == id) Destroy(gameObject);
            else GetComponentInParent<ShopTab>().OnProductUnlocked.RemoveListener(DestroySlot);
        }


        private void BuyProduct(ProductData data)
        {
            GetComponentInParent<ShopTab>().ShowClaimPopUp(data);

            //GetComponentInParent<ShopTab>().UpdateToClaimList();
        }
        #endregion
    }
}