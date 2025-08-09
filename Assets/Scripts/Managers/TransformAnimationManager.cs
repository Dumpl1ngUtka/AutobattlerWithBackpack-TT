using UnityEngine;

namespace Managers
{
    public class TransformAnimationManager : MonoBehaviour
    {
        private static TransformAnimationManager _instance;
        public static TransformAnimationManager Instance => _instance == null ? 
            _instance = FindAnyObjectByType<TransformAnimationManager>() : _instance;

        public void SpringMoveAnimation(RectTransform body, Vector2 targetPosition)
        {
            
        }
    }
}