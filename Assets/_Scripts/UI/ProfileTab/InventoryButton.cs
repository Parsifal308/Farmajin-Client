using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class InventoryButton : MonoBehaviour
    {
        [SerializeField] private Image _previewImage;

        private void Start()
        {
            if (_previewImage == null) _previewImage.GetComponentInChildren<Image>();
        }

        public void SetPreviewImage(Sprite PreviewSprite)
        {
            if (_previewImage == null || PreviewSprite == null) return;
            _previewImage.sprite = PreviewSprite;
        }
    }
}