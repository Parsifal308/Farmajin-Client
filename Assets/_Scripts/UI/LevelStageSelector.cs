using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Farmanji.Data;

namespace Farmanji.Game
{
    public class LevelStageSelector : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private TextMeshProUGUI stageProgressText;
        [SerializeField] private Image progressionBar;
        [SerializeField] private RectTransform separatorsRoot;
        [SerializeField] private GameObject separatorPrefab;

        #endregion

        #region UNITY METHODS
        #endregion

        #region PUBLIC MEHOTDS
        public void UpdateStage(StageData data)
        {
            if (data.Activities.Count > 0)
            {
                stageProgressText.text = GetCompletedStages(data) + "/" + data.Activities.Count;
                InitSeparators(data);
                progressionBar.fillAmount = data.GetStageProgress() / 100;
            }
            else
            {
                GetComponent<Button>().interactable = false;
                stageProgressText.text = "N/D";
                progressionBar.fillAmount = 0;
            }
        }
        #endregion

        #region PRIVATE METHODS
        private int GetCompletedStages(StageData data)
        {
            try
            {
                return data.Activities.Count(activity => activity.Completed);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        private void InitSeparators(StageData data)
        {
            if (data.Activities.Count > 0)
            {
                int separation = 360 / data.Activities.Count;
                for (int i = 0; i < data.Activities.Count; i++)
                {
                    GameObject separator = GameObject.Instantiate(separatorPrefab, separatorsRoot.transform);
                    separator.GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, separation * i);
                }
            }
        }

        #endregion
    }
}