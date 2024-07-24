using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] private ScrollRect _inventoryPanelScrollRect;
        [SerializeField] private Transform _inventoryPanelContent;
        [SerializeField] private InventoryButton _inventoryButtonPrefab;
        [SerializeField] private int _inventoryPanelCapacity = 20;
        [SerializeField] private int _minimumCapacityForScrollRect = 12;
        private int _currentInventoryPanelCapacity;

        private void Awake()
        {
            if (_inventoryPanelScrollRect != null && _inventoryPanelScrollRect.enabled) _inventoryPanelScrollRect.enabled = false;
        }

        public void CreateInventoryButton(Sprite PreviewSprite)
        {
            if (_inventoryPanelContent == null || _inventoryButtonPrefab == null || PreviewSprite == null || _currentInventoryPanelCapacity >= _inventoryPanelCapacity)
                return;
            
            Instantiate(_inventoryButtonPrefab, _inventoryPanelContent).SetPreviewImage(PreviewSprite);
            _currentInventoryPanelCapacity++;
            CheckScrollRect();
        }

        private void CheckScrollRect()
        {
            if (_inventoryPanelScrollRect == null || _inventoryPanelContent == null) return;
            
            if (!_inventoryPanelScrollRect.enabled && _inventoryPanelContent.childCount >= _minimumCapacityForScrollRect)
                _inventoryPanelScrollRect.enabled = true;
        }
    }
}