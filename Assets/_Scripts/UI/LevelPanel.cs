using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Farmanji.Data;
using UnityEngine.Events;

namespace Farmanji.Game
{
    public class LevelPanel : MonoBehaviour
    {
        #region FIELDS
        [Header("LEVEL INFO:")]
        [SerializeField] private TextMeshProUGUI world;
        [SerializeField] private TextMeshProUGUI levelValue;
        [SerializeField] private TextMeshProUGUI levelName;
        
        private List<GameObject> stagesButtons = new List<GameObject>();

        [Header("LEVEL ART:")]
        [Space(15), SerializeField] private Image background;
        [SerializeField] private Image platform;

        [Header("STAGES:")]
        [Space(15), SerializeField] private RectTransform stagesContentPanel;
        [Space(5), SerializeField] private GameObject stageButtonPrefab;
        
        [Header("DISABLED:")]
        [SerializeField] private Image _levelDisabledImage;
        [SerializeField] private bool _disabled = true;

        #endregion

        #region UNITY METHODS
        // private void OnEnable()
        // {
        //     ResourcesLoaderManager.Instance.OnFinishLoad += UpdateWorldList;
        // }
        #endregion

        #region PUBLIC METHODS
        public void UpdateLevel(LevelData data, Sprite platform)
        {
            //world.text = data.World;
            levelName.text = data.Name;
            background.sprite = data.BackgroundImage;
            this.platform.sprite = platform;
            InitializeStages(data);
            UpdateStages(data);
            data.UpdateLevelProgress();
            if (!_disabled && _levelDisabledImage) _levelDisabledImage.gameObject.SetActive(false);
        }

        #endregion

        #region PRIVATE METHODS
        private UnityAction GoToStageAction()
        {
            UnityAction goToStagesAction = () => { TabsManager.Instance.ChangeTab(TabsManager.Instance.Stages); };
            return goToStagesAction;
        }
        private void InitializeStages(LevelData data)
        {
            CleanStages();
            for (int i = 0; i < data.stages.Count; i++)
            {
                int indexParam = i;
                UnityAction loadStage = () => { TabsManager.Instance.Stages.GetComponent<StagesTab>().InitializeStage(data.stages[indexParam]); };

                stagesButtons.Add(GameObject.Instantiate(stageButtonPrefab, stagesContentPanel.transform));
                stagesButtons[i].GetComponent<Button>().onClick.AddListener(loadStage);
                stagesButtons[i].GetComponent<Button>().onClick.AddListener(GoToStageAction());
            }
        }
        private void CleanStages()
        {
            for (int i = 0; i < stagesContentPanel.childCount; i++)
            {
                Destroy(stagesContentPanel.GetChild(i).gameObject);
            }
            stagesButtons.Clear();
        }
        private void UpdateStages(LevelData data)
        {
            for (int i = 0; i < stagesButtons.Count; i++)
            {
                stagesButtons[i].GetComponent<LevelStageSelector>().UpdateStage(data.stages[i]);
            }
        }

        public bool IsDisabled()
        {
            return _disabled;
        }

        public void UnlockLevel()
        {
            _disabled = false;
        }
        #endregion

    }
}