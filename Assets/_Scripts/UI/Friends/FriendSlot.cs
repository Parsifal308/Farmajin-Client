using Farmanji.Auth;
using Farmanji.Data;
using Farmanji.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class FriendSlot : MonoBehaviour
    {
        #region FIELDS
        [Header("UserData:")]
        [SerializeField] private FriendData data;
        [Header("SLOT DATA")]
        [SerializeField] private Image _avatarImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _scoreValueText;

        [Header("BUTTONS")]
        [SerializeField] private Button _challengeButton;
        [SerializeField] private Button _mensagueButton;
        #endregion

        #region PROPERTIES
        public FriendData Data { get { return data; } }
        public Button MensagueButton { get { return _mensagueButton; } }
        #endregion

        #region UNITY METHODS
        void Start()
        {
            _challengeButton.onClick.AddListener(ChallengueButtonPressed);
            _mensagueButton.onClick.AddListener(MensagueButtonPressed);
        }
        #endregion

        #region PUBLIC METHODS
        public void InitializeSlot(FriendData data)
        {
            this.data = data;
            _nameText.text = this.data.Name;
        }
        #endregion

        #region PRIVATE METHODS
        private void ChallengueButtonPressed()
        {
            Debug.Log("ChallengueButtonPressed");
        }
        private void MensagueButtonPressed()
        {
            TabsManager.Instance.ChangeTab("ChatCanvas");
            TabsManager.Instance.HideCanvas(TabsManager.Instance.Navigation);
            TabsManager.Instance.HideCanvas(TabsManager.Instance.Information);

            //LOAD MESSAGES
            TabsManager.Instance.Chat.GetComponent<ChatTab>().LoadChat(SessionManager.Instance.UserData.UserInfo.Id, data.Id);
        }
        private void LoadMessages()
        {
            ResourcesLoaderManager.Instance.Messages.Load();

        }
        #endregion
    }
}