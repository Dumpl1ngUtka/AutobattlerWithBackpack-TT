using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace BattleScene.Backpack
{
    public class AvailableItemsContainer : MonoBehaviour, IItemHolder
    {
        [SerializeField] private DraggableItem _itemPrefab; 
        [SerializeField] private RectTransform _container;
        private RectTransform _rectTransform;
        private List<DraggableItem> _items;

        public void Reset()
        {
            _items = new List<DraggableItem>();
            _rectTransform = GetComponent<RectTransform>();
            ClearContainer();
        }

        public void Add(Item item)
        {
            var instantiate = Instantiate(_itemPrefab, _container);
            instantiate.Init(item, this);
        }

        public void Remove(DraggableItem item)
        {
            _items.Remove(item);
        }

        public void Hide(DraggableItem itemToHide)
        {
            foreach (var item in _items)
                if (item == itemToHide)
                    itemToHide.gameObject.SetActive(false);
        }

        private void ClearContainer()
        {
            foreach(Transform child in _container.transform) 
                Destroy(child.gameObject);
        }

        private void UpdatePositionsInContainer()
        {
            var length = _items.Count;
            var space = _rectTransform.rect.width / length;
            foreach (var item in _items)
            {
                //item.RectTransform.rect.position = 
            }
        }
    }
}