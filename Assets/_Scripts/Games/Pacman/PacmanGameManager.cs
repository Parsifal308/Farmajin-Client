using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Farmanji.Data;
using Farmanji.Game;
using Farmanji.Managers;
using Pathfinding;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Farmanji.Pacman
{
    public class PacmanGameManager : Singleton<PacmanGameManager>
    {
        #region FIELDS
        UnityAction OnEatenAction;
        private bool isTimerGoing;
        private AudioSource audioSource;
        [Header("REWARDS:")]
        [SerializeField] private int coinsReward;
        [SerializeField] private int gemsReward;
        [SerializeField] private int coinsExtraReward = 0;
        [SerializeField] private int gemsExtraReward = 0;
        [Header("UI:")]
        [SerializeField] private CanvasGroup loadingScreenCanvas;
        [SerializeField] private CanvasGroup infoCanvas;
        [SerializeField] private CanvasGroup trailCanvas;
        [SerializeField] private CanvasGroup quizCanvas;
        [SerializeField] private CanvasGroup gameStateCanvas;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button muteButton;
        [SerializeField] private Button unMuteButton;
        [Header("DATA:")]
        [SerializeField] private PacDotData dotsData;
        [SerializeField] private PacDotData powerUpData;
        [SerializeField] private PacDotData quizDotData;
        [SerializeField] private PacDotData gemDotData;

        [Header("SETTINGS:")]
        [SerializeField] private Vector3 playerInitialPosition;
        [SerializeField] private Vector3 playerInitialRotation;
        [SerializeField] private int playerMaxLifes = 5;
        [SerializeField] private int playerCurrentLifes;

        [Space(10), SerializeField] private int dotsAmount;
        [SerializeField] private int dotsEated;
        [Space(10), SerializeField] private int gemsAmount;
        [SerializeField] private int gemsEated;
        [Space(10), SerializeField] private int quizesAmount;
        [SerializeField] private int quizesEated;
        [Space(10), SerializeField] private int powerUpsAmount;
        [SerializeField] private int powerUpsEated;
        [Space(10), SerializeField] private List<GhostInstance> ghosts;

        [Header("ROOTS TRANSFORMS:")]
        [SerializeField] private Transform regularDotsRoot;
        [SerializeField] private Transform powerDotsRoot;
        [SerializeField] private Transform GemsDotsRoot;
        [SerializeField] private Transform QuizesDotsRoot;

        [Header("DOTS:")]
        [SerializeField] private List<PacmanDot> regularDots;
        [SerializeField] private List<PacmanDot> powerDots;
        [SerializeField] private List<PacmanDot> GainLife;
        [SerializeField] private List<PacmanDot> gemsDots;
        [SerializeField] private List<PacmanDot> quizesDots;

        [Header("MUSIC & SFX:")]
        [SerializeField] private AudioClip mainTheme;
        [SerializeField] private AudioClip win;
        [SerializeField] private AudioClip lose;
        [SerializeField] private AudioClip correctAswerSFX;
        [SerializeField] private AudioClip wrongAswerSFX;
        [SerializeField] private AudioClip powerUpSFX;
        [SerializeField] private AudioClip debuffSFX;

        [Header("GAME STATE:")]
        [SerializeField] private float timer;
        [SerializeField] private float points;
        [SerializeField] private UnityEvent OnGameStart, OnGamePause, OnGameWon, OnGameLose;
        [SerializeField] private GameState gameState = GameState.Starting;
        [Header("LEVEL SECTIONS:")]
        [SerializeField] private List<PacmanSection> levelSections;



        private GameObject player;
        #endregion

        #region PROPERTIES

        public int PlayerLifes { get { return playerCurrentLifes; } }
        public int PlayerMaxLifes { get { return playerMaxLifes; } }
        public int GemRewards { get { return gemsReward; } }
        public int CoinRewads { get { return coinsReward; } }
        public int CoinsExtraRewards { get { return coinsExtraReward; } set { coinsExtraReward = value; } }
        public int GemsExtraRewards { get { return gemsExtraReward; } set { gemsExtraReward = value; } }
        public List<PacmanSection> LevelSections { get { return levelSections; } }
        public CanvasGroup InfoCanvas { get { return infoCanvas; } }
        public CanvasGroup LoadingScreenCanvas { get { return loadingScreenCanvas; } }
        public CanvasGroup TrailCanvas { get { return trailCanvas; } }
        public CanvasGroup QuizCanvas { get { return quizCanvas; } }
        public CanvasGroup GameStateCanvas { get { return gameStateCanvas; } }
        public int DotsAmount { get { return dotsAmount; } set { dotsAmount = value; } }
        public int DotsEated { get { return dotsEated; } set { dotsEated = value; } }
        public int GemsAmount { get { return gemsAmount; } set { gemsAmount = value; } }
        public int GemsEated { get { return gemsEated; } set { gemsEated = value; } }
        public int QuizesAmount { get { return quizesAmount; } set { quizesAmount = value; } }
        public int QuizesEated { get { return quizesEated; } set { quizesEated = value; } }
        public int PowerUpsAmount { get { return powerUpsAmount; } set { powerUpsAmount = value; } }
        public int PowerUpsEated { get { return powerUpsEated; } set { powerUpsEated = value; } }
        public GameObject Player { get { return player; } }
        public AudioClip MainTheme { get { return mainTheme; } }
        #endregion

        #region UNITY METHJODS
        void Awake()
        {
            base.Awake();
            Initialize();
            InitializeDots();
            InitializeGemsDots();
            InitializeQuizesDots();
            InitializeAudios();

            SubscribeSectionsEvents();
            SubscribeButtons();
        }
        private void Start()
        {
            player.GetComponent<PacmanPlayer>().SubscribeInputs();
            PauseGame();
            unMuteButton.gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            UnsubscribeSectionsEvents();
            UnsubscribeButtons();
        }
        #endregion

        #region PRIVATE METHODS
        private void SubscribeButtons()
        {
            UnityAction mute = () =>
            {
                SoundManager.Instance.MuteMusic();
                SoundManager.Instance.MuteSFX();
                muteButton.gameObject.SetActive(false);
                unMuteButton.gameObject.SetActive(true);
            };
            UnityAction Unmute = () =>
            {
                SoundManager.Instance.UnMuteMusic();
                SoundManager.Instance.UnMuteSFX();
                muteButton.gameObject.SetActive(true);
                unMuteButton.gameObject.SetActive(false);
            };
            muteButton.onClick.AddListener(mute);
            unMuteButton.onClick.AddListener(Unmute);
        }
        private void UnsubscribeButtons()
        {
            muteButton.onClick.RemoveAllListeners();
            unMuteButton.onClick.RemoveAllListeners();
        }
        public void InitializeGemsDots()
        {
            int random;
            for (int i = 0; i < gemsAmount; i++)
            {
                random = Random.Range(0, regularDots.Count - 1);
                gemsDots.Add(GameObject.Instantiate(gemDotData.Prefab, regularDots[random].transform.position, regularDots[random].transform.rotation, GemsDotsRoot).GetComponent<PacmanDot>());
                regularDots[random].gameObject.SetActive(false);
                //regularDots.RemoveAt(random);

                int points = gemsDots[i].Data.Points;
                UnityAction OnEatenAction = () => { EatDot(points); };
                gemsDots[i].OnEatenEvent.AddListener(OnEatenAction);
            }
        }
        public void InitializeQuizesDots()
        {
            int random;
            for (int i = 0; i < quizesAmount; i++)
            {
                random = Random.Range(0, regularDots.Count - 1);
                quizesDots.Add(GameObject.Instantiate(quizDotData.Prefab, regularDots[random].transform.position, regularDots[random].transform.rotation, QuizesDotsRoot).GetComponent<PacmanDot>());
                regularDots[random].gameObject.SetActive(false);
                //regularDots.RemoveAt(random);

                int points = quizesDots[i].Data.Points;
                UnityAction OnEatenAction = () => { EatDot(points); };
                quizesDots[i].OnEatenEvent.AddListener(OnEatenAction);
            }
        }
        private void InitializePowerDots()
        {

        }
        private void InitializeDots()
        {
            PacmanDot pacmanDot;
            foreach (Transform dot in regularDotsRoot)
            {
                dot.TryGetComponent<PacmanDot>(out pacmanDot);
                regularDots.Add(pacmanDot);
                int points = pacmanDot.Data.Points;
                UnityAction OnEatenAction = () => { EatDot(points); };
                pacmanDot.OnEatenEvent.AddListener(OnEatenAction);
            }
        }

        private void Initialize()
        {
            player = GameObject.Find("Pacman");
            audioSource = GetComponent<AudioSource>();

            UnityAction PlayerDeathAction = () => { PlayerEated(); };
            player.GetComponent<PacmanPlayer>().OnDieEvent.AddListener(PlayerDeathAction);
        }
        private void InitializeAudios()
        {
            UnityAction startMusicAction = () => { audioSource.clip = mainTheme; audioSource.Play(); };
            OnGameStart.AddListener(startMusicAction);

            UnityAction victorySFXAction = () => { audioSource.clip = win; audioSource.Play(); };
            OnGameWon.AddListener(victorySFXAction);

            UnityAction DefeatSFXAction = () => { audioSource.clip = lose; audioSource.Play(); };
            OnGameLose.AddListener(DefeatSFXAction);
        }
        public void CheckDotsEated()
        {
            if (dotsEated == regularDots.Count)
            {
                gameState = GameState.Victory;
                EndGame();
            }
            else
            {
                gameState = GameState.Playing;
            }
        }
        public void ResetEntities()
        {
            foreach (var ghost in ghosts)
            {
                ghost.Instance.transform.localPosition = ghost.InitialPosition;
                ghost.Instance.transform.GetChild(0).transform.rotation = Quaternion.Euler(90, 0, 0);
            }
            player.transform.localPosition = playerInitialPosition;
            player.GetComponent<PacmanPlayer>().ResetRotation();
        }
        public void ResetAllDots()
        {
            dotsEated = 0;
            playerCurrentLifes = playerMaxLifes;
            coinsExtraReward = 0;
            gemsExtraReward = 0;
            foreach (Transform dot in regularDotsRoot)
            {
                dot.gameObject.SetActive(true);
            }
            for (int i = 0; i < GemsDotsRoot.childCount; i++)
            {
                Destroy(GemsDotsRoot.GetChild(i).gameObject);
            }
            for (int i = 0; i < QuizesDotsRoot.childCount; i++)
            {
                Destroy(QuizesDotsRoot.GetChild(i).gameObject);
            }
            for (int i = 0; i < powerDotsRoot.childCount; i++)
            {
                Destroy(powerDotsRoot.GetChild(i).gameObject);
            }
            gemsDots.Clear();
            quizesDots.Clear();
            powerDots.Clear();
        }
        #endregion

        #region PUBLIC MEHTODS
        public void StartGameDelayed()
        {
            OnGameStart?.Invoke();
            Invoke("ContinueGame", mainTheme.length);
        }
        public void EatDot(int points)
        {
            dotsEated++;
            Debug.Log("dots eated: " + dotsEated);
            this.points += points;
            CheckDotsEated();
        }
        // public void StartGame()
        // {
        //     StartCoroutine(UpdateTimer(true));
        // }
        public void PauseGame()
        {
            player.GetComponent<PacmanPlayer>().CanPlay = false;
            StartCoroutine(UpdateTimer(false));
            player.GetComponent<PlayerInput>().enabled = false;
            player.GetComponent<CharacterController>().enabled = false;
            //player.GetComponentInChildren<Animator>().speed = 0f;
            //player.GetComponent<PacmanPlayer>().AudioSource.Stop();

            foreach (var ghost in ghosts)
            {
                PacmanGhost pacmanGhost = ghost.Instance.GetComponent<PacmanGhost>();
                pacmanGhost.AIPath.enabled = false;
                pacmanGhost.Animator.speed = 0f;
            }
        }
        public void ContinueGame()
        {
            StartCoroutine(UpdateTimer(true));
            player.GetComponent<PlayerInput>().enabled = true;
            player.GetComponent<CharacterController>().enabled = true;
            player.GetComponentInChildren<Animator>().speed = 1f;
            //player.GetComponent<PacmanPlayer>().AudioSource.Play();

            foreach (var ghost in ghosts)
            {
                PacmanGhost pacmanGhost = ghost.Instance.GetComponent<PacmanGhost>();
                pacmanGhost.AIPath.enabled = true;
                pacmanGhost.Animator.speed = 1f;
            }
            player.GetComponent<PacmanPlayer>().CanPlay = true;
        }
        public void EndGame()
        {
            player.GetComponent<PacmanPlayer>().CanPlay = false;
            StartCoroutine(UpdateTimer(false));
            PauseGame();
            SoundManager.Instance.MusicLoop(false);
            switch (gameState)
            {
                case GameState.Victory:
                    SoundManager.Instance.SetMusicVolume(1f);
                    SoundManager.Instance.SetMusic(win);
                    SoundManager.Instance.PlayMusic();
                    gameStateCanvas.GetComponent<Animator>().SetTrigger("Win");
                    OnGameWon?.Invoke();
                    // EconomyBody body = EconomyBody.CreateCurrenciesBody(coinsReward + coinsExtraReward, gemsReward + gemsExtraReward);
                    // StartCoroutine(ResourcesLoaderManager.Instance.Economy.GetComponent<EconomyPost>().Post(body));
                    ResourcesLoaderManager.Instance.Worlds.CurrentActivity.ProgressPercent = 1f;
                    ResourcesLoaderManager.Instance.Worlds.CurrentActivity.CompleteActivity();
                    ResourcesLoaderManager.Instance.Worlds.CompleteCurrentActivity(1f);
                    CurrencyManager.Instance.AddCoins(coinsReward + coinsExtraReward);
                    CurrencyManager.Instance.AddGems(gemsReward + gemsExtraReward);
                    gameStateCanvas.GetComponent<GameStateCanvas>().MsgText.text = "Nivel Superado";
                    break;
                case GameState.Defeat:
                    SoundManager.Instance.SetMusicVolume(1f);
                    SoundManager.Instance.SetMusic(lose);
                    SoundManager.Instance.PlayMusic();
                    gameStateCanvas.GetComponent<Animator>().SetTrigger("Lose");
                    gameStateCanvas.GetComponent<GameStateCanvas>().MsgText.text = "Nivel No Superado";
                    OnGameLose?.Invoke();
                    ResourcesLoaderManager.Instance.Worlds.CurrentActivity.ProgressPercent = (dotsEated+gemsEated+quizesEated)/(dotsAmount+gemsAmount+quizesAmount);
                    ResourcesLoaderManager.Instance.Worlds.CurrentActivity.CompleteActivity();
                    ResourcesLoaderManager.Instance.Worlds.CompleteCurrentActivity(ResourcesLoaderManager.Instance.Worlds.CurrentActivity.ProgressPercent);
                    break;
            }
        }
        public void SaveGameStats(float points, float time, int lifes)
        {

        }
        public void PlayerEated()
        {
            PauseGame();
            playerCurrentLifes--;
            gameStateCanvas.GetComponent<GameStateCanvas>().PlayerLifes.text = playerCurrentLifes.ToString();
            if (playerCurrentLifes <= 0)
            {
                gameState = GameState.Defeat;
                EndGame();
            }
            else
            {
                Invoke("ResetEntities", 2f);
                Invoke("ContinueGame", 4f);
            }
        }
        public void ResetTimer()
        {
            timer = 0f;
        }
        #endregion

        #region COROUTINES
        IEnumerator UpdateTimer(bool isRunning)
        {
            isTimerGoing = isRunning;
            while (isTimerGoing)
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }

        internal void FinishQuiz(object sender, System.EventArgs e)
        {
            if ((sender as AnswerButton).IsCorrect)
            {
                WeakAllGhost(13f, 1f);
                SoundManager.Instance.SetSFX(correctAswerSFX);
                SoundManager.Instance.PlaySFX();
            }
            else
            {
                SoundManager.Instance.SetSFX(wrongAswerSFX);
                SoundManager.Instance.PlaySFX();
                WeakPlayer(8f, 2f);
            }
            StartCoroutine(ContinueGameDelayed(3f));
        }
        private void WeakAllGhost(float duration, float speed)
        {
            foreach (var fantasmin in ghosts)
            {
                fantasmin.Instance.GetComponent<PacmanGhost>().Weaken(duration, speed);
            }
            SoundManager.Instance.SetSFX(powerUpSFX);
            SoundManager.Instance.PlaySFX();
        }
        private void WeakPlayer(float duration, float speed)
        {
            PacmanPlayer player = this.player.GetComponent<PacmanPlayer>();
            player.Weaken(duration, speed);
            SoundManager.Instance.SetSFX(debuffSFX);
            SoundManager.Instance.PlaySFX();
            player.Animator.SetTrigger("Power");
        }
        private void SubscribeSectionsEvents()
        {
            foreach (var section in levelSections)
            {
                section.OnGhostEnteredEvent += UpdateGhostSection;
                section.OnPacmanEnteredEvent += UpdatePacmanSection;
            }
        }
        private void UnsubscribeSectionsEvents()
        {
            foreach (var section in levelSections)
            {
                section.OnGhostEnteredEvent -= UpdateGhostSection;
                section.OnPacmanEnteredEvent -= UpdatePacmanSection;
            }
        }
        private void UpdatePacmanSection(object sender, System.EventArgs e)
        {
            player.GetComponent<PacmanPlayer>().CurrentSection = sender as PacmanSection;
        }

        private void UpdateGhostSection(object sender, System.EventArgs e)
        {
            (sender as PacmanSection).LastTriggered.GetComponent<PacmanGhost>().CurrentSection = sender as PacmanSection;
        }

        #endregion

        #region ENUMS
        public enum GameState
        {
            Starting,
            Playing,
            Victory,
            Defeat
        }
        #endregion

        #region SERIALIZABLE CLASSES
        [System.Serializable]
        class GhostInstance
        {
            [SerializeField] private GhostData data;
            [SerializeField] private GameObject instance;
            [SerializeField] private Vector3 initialPosition;
            public GhostData Data { get { return data; } }
            public GameObject Instance { get { return instance; } }
            public Vector3 InitialPosition { get { return initialPosition; } }
        }
        #endregion

        #region COROUTINEs
        IEnumerator ContinueGameDelayed(float time)
        {
            yield return new WaitForSeconds(time);
            ContinueGame();
        }
        #endregion
    }
}