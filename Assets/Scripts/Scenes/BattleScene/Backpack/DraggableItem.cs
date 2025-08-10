using System;
using Items;
using Managers.PanelManager;
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
        private CanvasGroup _canvasGroup;
        
        public Item Item => _item;

        private void OnEnable()
        {
            _rectTransform = GetComponent<RectTransform>();
            //_image.alphaHitTestMinimumThreshold = 0.5f;
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Init(Item item, IItemHolder holder)
        {
            _item = item;
            _image.sprite = _item.Sprite;
            _holder = holder;
            _targetPosition = _rectTransform.anchoredPosition;
            _isDragging = false;
        }

        public void SetTargetPosition(Vector2 targetPosition)
        {
            _targetPosition = targetPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
            _canvasGroup.blocksRaycasts = false;
            //_holder.Hide(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
            _canvasGroup.blocksRaycasts = true;
            var itemHolder = eventData.pointerCurrentRaycast.gameObject?.GetComponent<IItemHolder>();
                
            if (itemHolder != null)
            {
                if (itemHolder == _holder)
                    return;
                
                itemHolder.Add(this);
            }
            
            var otherItem = eventData.pointerCurrentRaycast.gameObject?.GetComponent<DraggableItem>();

            if (otherItem != null)
            {
                if (otherItem._item.Key == _item.Key 
                    && otherItem._item.Level == _item.Level
                    && otherItem._item.CombinationResult != null)
                {
                    otherItem.Init(_item.CombinationResult, otherItem._holder);
                    _holder.Remove(this);
                }
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
            
            PanelManager.Instance.InstantiateItemInfoPanel(_item);
        }

        public void OnPointerDown(PointerEventData eventData) { }
    }
}