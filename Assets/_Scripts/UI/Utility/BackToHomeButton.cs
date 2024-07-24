using Farmanji.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Farmanji.General
{
    public class BackToHomeButton : MonoBehaviour
    {
        private Button backButton;
        // Start is called before the first frame update
        void Start()
        {
            backButton = GetComponent<Button>();
            backButton.onClick.AddListener(ReturnToHome);
        }
        private void ReturnToHome()
        {
            ScenesManager.Instance.ReturnToHome();
        }
    }

}
