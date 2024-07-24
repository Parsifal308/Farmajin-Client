using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    [System.Serializable]
    public class CollectibleItemData
    {
        #region FIELDS 
        [Header("Collectible Data:")]
        [SerializeField] private string id;
        [SerializeField] private string name;
        [SerializeField] private Sprite icon;

        [Header("Collectible-Profile Data:")]
        [SerializeField] private bool obtained;
        #endregion

        #region PROPERTIES
        #endregion

        #region UNITY METHODS
        #endregion

        #region METHODS
        #endregion
    }
}