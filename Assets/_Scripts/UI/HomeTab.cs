using System.Collections;
using System.Collections.Generic;
using Farmanji.Managers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Farmanji.Data;
using UnityEngine.Events;
using System;
using Farmanji.UI;

namespace Farmanji.Game
{
    public class HomeTab : BaseTab
    {
        #region FIELDS
        [SerializeField] private Image platformImage;
        [SerializeField] private Image faceImage;
        [SerializeField] private Image hairImage;
        [SerializeField] private Image headImage;
        [SerializeField] private Image legsImage;
        [SerializeField] private Image petImage;


        [Header("PROGRESS:")]
        [Space(20), SerializeField] private TextMeshProUGUI levelText;
        [Space(20), SerializeField] private WorldProgressBar worldProgressBar;

        [Header("WORLDS:")]
        [Space(20), SerializeField] private Button currentWorldButton;
        [Space(5), SerializeField] private GameObject worldsList;

        [Header("PREFABS:")]
        [Space(20), SerializeField] private GameObject worldButton;
        [SerializeField] private GameObject worldButtonsSeparator;
        [Header("CHALLENGES:")]
        [Space(20), SerializeField] private GameObject challengesPanel;
        [SerializeField] private GameObject challengesList;
        [Space(10), SerializeField] private GameObject challengesPrefab;

        [Header("MESSAGES:")]
        [Space(20), SerializeField] private GameObject messagesPanel;
        [SerializeField] private GameObject messagesList;
        [Space(10), SerializeField] private GameObject messagesPrefab;
        [Header("GAME:")]
        [Space(20), SerializeField] private Button adventure;
        [SerializeField] private Button games;

        [Header("Panels")]
        [SerializeField] private CanvasGroup informationBlurredPanel;
        [SerializeField] private NotificationPanel notificationPanel;
        #endregion

        #region PROPERTIES
        public NotificationPanel NotificationPanel { get { return notificationPanel; } }
        public CanvasGroup InformationBlurredPanel { get { return informationBlurredPanel; } }
        public Button Adventure { get { return adventure; } set { adventure = value; } }
        public Button Games { get { return games; } set { games = value; } }
        #endregion

        #region UNITY METHODs
        private void OnEnable()
        {

        }

        private void OnDestroy()
        {
            ResourcesLoaderManager.Instance.OnFinishLoad -= UpdateWorldList;
            ResourcesLoaderManager.Instance.OnFinishLoad -= InitializeWorld;
        }

        void Start()
        {
            UpdateCustomization();
            UpdateProgressInfo();

            ResourcesLoaderManager.Instance.OnFinishLoad += UpdateWorldList;
            ResourcesLoaderManager.Instance.OnFinishLoad += InitializeWorld;
            //currentWorldButton.onClick.AddListener(UpdateWorldList);
        }
        #endregion

        #region PUBLIC MEHTODS
        public void DisableButtons()
        {
            adventure.interactable = false;
            games.interactable = false;
        }
        public void EnableButtons()
        {
            adventure.interactable = true;
            games.interactable = true;
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

        public void UpdateProgressInfo()
        {
            //level.text = ResourcesLoaderManager.Instance
            //currentWorldButton.GetComponentInChildren<Image>().sprite = ResourcesLoaderManager.Instance
            //currentWorldButton.GetComponentInChildren<TextMeshProUGUI>().text = ResourcesLoaderManager.Instance
        }
        private void UpdateWorldList()
        {
            List<WorldData> data = ResourcesLoaderManager.Instance.Worlds.List;
            for (int i = 0; i < data.Count; i++)
            {
                GameObject world = GameObject.Instantiate(this.worldButton, worldsList.transform);
                Button worldButton = world.GetComponent<Button>();
                world.GetComponentInChildren<TextMeshProUGUI>().text = data[i].name;

                int indexParam = i;
                UnityAction updateWorldAction = () => { UpdateWorld(data[indexParam]); };
                //UnityAction initWorldLevelsAction = () => { manager.World.GetComponent<WorldsTab>().InitializeLevels(data[indexParam]); };

                worldButton.onClick.AddListener(updateWorldAction);
                //worldButton.onClick.AddListener(initWorldLevelsAction);
                if (i < data.Count - 1) GameObject.Instantiate(this.worldButtonsSeparator, worldsList.transform);

                data[i].OnWorldProgressUpdated += worldProgressBar.SetWorldProgress;

                var worldIndex = i;
                void UpdateStages(float x)
                {
                    TabsManager.Instance.World.GetComponent<WorldsTab>().UpdateLevels(data[worldIndex]);
                }

                data[i].OnWorldProgressUpdated += UpdateStages;
            }
        }
        private void UpdateWorld(WorldData data)
        {
            //print("UpdatedWorld");
            if (data.background != null)
            {
                backgroundImage.sprite = data.background;
            }
            else
            {
                backgroundImage.sprite = ResourcesLoaderManager.Instance.Worlds.DefaultData.background;
            }
            if (data.platformImage != null)
            {
                SetImageAlpha(platformImage, 1f);
                platformImage.sprite = data.platformImage;
            }
            else
            {
                SetImageAlpha(platformImage, 0f);
            }
            currentWorldButton.GetComponentInChildren<TextMeshProUGUI>().text = data.name;
            levelText.text = data.level.ToString();
            TabsManager.Instance.World.GetComponent<WorldsTab>().InitializeLevels(data);
            worldProgressBar.SetWorldProgress(data.GetWorldProgress());
        }
        private void UpdateChallenges()
        {
            Debug.Log("Updating Challenges...");
            // List<ScriptableObject> challenges = ResourcesLoaderManager.Instance....;//DE DONDE SE CARGA LA GILADA
            // for (int i = 0; i < challenges.Count; i++)
            // {
            //     GameObject newButton = GameObject.Instantiate(challengesPrefab, challengesList.transform);
            //     newButton.transform.Find("ChallengeNameText").text = challenges[i]....;
            //     newButton.transform.Find("ChallengeDescriptionText").text = challenges[i]....;
            //     newButton.transform.Find("ChallengeStateImage").text = challenges[i]....;
            // }
        }
        private void UpdateUnreadMsg()
        {
            // Debug.Log("Updating New Messages...");
            // List<ScriptableObject> messages = ResourcesLoaderManager.Instance....;//DE DONDE SE CARGA LA GILADA
            // for (int i = 0; i < messages.Count; i++)
            // {
            //     GameObject newButton = GameObject.Instantiate(challengesPrefab, challengesList.transform);
            //     newButton.transform.Find("SenderProfileImage").GetComponent<Image>().sprite = null; //FOTO DE PERFIL DEL EMISOR
            //     newButton.transform.Find("MsjBackgroundImage").GetChild(0).GetComponent<TextMeshProUGUI>().text = "Nombre del emisor"; //FROM
            //     newButton.transform.Find("MsjBackgroundImage").GetChild(1).GetComponent<TextMeshProUGUI>().text = "Contenido del mensaje"; //MESSAGE CONTENT
            // }
        }
        private void SetImageAlpha(Image image, float alphaValue)
        {
            Color tmp = image.color;
            tmp.a = alphaValue;
            image.color = tmp;
        }
        private void InitializeWorld()
        {

            UpdateWorld(ResourcesLoaderManager.Instance.Worlds.List[0]);
        }
        #endregion
    }
}