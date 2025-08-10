using Items;
using UnityEngine;

namespace BattleScene.Backpack
{
    public class BackpackContainerCell : MonoBehaviour, IItemHolder
    {
        [SerializeField] private Vector2Int _position;
        private BackpackContainer _holder;

        public bool IsEmpty { get; }
        public Vector2Int Position => _position;

        public void Init(BackpackContainer holder)
        {
            _holder = holder;
        }
            
        public void Add(DraggableItem item)
        {
            if (_holder.TryAdd(item, this)) ;

        }

        public void Remove(DraggableItem item)
        {
        }
    }
}