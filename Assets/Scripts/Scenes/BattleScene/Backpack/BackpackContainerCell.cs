using System;
using Items;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace BattleScene.Backpack
{
    public class BackpackContainerCell : MonoBehaviour, IItemHolder
    {
        [FormerlySerializedAs("_position")] [SerializeField] private Vector2Int _gridPosition;
        private Image _image;
        private BackpackContainer _backpackContainer;
        private RectTransform _rectTransform;

        public bool IsEmpty { get; private set; }
        public Vector2Int GridPosition => _gridPosition;
        public Vector2 RectPosition => _rectTransform.position - new Vector3(Screen.width, Screen.height) / 2;

        private void OnEnable()
        {
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
        }

        public void Init(BackpackContainer holder)
        {
            _backpackContainer = holder;
            SetEmptyStatus(true);
        }
            
        public void SetEmptyStatus(bool isEmpty)
        {
            IsEmpty = isEmpty;
            _image.color = isEmpty ? Color.white : Color.gray;
        }
        
        public bool Add(DraggableItem item)
        {
            if (_backpackContainer.TryAdd(item, this))
            {
                item.SetTargetPosition(RectPosition);
                return true; 
            }
            return false; 
        }

        public void Remove(DraggableItem item)
        {
            _backpackContainer.Remove(item, this);
            //item.SetHolder(this);
        }
    }
}