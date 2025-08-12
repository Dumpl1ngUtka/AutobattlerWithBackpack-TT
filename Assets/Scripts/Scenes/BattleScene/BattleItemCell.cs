using System;
using Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BattleScene
{
    public class BattleItemCell : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private Image _timerImage;
        private Item _item;
        private bool _isAuto;
        private float _timer;
        private float _reloadTime;
        
        public Action<Item> Used;
        
        public void Init(Item item)
        {
            _item = item;
            _itemIcon.sprite = item.Sprite;
            _isAuto = item.IsAutoUse;
            _reloadTime = item.ReloadTime;
            _timer = 0;
        }

        private void UseItem()
        {
            if (_timer <= 0)
            {
                Used?.Invoke(_item);
                _timer = _reloadTime;
            }
        }
        
        public void Update()
        {
            if (_timer >= 0)
            {
                _timer -= Time.deltaTime;
                _timerImage.fillAmount = _timer / _reloadTime;
                if (_isAuto && _timer <= 0) 
                    UseItem();
            }

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isAuto)
                return;
            
            UseItem();
        }
    }
}
