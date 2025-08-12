using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace BattleScene.Backpack
{
    public class AvailableItemsContainer : MonoBehaviour, IItemHolder
    {
        [SerializeField] private RectTransform _container;
        
        public List<DraggableItem> Items { get; private set; } = new List<DraggableItem>();
        public RectTransform Container => _container;

        public void Reset()
        {
            ClearContainer();
        }

        public void AddRange(List<DraggableItem> items)
        {
            foreach (var item in items) 
                Add(item);
        }

        public bool Add(DraggableItem item)
        {
            Items.Add(item);
            UpdatePositionsInContainer();
            return true;
        }

        public void Remove(DraggableItem item)
        {
            Items.Remove(item);
            UpdatePositionsInContainer();
        }

        private void ClearContainer()
        {
            foreach (var item in Items) 
                Destroy(item.gameObject);

            Items = new List<DraggableItem>();
        }

        public void UpdatePositionsInContainer()
        {
            var verticalPosition = -_container.position.y;
            var width = _container.rect.width;
            var space = width / (Items.Count + 1);
            var index = 0;
            foreach (var item in Items)
            {
                var horizontalPosition = space * ++index - width / 2;
                item.SetTargetPosition(new Vector2(horizontalPosition, verticalPosition));
            }
        }
    }
}