using UnityEngine;

namespace Managers.PanelManager.Panels
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] private RectTransform _cardInfoPanel;
        
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}