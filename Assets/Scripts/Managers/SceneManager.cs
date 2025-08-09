using UnityEngine;

namespace Managers
{
    public class SceneManager : MonoBehaviour
    {
        private static SceneManager _instance;
        public static SceneManager Instance => _instance == null ? 
            _instance = FindAnyObjectByType<SceneManager>() : _instance;
        
        private SceneController _battleScene;
        private SceneController _mainMenuScene;
        private SceneController _currentScene;

        public void Init(SceneController mainMenuScene, SceneController battleScene)
        {
            _mainMenuScene = mainMenuScene;
            _battleScene = battleScene;
            
            _mainMenuScene.gameObject.SetActive(false);
            _battleScene.gameObject.SetActive(false);
        }

        public void OpenBattleScene() => SwitchScene(_battleScene);
        
        public void OpenMainMenuScene() => SwitchScene(_mainMenuScene);

        private void SwitchScene(SceneController scene)
        {
            if (_currentScene != null)
            {
                _currentScene.OnExit();
                _currentScene.gameObject.SetActive(false);
            }
            _currentScene = scene;
            _currentScene.gameObject.SetActive(true);
            _currentScene.OnEnter();
        }
    }
}
