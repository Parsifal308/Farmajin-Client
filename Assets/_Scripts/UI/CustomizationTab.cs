using System.Collections;
using Farmanji.Data;
using Farmanji.Managers;
using Farmanji.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class CustomizationTab : BaseTab
    {
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private PopUp _confirmationPopUp;

        private UserCustomizationPost _userCustomizationPost;
        private AvatarManager _avatarManager;
        
        private void Start()
        {
            if (_avatarManager == null) _avatarManager = AvatarManager.Instance;
            if (_saveButton != null && _avatarManager != null) _saveButton.onClick.AddListener(OnSaved);
            if (_quitButton != null && _avatarManager != null) _quitButton.onClick.AddListener(OnNotSaved);
            
            _avatarManager.LoadAvatarData();
        }
        
        private void OnSaved() // If we save we will popu confirmation
        {
            _avatarManager.SaveAvatarData();
            _confirmationPopUp.Open();
        }

        private void OnNotSaved() // If we didnt save we will lost the last changes 
        {
            StartCoroutine(ResetAvatarPiecesCoroutine());
        }

        private IEnumerator ResetAvatarPiecesCoroutine() // Last changes will be changed to the saved settings
        {
            yield return new WaitForSeconds(.35f);
            if (_avatarManager) _avatarManager.LoadAvatarData();
        }
    }
}