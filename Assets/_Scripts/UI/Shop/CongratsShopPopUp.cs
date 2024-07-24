using Farmanji.Auth;
using Farmanji.Data;
using Farmanji.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class CongratsShopPopUp : ShopPopUp
    {
        [SerializeField] protected Image itemIcon;
        //Obtengo el item desde este PopUp ya que se puede aplicar para los objetos gratias (de progreso) y a los objetos que se compran
        protected override void ExitButtonPressed()
        {
            if (_itemData != null)
            {
                var _itemBody = ItemBody.CreateItemBody(_itemData.ProductId, SessionManager.Instance.UserData._basicInfo.Id); //TODO get user ID 
                Debug.Log(_itemData.Id + " , " + SessionManager.Instance.UserData._basicInfo.Id);
                _shopTab.PostItemData(_itemBody, _itemData);
            }
            else
            {
                _shopTab.PostProductData(_productData);
            }
            base.ExitButtonPressed();
        }

        protected override void SetPopUpText()
        {
            if (_productData != null)
            {
                if (_popUpText) _popUpText.SetText("¡Felicidades por adquirir " + _productData.Name + "!");
                itemIcon.sprite = _productData.images[0];
            }
            else if (_itemData != null)
            {
                if (_popUpText) _popUpText.SetText("¡Felicidades por adquirir " + _itemData.Name + "!");
                itemIcon.sprite = _itemData.PreviewSprite;
            }
        }
    }
}