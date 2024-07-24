using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Farmanji.Managers;

namespace Farmanji.Game
{
    public class InfoTab : MonoBehaviour
    {
        #region FIELDS
        [Header("PROFILE:"), Space(15)]
        [SerializeField] private Image avatar;
        [SerializeField] private GameObject progress;

        [Header("LEVEL:"), Space(15)]
        [SerializeField] private TextMeshProUGUI levelValue;

        [Header("CURRENCY:"), Space(15)]
        [SerializeField] private TextMeshProUGUI gemsValue;
        [SerializeField] private TextMeshProUGUI coinsValue;
        #endregion
        #region UNITY METHODs
        void Start()
        {
            UpdateInfo(0);
        }
        #endregion

        #region PUBLIC METHODS
        public void UpdateInfo(int index)
        {
            //levelValue.text = ResourcesLoaderManager.Instance.Worlds.List[index].level.ToString();
            gemsValue.text = ResourcesLoaderManager.Instance.Economy.Gems.ToString();
            coinsValue.text = ResourcesLoaderManager.Instance.Economy.Coins.ToString();
        }
        #endregion
    }
}