using System.Globalization;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers.PanelManager.Panels
{
    public class ItemInfoPanel : Panel
    {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _reloadTime;
        [SerializeField] private TMP_Text _damage;
        
        public void Render(Item item)
        {
            _itemIcon.sprite = item.Sprite;
            _name.text = item.Key;
            _level.text = "Lv. " + item.Level;
            _reloadTime.text = item.ReloadTime.ToString(CultureInfo.InvariantCulture);
            _damage.text = item.Damage.ToString(CultureInfo.InvariantCulture);
        }
        
        public void CloseInfoPanel()
        {
            Destroy(gameObject);
        }
    }
}
