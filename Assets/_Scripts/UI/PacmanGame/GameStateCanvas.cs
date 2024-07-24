
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using Farmanji.Managers;

namespace Farmanji.Pacman
{
    public class GameStateCanvas : MonoBehaviour
    {
        #region FIELDS
        UnityAction StartGameAction, RepeatGameAction, ExitGameAction, ContinueGameAction;
        private Animator animator;
        [SerializeField] private Image pacmanImage;
        [Header("TEXTS:")]
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI msgText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI recordScoreText;
        [SerializeField] private TextMeshProUGUI gemsRewardText;
        [SerializeField] private TextMeshProUGUI gemsExtraRewardText;
        [SerializeField] private TextMeshProUGUI coinsRewardText;
        [SerializeField] private TextMeshProUGUI playerLifesText;
        [SerializeField] private TextMeshProUGUI playerMaxLifesText;
        [SerializeField] private TextMeshProUGUI acountCoinsText;
        [SerializeField] private TextMeshProUGUI acountGemsText;
        [Header("BUTTONS:")]
        [SerializeField] private Button playButton;
        [SerializeField] private Button repeatButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button continueButton;
        [Header("CONTAINERS:")]
        [SerializeField] private RectTransform starsPanel;
        [SerializeField] private RectTransform rewardsPanel;
        [SerializeField] private RectTransform scorePanel;
        [SerializeField] private RectTransform pacmanImagePanel;
        #endregion

        #region PROPERTIES

        public TextMeshProUGUI GemsExtraRewards { get { return gemsExtraRewardText; } }
        public TextMeshProUGUI PlayerLifes { get { return playerLifesText; } }
        public TextMeshProUGUI LevelText { get { return levelText; } }
        public TextMeshProUGUI MsgText { get { return msgText; } }
        public TextMeshProUGUI ScoreText { get { return scoreText; } }
        public TextMeshProUGUI RecordScoreText { get { return recordScoreText; } }
        public TextMeshProUGUI GemsRewardText { get { return gemsRewardText; } }
        public TextMeshProUGUI CoinsRewardText { get { return coinsRewardText; } }
        public Image PacmanImage { get { return pacmanImage; } }
        #endregion

        #region UNITY METHODS
        void Awake()
        {
            animator = GetComponent<Animator>();
            SubscribeEvents();

        }
        void Start()
        {
            gemsRewardText.text = ResourcesLoaderManager.Instance.Worlds.CurrentActivity.GemsReward.ToString();
            //gemsRewardText.text = PacmanGameManager.Instance.GemRewards.ToString();
            //coinsRewardText.text = PacmanGameManager.Instance.CoinRewads.ToString();
            coinsRewardText.text = ResourcesLoaderManager.Instance.Worlds.CurrentActivity.CoinsReward.ToString();
            playerLifesText.text = PacmanGameManager.Instance.PlayerLifes.ToString();
            playerMaxLifesText.text = "/" + PacmanGameManager.Instance.PlayerMaxLifes.ToString();
            acountCoinsText.text = CurrencyManager.Instance.GetCoins.ToString();
            acountGemsText.text = CurrencyManager.Instance.GetGems.ToString();
            
        }
        #endregion

        #region PRIVATE METHODS
        private void SubscribeEvents()
        {
            StartGameAction = () =>
            {
                PacmanGameManager.Instance.StartGameDelayed();
                animator.SetTrigger("FadeOut");
                SoundManager.Instance.SetMusicVolume(0.5f);
            };
            RepeatGameAction = () =>
            {
                PacmanGameManager.Instance.PauseGame();
                PacmanGameManager.Instance.ResetEntities();
                PacmanGameManager.Instance.ResetAllDots();
                PacmanGameManager.Instance.InitializeGemsDots();
                PacmanGameManager.Instance.InitializeQuizesDots();
                PacmanGameManager.Instance.ResetTimer();
                animator.SetTrigger("FadeOut");
            };
            ExitGameAction = () =>
            {
                //VOLVER A ESCENA HOME
            };
            ContinueGameAction = () =>
            {
                //Abrir siguiente nivel??
            };

            playButton.onClick.AddListener(StartGameAction);
            repeatButton.onClick.AddListener(RepeatGameAction);
            repeatButton.onClick.AddListener(StartGameAction);
            exitButton.onClick.AddListener(ExitGameAction);
            //continueButton.onClick.AddListener(ContinueGameAction);
        }
        public void SetStart()
        {
            animator.SetTrigger("Start");
            // rewardsPanel.gameObject.SetActive(true);
            // scorePanel.gameObject.SetActive(true);
            // starsPanel.gameObject.SetActive(false);
            // pacmanImagePanel.gameObject.SetActive(false);

            // playButton.enabled = true;
            // repeatButton.enabled = false;
            // exitButton.enabled = true;
            // continueButton.enabled = false;
        }
        private void SetVictory()
        {
            animator.SetTrigger("Win");
            // rewardsPanel.gameObject.SetActive(true);
            // scorePanel.gameObject.SetActive(true);
            // starsPanel.gameObject.SetActive(true);
            // pacmanImagePanel.gameObject.SetActive(false);

            // playButton.enabled = false;
            // repeatButton.enabled = true;
            // exitButton.enabled = true;
            // continueButton.enabled = true;
        }
        private void SetDefeat()
        {
            animator.SetTrigger("Lose");
            // rewardsPanel.gameObject.SetActive(false);
            // scorePanel.gameObject.SetActive(true);
            // starsPanel.gameObject.SetActive(false);
            // pacmanImagePanel.gameObject.SetActive(true);

            // playButton.enabled = false;
            // repeatButton.enabled = true;
            // exitButton.enabled = true;
            // continueButton.enabled = false;
        }
        #endregion

        #region PUBLIC METHODS
        public void UpdateLifes()
        {
            playerLifesText.text = PacmanGameManager.Instance.PlayerLifes.ToString();
        }
        #endregion
    }
}