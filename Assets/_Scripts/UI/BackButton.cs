using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class BackButton : MonoBehaviour
    {
        #region UNITY MEHTODS
        void Start()
        {
            UnityAction backAction = () => { TabsManager.Instance.PreviousTab(); };
            GetComponent<Button>().onClick.AddListener(backAction);
        }
        #endregion
    }
}