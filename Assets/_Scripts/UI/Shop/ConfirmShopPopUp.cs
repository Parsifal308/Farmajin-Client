using Farmanji.Data;
using Farmanji.Managers;
using UnityEngine;

namespace Farmanji.Game
{
    public class ConfirmShopPopUp : ShopPopUp
    {
        [SerializeField] private CongratsShopPopUp _congratsShopPopUp;
        protected override void ClaimButtonPressed()
        {
            if (_itemData != null)
            {
                if (ResourcesLoaderManager.Instance.Economy.Coins >= _itemData.CoinsPrice & ResourcesLoaderManager.Instance.Economy.Gems >= _itemData.GemsPrice)
                {
                    Debug.Log("_itemData coins " + _itemData.CoinsPrice);
                    Debug.Log("_itemData gems " + _itemData.CoinsPrice);
                    // EconomyBody body = EconomyBody.CreateCurrenciesBody(-_itemData.CoinsPrice, -_itemData.GemsPrice);
                    // StartCoroutine(ResourcesLoaderManager.Instance.Economy.GetComponent<EconomyPost>().Post(body));
                    CurrencyManager.Instance.SpendCoins(_itemData.CoinsPrice);
                    CurrencyManager.Instance.SpendGems(_itemData.GemsPrice);
                    base.ClaimButtonPressed();
                }
                _congratsShopPopUp.ShowPopUp(_itemData);
                return;
            }
            if (_productData != null)
            {
                if (ResourcesLoaderManager.Instance.Economy.Coins >= _productData.CoinsPrice & ResourcesLoaderManager.Instance.Economy.Gems >= _productData.GemsPrice)
                {
                    Debug.Log("_productData coins" + _productData.CoinsPrice);
                    Debug.Log("_productData gems" + _productData.GemsPrice);
                    // EconomyBody body = EconomyBody.CreateCurrenciesBody(-_productData.CoinsPrice, -_productData.GemsPrice);
                    // StartCoroutine(ResourcesLoaderManager.Instance.Economy.GetComponent<EconomyPost>().Post(body));
                    CurrencyManager.Instance.SpendCoins(_productData.CoinsPrice);
                    CurrencyManager.Instance.SpendGems(_productData.GemsPrice);
                    base.ClaimButtonPressed();
                }
                _congratsShopPopUp.ShowPopUp(_productData);
            }
        }

        protected override void SetPopUpText()
        {
            Debug.Log("_itemData " + _itemData);
            Debug.Log("_productData " + _productData);
            if (_productData != null)
            {
                if (CurrencyManager.Instance.CanBuyWithCoins(_productData.CoinsPrice) & CurrencyManager.Instance.CanBuyWithGems(_productData.GemsPrice))
                {
                    if (_popUpText) _popUpText.SetText("¿Deseas comprar " + _productData.Name + " por " + _productData.CoinsPrice + " monedas y " + _productData.GemsPrice + " gemas?");
                    return;
                }
                else
                {
                    if (_popUpText) _popUpText.SetText("No puedes comprar este objeto");
                    _claimButton.interactable = false;
                    return;
                }
            }
            if (_itemData != null)
            {
                if (CurrencyManager.Instance.CanBuyWithCoins(_itemData.CoinsPrice) & CurrencyManager.Instance.CanBuyWithGems(_itemData.GemsPrice))
                {
                    if (_popUpText) _popUpText.SetText("¿Deseas comprar " + _itemData.Name + " por " + _itemData.CoinsPrice + " monedas y " + _itemData.GemsPrice + " gemas?");
                    return;
                }
                else
                {
                    if (_popUpText) _popUpText.SetText("No puedes comprar este objeto");
                    _claimButton.interactable = false;
                    return;
                }
            }

            // //TODO chequear segun el tipo de moneda que pide, y cambiar el precio por el del item
            // if (CurrencyManager.Instance.CanBuyWithCoins(10))
            // {
            //     if (_productData.Id != "")
            //     {
            //         if (_popUpText) _popUpText.SetText("¿Deseas comprar " + _productData.Name + " por $" + _productData.Price + "?");
            //     }
            //     else if (_itemData.Id != "")
            //     {
            //         if (_popUpText) _popUpText.SetText("¿Deseas comprar " + _itemData.Name + " por $" + _itemData.Price + "?");
            //     }
            //     _claimButton.interactable = true;
            // }
            // else
            // {
            //     if (_popUpText) _popUpText.SetText("No puedes comprar este objeto");
            //     _claimButton.interactable = false;
            // }
        }
    }
}