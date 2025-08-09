using Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BattleScene.Backpack
{
    public class DraggableItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image _image;
        private Item _item;
        private IItemHolder _holder;
        
        public RectTransform RectTransform {get; private set;}
        
        public void Init(Item item, IItemHolder holder)
        {
            _item = item;
            _image.sprite = _item.Sprite;
            _holder = holder;
            RectTransform = GetComponent<RectTransform>();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _holder.Hide(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransform.anchoredPosition += eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var itemHolder = eventData.pointerCurrentRaycast.gameObject?.GetComponent<IItemHolder>();
            if (itemHolder != null)
            {
                itemHolder.Add(_item);
                _holder.Remove(this);
                _holder = itemHolder;
            }
        }
    }
}