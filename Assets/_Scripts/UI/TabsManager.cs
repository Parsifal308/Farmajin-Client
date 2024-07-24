using System;
using System.Collections;
using System.Collections.Generic;
using Farmanji.Auth;
using Farmanji.Managers;
using Farmanji.Ws;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class TabsManager : Singleton<TabsManager>
    {
        #region FIELDS  
        private NavTab navTab;
        private InfoTab infoTab;
        private HomeTab homeTab;
        [Header("TEXTS:")]
        [SerializeField] private List<TextMeshProUGUI> namesList;
        [SerializeField] private List<TextMeshProUGUI> departamentoList;
        #region CANVASES
        private Canvas home;
        private Canvas profile;
        private Canvas achievements;
        private Canvas shop;
        private Canvas games;
        private Canvas worlds;
        private Canvas stages;
        private Canvas customization;
        private Canvas friends;
        private Canvas chat;
        //private Canvas systems;

        private Canvas navigation;
        private Canvas information;

        private Canvas currentActiveTab;
        private Canvas previousTab;
        private Canvas previousPreviousTab;
        private Canvas previousPreviousPreviousTab;
        #endregion
        [SerializeField] private UnityEvent OnCanvasChanged;
        #endregion

        #region PROPERTIES
        public Canvas Customization { get { return customization; } }
        public Canvas Profile { get { return profile; } }
        public Canvas Achievements { get { return achievements; } }
        public Canvas Friends { get { return friends; } }
        public Canvas Navigation { get { return navigation; } }
        public Canvas Information { get { return information; } }
        public Canvas Home { get { return home; } }
        //public Canvas Systems { get { return systems; } }
        public Canvas Stages { get { return stages; } }
        public Canvas Shop { get { return shop; } }
        public Canvas World { get { return worlds; } }
        public Canvas CurrentCanvas { get { return currentActiveTab; } }
        public UnityEvent OnCanvasChangedEvent { get { return OnCanvasChanged; } set { OnCanvasChanged = value; } }
        public Canvas Chat { get { return chat; } }
        #endregion

        #region UNITY METHODS
        void Awake()
        {
            base.Awake();
            InitializeScene();
        }

        void Start()
        {
            WebSocketMsgsHandler.Instance.ConnectToWebSocketServer();

            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            foreach (var text in namesList)
            {
                text.text = SessionManager.Instance.UserData._basicInfo.Name;
            }
            foreach (var depto in departamentoList)
            {
                depto.text = SessionManager.Instance.UserData._basicInfo.Role;
            }
        }
        #endregion


        #region PRIVATE METHODS
        #region SCENE INIT
        private void InitializeScene()
        {
            FindCanvases();
            currentActiveTab = home;
            previousTab = currentActiveTab;
            previousPreviousTab = currentActiveTab;
            previousPreviousPreviousTab = currentActiveTab;
            ShowCanvas(home);
            HideCanvas(profile);
            HideCanvas(achievements);
            HideCanvas(shop);
            HideCanvas(games);
            HideCanvas(worlds);
            HideCanvas(stages);
            HideCanvas(customization);
            HideCanvas(friends);
            HideCanvas(chat);
            ShowCanvas(navigation);
            ShowCanvas(information);
        }
        private void FindCanvases()
        {
            home = transform.GetChild(0).transform.Find("HomeCanvas").GetComponent<Canvas>();
            profile = transform.GetChild(0).transform.Find("ProfileCanvas").GetComponent<Canvas>();
            achievements = transform.GetChild(0).transform.Find("AchievementsCanvas").GetComponent<Canvas>();
            shop = transform.GetChild(0).transform.Find("ShopCanvas").GetComponent<Canvas>();
            games = transform.GetChild(0).transform.Find("GamesCanvas").GetComponent<Canvas>();
            worlds = transform.GetChild(0).transform.Find("WorldsCanvas").GetComponent<Canvas>();
            stages = transform.GetChild(0).transform.Find("StagesCanvas").GetComponent<Canvas>();
            customization = transform.GetChild(0).transform.Find("CustomizationCanvas").GetComponent<Canvas>();
            friends = transform.GetChild(0).transform.Find("FriendsCanvas").GetComponent<Canvas>();
            chat = transform.GetChild(0).transform.Find("ChatCanvas").GetComponent<Canvas>();
            navigation = transform.GetChild(0).transform.Find("NavigationCanvas").GetComponent<Canvas>();
            information = transform.GetChild(0).transform.Find("InformationCanvas").GetComponent<Canvas>();
            //systems = transform.GetChild(0).transform.Find("SystemsCanvas").GetComponent<Canvas>();
        }
        #endregion
        #region CANVAS CONTROL
        public void ShowCanvas(Canvas canvas)
        {
            canvas.gameObject.SetActive(true);
            CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

        }
        public void HideCanvas(Canvas canvas)
        {
            canvas.gameObject.SetActive(true);
            CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
        }
        #endregion

        #endregion

        #region PUBLIC METHODS
        public void ChangeTab(Canvas nextTab)
        {

            if (currentActiveTab != nextTab)
            {
                CanvasGroup nextTabCG = nextTab.GetComponent<CanvasGroup>();
                CanvasGroup currentTabCG = currentActiveTab.GetComponent<CanvasGroup>();
                currentTabCG.GetComponent<BaseTab>().Close(); //GetComponent<Animator>().SetTrigger("Hide");
                //currentTabCG.alpha = 0f;
                currentTabCG.blocksRaycasts = false;
                nextTabCG.GetComponent<BaseTab>().Open(); //GetComponent<Animator>().SetTrigger("Show");
                //nextTabCG.alpha = 1f;
                nextTabCG.blocksRaycasts = true;

                previousPreviousPreviousTab = previousPreviousTab;
                previousPreviousTab = previousTab;
                previousTab = currentActiveTab;

                currentActiveTab = nextTab;
                OnCanvasChanged?.Invoke();
            }
        }
        public void ChangeTab(string canvasName)
        {
            Canvas nextTab;
            if (GameObject.Find(canvasName).TryGetComponent<Canvas>(out nextTab))
            {
                if (currentActiveTab != nextTab)
                {
                    CanvasGroup nextTabCG = nextTab.GetComponent<CanvasGroup>();
                    CanvasGroup currentTabCG = currentActiveTab.GetComponent<CanvasGroup>();
                    currentTabCG.GetComponent<BaseTab>().Close();
                    currentTabCG.blocksRaycasts = false;
                    nextTabCG.GetComponent<BaseTab>().Open();
                    nextTabCG.blocksRaycasts = true;

                    previousPreviousPreviousTab = previousPreviousTab;
                    previousPreviousTab = previousTab;
                    previousTab = currentActiveTab;

                    currentActiveTab = nextTab;
                    OnCanvasChanged?.Invoke();
                }
            }
        }
        public void PreviousTab()
        {
            FriendsTab aux;
            if (currentActiveTab.TryGetComponent<FriendsTab>(out aux))
            {
                ChangeTab(previousPreviousPreviousTab);
                return;
            }
            ChangeTab(previousTab);
        }
        #endregion 
    }
}