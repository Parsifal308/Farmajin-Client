using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class CategoriesButton : MonoBehaviour
    {
        [SerializeField] private Image IconImage;
        [SerializeField] private Image BorderImage;
        [SerializeField] private CategoriesSelectorPanel CategoriesSelectorPanel;
    
        public Button button;

        private void Start()
        {
            if (CategoriesSelectorPanel == null) CategoriesSelectorPanel.GetComponentInParent<CategoriesSelectorPanel>();
            if (button == null)button = GetComponent<Button>();
            if (button) button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (IconImage == null || BorderImage == null || CategoriesSelectorPanel == null) return;
            IconImage.color = Color.green;
            BorderImage.color = Color.green;
            CategoriesSelectorPanel.SetButtonPressed(this);
        }

        public void OnDeselect()
        {
            if (IconImage == null || BorderImage == null) return;
            IconImage.color = Color.white;
            BorderImage.color = Color.white;
        }
    }
}