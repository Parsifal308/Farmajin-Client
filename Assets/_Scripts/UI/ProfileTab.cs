using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Farmanji.Data;
using static Farmanji.Data.AchievementsLoader;
using Farmanji.Managers;
using Farmanji.Auth;

namespace Farmanji.Game
{
    public class ProfileTab : BaseTab
    {
        #region FIELDS
        [SerializeField] private RectTransform _boardTransform;
        [SerializeField] private Image platformImage;
        [SerializeField] private Image faceImage;
        [SerializeField] private Image hairImage;
        [SerializeField] private Image headImage;
        [SerializeField] private Image legsImage;
        [SerializeField] private Image petImage;
        [Header("AVATAR PANEL:")]
        [SerializeField] private Button _shareButton;
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private TextMeshProUGUI playerLevel;
        [SerializeField] private TextMeshProUGUI playerSection;
        [Header("INVENTORY PANEL:")]
        [Space(20), SerializeField] private GameObject inventoryList;
        [Space(10), SerializeField] private GameObject inventorySlotPrefab;
        [Header("ACHIEVEMENTS PANEL:")]
        [Space(20), SerializeField] private GameObject achievementsList;
        [Space(10), SerializeField] private GameObject achievementSlotPrefab;
        #endregion

        #region UNITY METHODs
        void Start()
        {
            UpdateAchievements(ResourcesLoaderManager.Instance.UserAchievements.Achievements);
            _shareButton.onClick.AddListener(Share);
            UpdatePlayerInfo();
        }
        #endregion

        #region MEHTODS
        public void UpdatePlayerInfo()
        {
            //REEMPLAZAR CON INFO TRAIDA DE VAYA A SABER DIOS DONDE
            playerName.text = SessionManager.Instance.UserData.UserInfo.Name;
            playerSection.text = SessionManager.Instance.UserData.UserInfo.Role;
            //playerLevel.text = "1";     
        }
        public void UpdateCustomization()
        {
            // background.sprite = ResourcesLoaderManager.Instance.
            // platform.sprite = ResourcesLoaderManager.Instance.
            // face.sprite = ResourcesLoaderManager.Instance.
            // hair.sprite = ResourcesLoaderManager.Instance.
            // head.sprite = ResourcesLoaderManager.Instance.
            // legs.sprite = ResourcesLoaderManager.Instance.
            // pet.sprite = ResourcesLoaderManager.Instance.
        }
        public void UpdateInventory()
        {
            // List<ScriptableObject> inventory = new List<ScriptableObject>(); //CAMBIAR POR LOS ITEMS QE POSEE EL USUARIO
            // for (int i = 0; i < inventory.Count; i++)
            // {
            //     GameObject slot = GameObject.Instantiate(inventorySlotPrefab, inventoryList.transform);
            //     slot.GetComponent<Image>().sprite = inventory[i].........;//Fondo de la targetita de item
            //     slot.GetComponentInChildren<Image>().sprite = inventory[i].........;//IMAGEN DEL ITEM EN CUESTION
            // }
        }

        private void UpdateAchievements(List<UserAchievementData> data)
        {
            if (data == null || data.Count <= 0) return;
            
            for (int i = 0; i < data[0].Badges.Count; i++)
            {
                Instantiate(achievementSlotPrefab, achievementsList.transform).GetComponent<AchievementSlot>().InitializeSlot(data[0].Badges[i]);
            }
            // foreach (var achievementData in data)
            // {
            //     Instantiate(achievementSlotPrefab, achievementsList.transform).GetComponent<AchievementSlot>().InitializeSlot(achievementData);
            // }
        }

        private void Share()
        {
            StartCoroutine(Utilities.ShareUtility.TakeScreenShotAndShare(_boardTransform, "Mi perfil!"));
        }
        #endregion
    }
}