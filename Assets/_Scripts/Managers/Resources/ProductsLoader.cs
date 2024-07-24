using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    public class ProductsLoader : ResourceLoader
    {
        [SerializeField] protected JsonLoader _userProductsLoader;
        [SerializeField] protected JsonLoader _userOrdersLoader;
        public List<ProductData> Products = new List<ProductData>();
        public List<ProductData> UserProducts = new List<ProductData>();
        public List<ProductData> UserOrders = new List<ProductData>();

        public override IEnumerator Load()
        {
            Products.Clear();

            yield return StartCoroutine(_jsonLoader.LoadFromWeb<ProductsCollection>("?page=1&perPage=250"));

            yield return StartCoroutine(CreateConcreteData());
            
            yield return StartCoroutine(_userProductsLoader.LoadFromWeb<UserProductsCollection>("?page=1&perPage=500"));

            yield return StartCoroutine(CreateUserProductsData());
            
            yield return StartCoroutine(_userOrdersLoader.LoadFromWeb<OrdersCollection>("?page=1&perPage=500"));

            yield return StartCoroutine(CreateOrdersData());
        }
        
        public override IEnumerator CreateConcreteData()
        {
            var response = _jsonLoader.ResponseInfo as ProductsCollection;

            if (response != null)
                foreach (var productElement in response.data)
                {
                    List<Sprite> images=new List<Sprite>();
                    foreach (var url in productElement.assets)
                    {
                        yield return StartCoroutine(DownloadImage(url));
                        images.Add(DownloadedSprite);
                    }
                    /*
                    yield return StartCoroutine(DownloadImage(productElement.assets[0]));
                    var MainImage = DownloadedSprite;

                    yield return StartCoroutine(DownloadImage(productElement.assets[1]));
                    var SecondaryImage = DownloadedSprite;
                    */
                    Products.Add(new ProductData(productElement, images));
                }

            yield return null;
        }

        private IEnumerator CreateUserProductsData()
        {
            UserProducts.Clear();
            
            var response = _userProductsLoader.ResponseInfo as UserProductsCollection;

            if (response != null)
                foreach (var productElement in response.data)
                {
                    foreach (var product in productElement.products)
                    {
                        //yield return StartCoroutine(DownloadImage(product.imageUrl));
                        //var MainImage = DownloadedSprite;
                        List<Sprite> images = new List<Sprite>();
                        foreach (var url in product.assets)
                        {
                            yield return StartCoroutine(DownloadImage(url));
                            images.Add(DownloadedSprite);
                        }

                        UserProducts.Add(new ProductData(product, images, null));
                    }
                }

            yield return null;
        }
        
        private IEnumerator CreateOrdersData()
        {
            UserOrders.Clear();
            
            var response = _userOrdersLoader.ResponseInfo as OrdersCollection;

            if (response != null)
                foreach (var orderElement in response.orders)
                {
                    List<Sprite> images = new List<Sprite>();
                    yield return StartCoroutine(DownloadImage(orderElement.productId.imageUrl));
                    images.Add(DownloadedSprite);

                    UserOrders.Add(new ProductData(orderElement.productId, images, orderElement.code));
                }

            yield return null;
        }

        internal ProductData FindProductByID(string id)
        {
            foreach (var product in Products)
            {
                if (product.Id == id)
                {
                    return product;
                }
            }
            Debug.Log("NO SE ENCONTRO PRODUCTO CON DICHO ID");
            return null;
        }
    }
}