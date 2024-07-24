using Farmanji.Auth;
using Farmanji.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.General
{
    public class LogOutButton : MonoBehaviour
    {
        private Button backButton;
        // Start is called before the first frame update
        void Start()
        {
            backButton = GetComponent<Button>();
            backButton.onClick.AddListener(LogOut);

        }

        private void LogOut()
        {
            SessionManager.Instance.LogOut();
        }
    }

}