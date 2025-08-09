using UnityEngine;

namespace Managers
{
    public class SceneManager : MonoBehaviour
    {
        private static SceneManager _instance;
        public static SceneManager Instance => _instance == null ? 
            _instance = FindAnyObjectByType<SceneManager>() : _instance;

        public void Init()
        {
            
        }
    }
}
