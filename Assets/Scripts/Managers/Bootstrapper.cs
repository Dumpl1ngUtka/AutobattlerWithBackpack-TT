using System;
using BattleScene;
using MainMenuScene;
using Managers.SaveLoadManager;
using UnityEngine;

namespace Managers
{
    public class Bootstrapper : MonoBehaviour
    {
        [Header("Scenes")]
        [SerializeField] private MainMenuController _mainMenuScene;
        [SerializeField] private BattleSceneController _battleScene;
        
        private void Awake()
        {
            CreateSceneManager();
            CreateSaveLoadManager();
        }

        private void Start()
        {
            SceneManager.Instance.OpenMainMenuScene();
        }

        private void CreateSceneManager()
        {
            var obj = new GameObject("SceneManager");
            var manager = obj.AddComponent<SceneManager>();
            manager.Init(_mainMenuScene, _battleScene);
            DontDestroyOnLoad(obj);
        }
        
        private void CreateSaveLoadManager()
        {
            var jsonSaveLoadService = new JsonSaveDataManager();
            var obj = new GameObject("SaveLoadManager");
            var service = obj.AddComponent<SaveLoadManager.SaveLoadManager>();
            service.Init(jsonSaveLoadService);
            DontDestroyOnLoad(obj);
        }
    }
}
