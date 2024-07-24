using Farmanji.Managers;
using UnityEngine;

namespace Farmanji.Game
{
    public class CustomizationColorSelector : MonoBehaviour
    {
        [SerializeField] private AvatarPiece avatarPieceToTint;
        private AvatarManager _avatarManager;

        private void Start()
        {
            if (!_avatarManager) _avatarManager = AvatarManager.Instance;
        }

        public void TintAvatarPiece(Color TintColor)
        {
            if (!_avatarManager) return;
            _avatarManager.TintAvatarPiece(avatarPieceToTint, TintColor);
        }
    }
}