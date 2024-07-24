using Farmanji.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Farmanji.General
{
    public class ReloadSceneButton : MonoBehaviour
    {
        private Button reloadButton;
        // Start is called before the first frame update
        void Start()
        {
            reloadButton = GetComponent<Button>();
            reloadButton.onClick.AddListener(ReturnToHome);

        }

        private void ReturnToHome()
        {
            ScenesManager.Instance.ReloadScene();
        }
    }

}
