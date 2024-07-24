using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class ColorSelectorButton : MonoBehaviour
    {
        [SerializeField] private CustomizationColorSelector CustomizationColorSelector;
        [SerializeField] private Color ColorToSet;
        [SerializeField] private Image _colorBackgroundImage;
        private Button button;

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(ColorButtonPressed);
            if (CustomizationColorSelector == null) GetComponentInParent<CustomizationColorSelector>();
            if (_colorBackgroundImage == null) _colorBackgroundImage = GetComponentInChildren<Image>();
            if (_colorBackgroundImage) _colorBackgroundImage.color = ColorToSet;
        }

        private void ColorButtonPressed()
        {
            if (CustomizationColorSelector == null) return;
            CustomizationColorSelector.TintAvatarPiece(ColorToSet);
        }
    }
}