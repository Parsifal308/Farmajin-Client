using Farmanji.Data;
using System.Collections;
using UnityEngine;
using System;
using System.Linq;
using Farmaji.Data;
using Farmanji.Auth;

namespace Farmanji.Managers
{
    public class ResourcesLoaderManager : SingletonPersistent<ResourcesLoaderManager>
    {
        #region FIELDS

        [SerializeField] private Login _login;
        [HideInInspector] private WorldsLoader worlds;
        [HideInInspector] private LevelsLoader levels;
        [HideInInspector] private VideosLoader videos;
        [HideInInspector] private GamesLoader games;
        [HideInInspector] private AchievementsLoader achievements;
        [HideInInspector] private ItemsLoader items;
        [HideInInspector] private ProductsLoader products;
        [HideInInspector] private EconomyLoader economy;
        [HideInInspector] private UserProgressLoader userProgress;
        [HideInInspector] private FriendsLoader friends;
        [HideInInspector] private MessagesLoader messages;
        [HideInInspector] private ConversationsLoader conversations;
        [HideInInspector] private UserAchievementsLoader userAchievements;
        [HideInInspector] private ChallengesLoader challenges;
        [HideInInspector] private ActivitiesLoader activities;

        public Action OnLoadingAll;

        public Action OnFinishLoad;
        #endregion

        #region PROPERTIES
        public ActivitiesLoader Activities { get { return activities; } }
        public ChallengesLoader Challenges { get { return challenges; } }
        public UserAchievementsLoader UserAchievements { get { return userAchievements; } }
        public WorldsLoader Worlds { get { return worlds; } }
        public LevelsLoader Levels { get { return levels; } }
        public VideosLoader Videos { get { return videos; } }
        public GamesLoader Games { get { return games; } }
        public AchievementsLoader Achievements { get { return achievements; } }
        public ItemsLoader Items { get { return items; } }
        public EconomyLoader Economy { get { return economy; } }
        public UserProgressLoader UserProgress { get { return userProgress; } }
        public ProductsLoader Products { get { return products; } }
        public FriendsLoader Friends { get { return friends; } }
        public MessagesLoader Messages { get { return messages; } }
        public ConversationsLoader Conversations { get { return conversations; } }
        #endregion

        #region METHODS
        private void OnEnable()
        {
            worlds = GetComponentInChildren<WorldsLoader>();
            levels = GetComponentInChildren<LevelsLoader>();
            videos = GetComponentInChildren<VideosLoader>();
            games = GetComponentInChildren<GamesLoader>();
            achievements = GetComponentInChildren<AchievementsLoader>();
            items = GetComponentInChildren<ItemsLoader>();
            products = GetComponentInChildren<ProductsLoader>();
            economy = GetComponentInChildren<EconomyLoader>();
            userProgress = GetComponentInChildren<UserProgressLoader>();
            friends = GetComponentInChildren<FriendsLoader>();
            messages = GetComponentInChildren<MessagesLoader>();
            conversations = GetComponentInChildren<ConversationsLoader>();
            userAchievements = GetComponentInChildren<UserAchievementsLoader>();
            challenges = GetComponentInChildren<ChallengesLoader>();
            activities = GetComponentInChildren<ActivitiesLoader>();
        }


        public IEnumerator LoadAll()
        {
            OnLoadingAll?.Invoke();
            yield return StartCoroutine(worlds.Load());
            foreach (var WorldData in worlds.List)
            {
                yield return StartCoroutine(levels.LoadLevelsAtWorld(WorldData.id));

                var WorldProgress = levels.List.Sum(level => level.LevelProgress);
                WorldData.WorldProgress = WorldProgress;

                worlds.AddLevelToWorld(WorldData.id, levels.List);
            }

            BindCompleteEventsPost();
            worlds.CreateWorldsLevelsData();

            levels.List.Clear();

            Debug.Log("INTENTADO CARGAR ACTIVIDADES...");
            yield return StartCoroutine(activities.Load());
            Debug.Log("... FINALIZADO CARGAR ACTIVIDADES");
            yield return StartCoroutine(friends.Load());
            yield return StartCoroutine(achievements.Load());
            yield return StartCoroutine(conversations.Load());
            yield return StartCoroutine(items.Load());
            yield return StartCoroutine(products.Load());

            yield return StartCoroutine(userAchievements.Load());
            yield return StartCoroutine(economy.Load());
            yield return StartCoroutine(userProgress.Load());
            yield return StartCoroutine(challenges.Load());

        }


