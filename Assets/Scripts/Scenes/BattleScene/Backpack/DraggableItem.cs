using System;
using Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BattleScene.Backpack
{
    public class DraggableItem : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image _image;
        private RectTransform _rectTransform;
        private Item _item;
        private IItemHolder _holder;
        private Vector2 _targetPosition;
        private float _moveSpeed = 5f;
        private bool _isDragging;
        
        public Action<DraggableItem> Clicked;
        public Action<DraggableItem> BeginDrag;
        public Action<DraggableItem> EndDrag;
        public Action<DraggableItem, IItemHolder> DraggingOverHolder;
        
        public void Init(Item item, IItemHolder holder)
        {
            _item = item;
            _image.sprite = _item.Sprite;
            _holder = holder;
            _rectTransform = GetComponent<RectTransform>();
            _targetPosition = _rectTransform.anchoredPosition;
            _isDragging = false;
        }

        public void SetTargetPosition(Vector2 targetPosition)
        {
            _targetPosition = targetPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("aaa");
            _isDragging = true;
            //_holder.Hide(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
            var itemHolder = eventData.pointerCurrentRaycast.gameObject?.GetComponent<IItemHolder>();
                
            if (itemHolder != null)
            {
                if (itemHolder == _holder)
                    return;
                
                itemHolder.Add(_item);
                _holder.Remove(this);
                _holder = itemHolder;
            }
        }

        private void Update()
        {
            if (!_isDragging)
                _rectTransform.anchoredPosition = 
                Vector2.Lerp(_rectTransform.anchoredPosition, _targetPosition, Time.deltaTime * _moveSpeed);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isDragging)
                return;
            
            Debug.Log(_item.Key);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }
    }
}