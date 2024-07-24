using Farmanji.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class ShopPopUp : MonoBehaviour
    {
        [SerializeField] protected Button _claimButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] protected ItemData _itemData;
        [SerializeField] protected ProductData _productData;
        [SerializeField] protected ShopTab _shopTab;
        [SerializeField] protected TextMeshProUGUI _popUpText;


        private void Start()
        {
            _claimButton.onClick.AddListener(ClaimButtonPressed);
            _exitButton.onClick.AddListener(ExitButtonPressed);
            _shopTab = GetComponentInParent<ShopTab>();
        }

        public void ShowPopUp(ItemData itemData)
        {
            _itemData = itemData;
            _productData = null;
            SetPopUpText();
            gameObject.SetActive(true);
        }

        public void ShowPopUp(ProductData productData)
        {
            _productData = productData;
            _itemData = null;
            SetPopUpText();
            gameObject.SetActive(true);
        }

        protected virtual void SetPopUpText()
        {
            if (_productData != null)
            {
                if (_popUpText) _popUpText.SetText("¿Deseas canjear " + _productData.Name + " por " + _productData.CoinsPrice + " monedas y " + _productData.GemsPrice + " gemas?");
            }
            else if (_itemData != null)
            {
                if (_popUpText) _popUpText.SetText("¿Deseas canjear " + _itemData.Name + " por " + _itemData.CoinsPrice + " monedas y " + _itemData.GemsPrice + " gemas?");
            }
        }

        protected virtual void ClaimButtonPressed()
        {
            gameObject.SetActive(false);
        }

        protected virtual void ExitButtonPressed()
        {
            gameObject.SetActive(false);
        }
    }
}