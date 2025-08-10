using Items;
using Managers.PanelManager.Panels;
using UnityEngine;

namespace Managers.PanelManager
{
    public class PanelManager : MonoBehaviour
    {
        private ItemInfoPanel _itemInfoPanelPrefab;
        private Canvas _canvas;

        private Canvas Canvas
        {
            get
            {
                if (_canvas == null)
                    _canvas = FindAnyObjectByType<Canvas>();
                return _canvas;
            }
        }
        private static PanelManager _instance;
        public static PanelManager Instance => _instance == null ? 
            _instance = FindAnyObjectByType<PanelManager>() : _instance;
        
        public void Init(ItemInfoPanel itemInfoPanelPrefab)
        {
            _itemInfoPanelPrefab = itemInfoPanelPrefab;
        }
        
        public void InstantiateItemInfoPanel(Item item)
        {
            var panel = Instantiate(_itemInfoPanelPrefab, Canvas.transform);
            panel.Render(item);
        }
    }
}