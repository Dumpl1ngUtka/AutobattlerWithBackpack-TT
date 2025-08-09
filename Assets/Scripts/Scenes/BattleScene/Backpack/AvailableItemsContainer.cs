using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace BattleScene.Backpack
{
    public class AvailableItemsContainer : MonoBehaviour, IItemHolder
    {
        [SerializeField] private DraggableItem _itemPrefab; 
        [SerializeField] private RectTransform _container;
        
        public List<DraggableItem> Items { get; private set; }

        public void Reset()
        {
            Items = new List<DraggableItem>();
            ClearContainer();
        }

        public void AddRange(Item[] items)
        {
            foreach (var item in items) 
                Add(item);
        }

        public void Add(Item item)
        {
            var instantiate = Instantiate(_itemPrefab, _container);
            instantiate.Init(item, this);
            Items.Add(instantiate);
            UpdatePositionsInContainer();
        }

        public void Remove(DraggableItem item)
        {
            Items.Remove(item);
            UpdatePositionsInContainer();
        }

        private void ClearContainer()
        {
            foreach(Transform child in _container.transform) 
                Destroy(child.gameObject);
        }

        private void UpdatePositionsInContainer()
        {
            var width = _container.rect.width;
            var space = width / (Items.Count + 1);
            var index = 0;
            foreach (var item in Items)
            {
                var horizontalPosition = space * ++index - width / 2;
                item.SetTargetPosition(new Vector2(horizontalPosition, 0));
            }
        }
    }
}