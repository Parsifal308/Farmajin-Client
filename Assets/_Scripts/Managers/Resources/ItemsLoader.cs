using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    public class ItemsLoader : ResourceLoader
    {
        [SerializeField] private JsonLoader _userItemsLoader;
        [SerializeField] private JsonLoader _userCustomizationLoader;

        public List<ItemData> Items = new List<ItemData>();
        public List<ItemData> UserItems = new List<ItemData>();
        //public List<ItemData> UserCustomization = new List<ItemData>();
        public UserCustomization UserCustomization;

        public override IEnumerator Load()
        {
            string queryParams = "?perPage=500";

            yield return StartCoroutine(_jsonLoader.LoadFromWeb<ItemsCollection>(queryParams, null));

            yield return StartCoroutine(CreateConcreteData());

            yield return StartCoroutine(_userItemsLoader.LoadFromWeb<UserItemCollection>(queryParams));

            yield return StartCoroutine(CreateUserItemsData());

            yield return StartCoroutine(_userCustomizationLoader.LoadFromWeb<UserCustomizationCollection>(queryParams));

            yield return StartCoroutine(CreateUserCustomizationData());
        }

        public override IEnumerator CreateConcreteData()
        {
            Items.Clear();
            UserItems.Clear();

            if (_jsonLoader.ResponseInfo is ItemsCollection response)
            {
                foreach (var itemElement in response.data)
                {
                    yield return StartCoroutine(DownloadImage(itemElement.itemUrl));
                    var PreviewSprite = DownloadedSprite;

                    yield return StartCoroutine(DownloadImage(itemElement.assets[0].url));
                    var MainSprite = DownloadedSprite;

                    Sprite SecondarySprite = null;
                    if (itemElement.assets.Length > 1)
                    {
                        yield return StartCoroutine(DownloadImage(itemElement.assets[1].url));
                        SecondarySprite = DownloadedSprite;
                    }

                    Sprite TerciarySprite = null;
                    if (itemElement.assets.Length > 2)
                    {
                        yield return StartCoroutine(DownloadImage(itemElement.assets[2].url));
                        TerciarySprite = DownloadedSprite;
                    }

                    if (itemElement.unlockMode != "buy")
                    {
                        UserItems.Add(new ItemData(itemElement, MainSprite, MainSprite, SecondarySprite, TerciarySprite));
                    }
                    else
                    {
                        Items.Add(new ItemData(itemElement, MainSprite, MainSprite, SecondarySprite, TerciarySprite));
                    }
                }
            }
            yield return null;
        }

        private IEnumerator CreateUserItemsData()
        {
            if (_userItemsLoader.ResponseInfo is UserItemCollection response)
            {
                foreach (var userItem in response.data)
                {
                    foreach (var item in userItem.items)
                    {
                        yield return StartCoroutine(DownloadImage(item.itemUrl));
                        var PreviewSprite = DownloadedSprite;

                        yield return StartCoroutine(DownloadImage(item.assets[0].url));
                        var MainSprite = DownloadedSprite;

                        Sprite SecondarySprite = null;
                        if (item.assets.Length > 1)
                        {
                            yield return StartCoroutine(DownloadImage(item.assets[1].url));
                            SecondarySprite = DownloadedSprite;
                        }

                        Sprite TerciarySprite = null;
                        if (item.assets.Length > 2)
                        {
                            yield return StartCoroutine(DownloadImage(item.assets[2].url));
                            TerciarySprite = DownloadedSprite;
                        }

                        UserItems.Add(new ItemData(item, MainSprite, MainSprite, SecondarySprite, TerciarySprite));
                    }
                }
            }
            yield return null;
        }

        private IEnumerator CreateUserCustomizationData()
        {
            var LoadedData = new List<ItemData>();

            if (_userCustomizationLoader.ResponseInfo is UserCustomizationCollection response)
            {
                if ((_userCustomizationLoader.ResponseInfo as UserCustomizationCollection).data.hat._id == null)
                {
                    UserCustomization.Hat = null;
                    UserCustomization.Hair = null;
                    UserCustomization.Face = null;
                    UserCustomization.Trunk = null;
                    UserCustomization.Legs = null;
                    UserCustomization.Pet = null;
                    yield return null;
                }
                else
                {
                    //HAT

                    yield return StartCoroutine(DownloadImage(response.data.hat.itemUrl));
                    var PreviewSprite = DownloadedSprite;

                    yield return StartCoroutine(DownloadImage(response.data.hat.assets[0].url));
                    var MainSprite = DownloadedSprite;

                    LoadedData.Add(new ItemData(response.data.hat, PreviewSprite, MainSprite, null, null));

                    //HAIR

                    yield return StartCoroutine(DownloadImage(response.data.hair.itemUrl));
                    PreviewSprite = DownloadedSprite;

                    yield return StartCoroutine(DownloadImage(response.data.hair.assets[0].url));
                    MainSprite = DownloadedSprite;

                    LoadedData.Add(new ItemData(response.data.hair, PreviewSprite, MainSprite, null, null));

                    //FACE

                    yield return StartCoroutine(DownloadImage(response.data.face.itemUrl));
                    PreviewSprite = DownloadedSprite;

                    yield return StartCoroutine(DownloadImage(response.data.face.assets[0].url));
                    MainSprite = DownloadedSprite;

                    LoadedData.Add(new ItemData(response.data.face, PreviewSprite, MainSprite, null, null));

                    //TRUNK

                    yield return StartCoroutine(DownloadImage(response.data.trunk.itemUrl));
                    PreviewSprite = DownloadedSprite;

                    yield return StartCoroutine(DownloadImage(response.data.trunk.assets[0].url));
                    MainSprite = DownloadedSprite;

                    Sprite SecondarySprite = null;
                    if (response.data.trunk.assets.Length > 1)
                    {
                        yield return StartCoroutine(DownloadImage(response.data.trunk.assets[1].url));
                        SecondarySprite = DownloadedSprite;
                    }

                    Sprite TerciarySprite = null;
                    if (response.data.trunk.assets.Length > 2)
                    {
                        yield return StartCoroutine(DownloadImage(response.data.trunk.assets[2].url));
                        TerciarySprite = DownloadedSprite;
                    }

                    LoadedData.Add(new ItemData(response.data.trunk, PreviewSprite, MainSprite, SecondarySprite, TerciarySprite));

                    //LEGS

                    yield return StartCoroutine(DownloadImage(response.data.legs.itemUrl));
                    PreviewSprite = DownloadedSprite;

                    yield return StartCoroutine(DownloadImage(response.data.legs.assets[0].url));
                    MainSprite = DownloadedSprite;

                    SecondarySprite = null;
                    if (response.data.legs.assets.Length > 1)
                    {
                        yield return StartCoroutine(DownloadImage(response.data.legs.assets[1].url));
                        SecondarySprite = DownloadedSprite;
                    }

                    LoadedData.Add(new ItemData(response.data.legs, PreviewSprite, MainSprite, SecondarySprite, null));

                    //PET

                    yield return StartCoroutine(DownloadImage(response.data.pet.itemUrl));
                    PreviewSprite = DownloadedSprite;

                    MainSprite = null;
                    if (response.data.pet.assets[0] != null && response.data.pet.assets != null)
                    {
                        yield return StartCoroutine(DownloadImage(response.data.pet.assets[0].url));
                        MainSprite = DownloadedSprite;
                    }
                    
                    SecondarySprite = null;
                    if (response.data.pet.assets.Length > 1)
                    {
                        yield return StartCoroutine(DownloadImage(response.data.pet.assets[1].url));
                        SecondarySprite = DownloadedSprite;
                    }

                    LoadedData.Add(new ItemData(response.data.pet, PreviewSprite, MainSprite, SecondarySprite, null));

                    UserCustomization.Hat = LoadedData[0];
                    UserCustomization.Hair = LoadedData[1];
                    UserCustomization.Face = LoadedData[2];
                    UserCustomization.Trunk = LoadedData[3];
                    UserCustomization.Legs = LoadedData[4];
                    UserCustomization.Pet = LoadedData[5];
                }
                yield return null;
            }
        }

        internal ItemData FindItemByID(string id)
        {
            foreach (var item in Items)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            Debug.Log("NO SE ENCONTRO ITEM CON DICHO ID");
            return null;
        }
        
        internal ItemData FindItemByProductID(string id)
        {
            foreach (var item in Items)
            {
                if (item.ProductId == id)
                {
                    return item;
                }
            }
            Debug.Log("NO SE ENCONTRO ITEM CON DICHO ID");
            return null;
        }
    }

    [System.Serializable]
    public struct UserCustomization
    {
        public ItemData Hat;
        public ItemData Hair;
        public ItemData Face;
        public ItemData Trunk;
        public ItemData Legs;
        public ItemData Pet;
    }
}