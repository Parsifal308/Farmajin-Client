using System.Collections;
using System.Collections.Generic;
using Farmanji.Data;
using Farmanji.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class WorldsTab : BaseTab
    {
        #region FIELDS
        [SerializeField] private Transform levelsContentPanel;
        [SerializeField] private GameObject levelPanelPrefab;

        private int currentIndex;
        private List<GameObject> panels = new List<GameObject>();
        private GameObject currentPanelObj;
        private List<LevelPanel> levels = new List<LevelPanel>();

        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        #endregion

        #region UNITY METHODS
        #endregion

        #region PUBLIC METHODS
        public void NextButton()
        {
            currentPanelObj.GetComponent<Animator>().Play("OutByLeft");

            if (currentIndex + 1 == panels.Count)
            {
                
                //previousButton.gameObject.SetActive(false);
                currentIndex = 0;

            }
            else
            {
                currentIndex++;
            }
            currentPanelObj = panels[currentIndex];
            currentPanelObj.GetComponent<Animator>().Play("InByRight");

            if (currentIndex + 1 == panels.Count)
            {
                nextButton.gameObject.SetActive(false);
            }
            else
            {
                nextButton.gameObject.SetActive(true);
            }

            if (currentIndex - 1 == -1)
            {
                previousButton.gameObject.SetActive(false);
            }
            else
            {
                previousButton.gameObject.SetActive(true);
            }
        }
        public void BackButton()
        {
            currentPanelObj.GetComponent<Animator>().Play("OutByRight");
            if (currentIndex - 1 == -1)
            {
                currentIndex = panels.Count - 1;
            }
            else
            {
                currentIndex--;
            }
            currentPanelObj = panels[currentIndex];
            currentPanelObj.GetComponent<Animator>().Play("InByLeft");


            if (currentIndex + 1 == panels.Count)
            {
                nextButton.gameObject.SetActive(false);
            }
            else
            {
                nextButton.gameObject.SetActive(true);
            }

            if (currentIndex - 1 == -1)
            {
                previousButton.gameObject.SetActive(false);
            }
            else
            {
                previousButton.gameObject.SetActive(true);
            }

        }
        public void UpdateLevels(int worldIndex)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                levels[i].UpdateLevel(ResourcesLoaderManager.Instance.Worlds.List[worldIndex].levels[i], ResourcesLoaderManager.Instance.Worlds.List[worldIndex].platformImage);
            }
        }
        public void UpdateLevels(WorldData data)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                if (i == 0 && levels[i].IsDisabled()) levels[i].UnlockLevel(); //Desbloquear el primer nivel de cada mundo
                
                if (data.levels[i].LevelCompleted && i + 1 < levels.Count) levels[i + 1].UnlockLevel(); //Si el nivel ya esta completo desbloquear el siguiente
                levels[i].UpdateLevel(data.levels[i], data.platformImage);
            }
        }
        #endregion

        #region PRIVATE METHODS 
        public void InitializeLevels(WorldData data)
        {
            CleanLevels();
            //Debug.Log("-> World levels amount: " + data.levels.Count);
            if (data.levels.Count != 0)
            {
                TabsManager.Instance.Home.GetComponent<HomeTab>().EnableButtons();
                for (int i = 0; i < data.levels.Count; i++)
                {
                    panels.Add(GameObject.Instantiate(levelPanelPrefab, levelsContentPanel.transform));
                    panels[i].GetComponent<Animator>().Play("OutByLeft");
                    levels.Add(panels[i].GetComponent<LevelPanel>());
                }
                currentPanelObj = panels[0];
                currentPanelObj.GetComponent<Animator>().Play("Active");
                UpdateLevels(data);
                InitButtons(levels.Count);
                data.UpdateWorldProgress();

            }
            else
            {
                TabsManager.Instance.Home.GetComponent<HomeTab>().DisableButtons();
            }
        }
        private void InitButtons(int levelCount)
        {
            if (levelCount == 1)
            {
                nextButton.gameObject.SetActive(false);
                previousButton.gameObject.SetActive(false);
            }
            else
            {
                nextButton.gameObject.SetActive(true);
                previousButton.gameObject.SetActive(false);
            }
        }
        private void Initialize()
        {
            foreach (Transform child in levelsContentPanel)
            {
                panels.Add(child.gameObject);
                levels.Add(child.GetComponent<LevelPanel>());
            }
            currentPanelObj = panels[0];
            currentPanelObj.GetComponent<Animator>().Play("Active");
        }
        private void CleanLevels()
        {
            for (int i = 0; i < panels.Count; i++)
            {
                Destroy(panels[i].gameObject);
            }
            panels.Clear();
            levels.Clear();
        }
        #endregion
    }
}
