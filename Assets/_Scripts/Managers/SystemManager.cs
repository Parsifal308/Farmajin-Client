using System.Collections;
using Farmanji.Game;
using UnityEngine;

namespace Farmanji.Managers
{
    public class SystemManager : SingletonPersistent<SystemManager>
    {
        #region FIELDS

        [Header("SYSTEMS TAB:")] 
        [SerializeField] private SystemsTab _systemsTabPrefab;
        private SystemsTab _systemsTabInstance;
        #endregion

        public void ShowErrorMessage(ErrorType _errorType)
        {
            if (!_systemsTabInstance)
            {
                _systemsTabInstance = Instantiate(_systemsTabPrefab).SetErrorType(_errorType);
            }
            else
            {
                _systemsTabInstance.SetErrorType(_errorType);
                _systemsTabInstance.gameObject.SetActive(true);
            }
        }

        private void Start()
        {
            //StartCoroutine(ErrorTestCorroutine());
        }

        private IEnumerator ErrorTestCorroutine()
        {
            yield return new WaitForSeconds(5f);
            ShowErrorMessage(ErrorType.Game);
            yield return new WaitForSeconds(5f);
            ShowErrorMessage(ErrorType.Chat);
            yield return new WaitForSeconds(5f);
            ShowErrorMessage(ErrorType.Login);
        }
    }
}

public enum ErrorType
{
    Login,
    Chat,
    Game,
}