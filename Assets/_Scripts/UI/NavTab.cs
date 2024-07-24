using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class NavTab : MonoBehaviour
    {
        #region FIELDS
        [Header("BUTTONS:")]
        [SerializeField] private Button home;
        [SerializeField] private Sprite homeUnselected;
        [SerializeField] private Sprite homeSelected;

        [Space(10), SerializeField] private Button profile;
        [SerializeField] private Sprite profileUnselected;
        [SerializeField] private Sprite profileSelected;

        [Space(10), SerializeField] private Button achievements;
        [SerializeField] private Sprite achievementsUnselected;
        [SerializeField] private Sprite achievementsSelected;

        [Space(10), SerializeField] private Button store;
        [SerializeField] private Sprite storeUnselected;
        [SerializeField] private Sprite storeSelected;
        #endregion

        #region UNITY METHODS
        void Start()
        {
            TabsManager.Instance.OnCanvasChangedEvent.AddListener(UpdateButtons);
            UpdateButtons();
        }
        #endregion

        #region METHODS
        private void UpdateButtons()
        {
            switch (TabsManager.Instance.CurrentCanvas.gameObject.name)
            {
                case "HomeCanvas":
                    UnselectAll();
                    home.GetComponent<Image>().sprite = homeSelected;
                    break;
                case "ProfileCanvas":
                    UnselectAll();
                    profile.GetComponent<Image>().sprite = profileSelected;
                    break;
                case "ShopCanvas":
                    UnselectAll();
                    store.GetComponent<Image>().sprite = storeSelected;
                    break;
                case "AchievementsCanvas":
                    UnselectAll();
                    achievements.GetComponent<Image>().sprite = achievementsSelected;
                    break;
                default:
                    UnselectAll();
                    break;
            }

        }
        private void UnselectAll()
        {
            profile.GetComponent<Image>().sprite = profileUnselected;
            store.GetComponent<Image>().sprite = storeUnselected;
            achievements.GetComponent<Image>().sprite = achievementsUnselected;
            home.GetComponent<Image>().sprite = homeUnselected;
        }
        #endregion
    }
}