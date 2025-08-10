using System.Collections;
using System.Collections.Generic;
using BattleScene.Backpack;
using Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace BattleScene
{
    public class BattleSceneView : MonoBehaviour
    {
        [Header("Backpack")]
        [SerializeField] private DraggableItem _draggableItemPrefab; 
        [SerializeField] private RectTransform _backpackPanel;
        [SerializeField] private float _animationDuration;
        [SerializeField] private AvailableItemsContainer _availableItemsContainer;
        [SerializeField] private RectTransform _itemsContainer;
        private float _backpackHideVerticalPosition;
        private float _backpackVisibleVerticalPosition;
        
        public void OnEnter()
        {
            _backpackVisibleVerticalPosition = 0;
            _backpackHideVerticalPosition = -_backpackPanel.rect.height;
            _availableItemsContainer.Reset();
        }

        public void RenderAvailableItems(Item[] items)
        {
            _availableItemsContainer.Reset();
            
            var draggableItems = new List<DraggableItem>();
            foreach (var item in items)
            {
                var draggableItem = Instantiate(_draggableItemPrefab, _itemsContainer);
                draggableItem.Init(item, _availableItemsContainer);
                draggableItems.Add(draggableItem);
            }
            
            _availableItemsContainer.AddRange(draggableItems);
        }

        public List<DraggableItem> GetItems()
        {
            return _availableItemsContainer.Items;
        }
        
        public void SetBackpackVisible(bool isVisible)
        {
            var toPos = new Vector2(0, isVisible? _backpackVisibleVerticalPosition : _backpackHideVerticalPosition);
            StartCoroutine(MoveBackpack(_backpackPanel.anchoredPosition, toPos));
        }

        private IEnumerator MoveBackpack(Vector2 from, Vector2 to)
        {
            var timer = 0f;
            while (timer <= _animationDuration)
            {
                _backpackPanel.anchoredPosition = Vector2.Lerp(from, to, timer / _animationDuration);
                timer += Time.deltaTime;
                yield return null;
            }
            _backpackPanel.anchoredPosition = to;
        }
    }
}