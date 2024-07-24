using UnityEngine;

namespace Farmanji.Game
{
    public class CategoriesSelectorPanel : MonoBehaviour
    {
        [Header("Button References")]
        [SerializeField] private CategoriesButton faceButton;
        [SerializeField] private CategoriesButton hairButton;
        [SerializeField] private CategoriesButton clothButton;
        [SerializeField] private CategoriesButton pantsButton;
        [SerializeField] private CategoriesButton accessoryButton;
        [SerializeField] private CategoriesButton petButton;
        [SerializeField] private CategoriesButton backgroundButton;
        
        private CategoriesButton lastButtonPressed;
        
        [Header("Color Selectors")] 
        [SerializeField] private ColorSelectorPanel ColorSelectorPanel;
        [SerializeField] private CustomizationColorSelector faceCustomizationColorSelector;
        [SerializeField] private CustomizationColorSelector hairCustomizationColorSelector;

        private void Start()
        {
            faceButton.button.onClick.AddListener(EnableFaceColorSelector);
            hairButton.button.onClick.AddListener(EnableHairColorSelector);
            clothButton.button.onClick.AddListener(DisableColorSelectors);
            pantsButton.button.onClick.AddListener(DisableColorSelectors);
            accessoryButton.button.onClick.AddListener(DisableColorSelectors);
            petButton.button.onClick.AddListener(DisableColorSelectors);
            backgroundButton.button.onClick.AddListener(DisableColorSelectors);
            faceButton.button.onClick.Invoke();
        }

        public void SetButtonPressed(CategoriesButton categoriesButton)
        {
            if (lastButtonPressed != null && lastButtonPressed != categoriesButton)
            {
                lastButtonPressed.OnDeselect();
                lastButtonPressed = categoriesButton;
            }
            else
            {
                lastButtonPressed = categoriesButton;
            }
        }

        private void DisableColorSelectors()
        {
            if (ColorSelectorPanel.gameObject.activeSelf) ColorSelectorPanel.gameObject.SetActive(false);
        }

        private void EnableFaceColorSelector()
        {
            if (!ColorSelectorPanel.gameObject.activeSelf) ColorSelectorPanel.gameObject.SetActive(true);
            if (!ColorSelectorPanel.GetFaceColorWindow.activeSelf) ColorSelectorPanel.EnableFaceColorWindow(true);
            DisableHairColorSelector();
        }

        private void EnableHairColorSelector()
        {
            if (!ColorSelectorPanel.gameObject.activeSelf) ColorSelectorPanel.gameObject.SetActive(true);
            if (!ColorSelectorPanel.GetHairColorWindow.activeSelf) ColorSelectorPanel.EnableHairColorWindow(true);
            DisableFaceColorSelector();
        }
        
        private void DisableFaceColorSelector()
        {
            if (ColorSelectorPanel.GetFaceColorWindow.activeSelf) ColorSelectorPanel.EnableFaceColorWindow(false);
        }
        
        private void DisableHairColorSelector()
        {
            if (ColorSelectorPanel.GetFaceColorWindow.activeSelf) ColorSelectorPanel.EnableHairColorWindow(false);
        }
    }
}