        private void BindCompleteEventsPost()
        {
            if (_login.isFirstLogin)
            {
                var body = ActivityProgresBody.Create(Instance.Worlds.List[0].id, Instance.Worlds.List[0].levels[0].Id, "", "", false, "level", 0);
                StartCoroutine(Instance.Worlds.GetComponent<ActivityProgresPost>().Post(body));
                _login.isFirstLogin = false;
            }


            foreach (var world in Instance.Worlds.List)
            {
                string worldWorldId = world.id;
                Action completeWorld = () =>
                {
                    Debug.Log("INTENTANDO COMPLETAR WORLD");
                    ActivityProgresBody body = ActivityProgresBody.Create(worldWorldId, "", "", "", true, "world", 1f);
                    StartCoroutine(Instance.Worlds.GetComponent<ActivityProgresPost>().Post(body));
                };
                world.OnWorldCompleted += completeWorld;
                LevelData previousLevel = null;
                foreach (var level in world.levels)
                {
                    string levelWorldId = world.id;
                    string levelLevelId = level.Id;
                    Action completeLevel = () =>
                    {
                        Debug.Log("INTENTANDO COMPLETAR LEVEL");
                        ActivityProgresBody body = ActivityProgresBody.Create(levelWorldId, levelLevelId, "", "", true, "level", 1f);
                        StartCoroutine(Instance.Worlds.GetComponent<ActivityProgresPost>().Post(body));
                    };

                    Action initializeNextLevel = () =>
                    {
                        if (previousLevel != null)
                        {
                            ActivityProgresBody body2 = ActivityProgresBody.Create(levelWorldId, levelLevelId, "", "", false, "level", 0f);
                            StartCoroutine(Instance.Worlds.GetComponent<ActivityProgresPost>().Post(body2));
                        }
                    };
                    if (previousLevel != null) previousLevel.OnLevelCompleted += initializeNextLevel;
                    previousLevel = level;

                    level.OnLevelCompleted += completeLevel;

                    foreach (var stage in level.stages)
                    {
                        string _worldId = world.id;
                        string levelId = level.Id;
                        string stageId = stage.Id;
                        Action completeStage = () =>
                        {
                            Debug.Log("INTENTANDO COMPLETAR STAGE");
                            ActivityProgresBody body = ActivityProgresBody.Create(_worldId, levelId, stageId, "", true, "stage", 1f);
                            StartCoroutine(Instance.Worlds.GetComponent<ActivityProgresPost>().Post(body));
                        };
                        stage.OnStageCompleted += completeStage;
                        foreach (var activity in stage.Activities)
                        {
                            if (activity.Completed) continue;
                            activity.OnActivityCompleted += stage.UpdateStageProgress;
                            activity.OnActivityCompleted += level.UpdateLevelProgress;
                            activity.OnActivityCompleted += world.UpdateWorldProgress;
                            Action completeActivity = () =>
                            {
                                Debug.Log("INTENTANDO COMPLETAR ACTIVITY");
                                ActivityProgresBody body = ActivityProgresBody.Create(_worldId, levelId, stageId, activity.Id, true, "activity", ResourcesLoaderManager.Instance.Worlds.CurrentActivity.ProgressPercent);
                                StartCoroutine(Instance.Worlds.GetComponent<ActivityProgresPost>().Post(body));
                            };
                            activity.OnActivityCompleted += completeActivity;
                        }
                    }
                }
            }
        }
        #endregion
    }
}