using System;
using BattleScene;
using MainMenuScene;
using Managers.PanelManager.Panels;
using Managers.SaveLoadManager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class Bootstrapper : MonoBehaviour
    {
        [Header("Scenes")]
        [SerializeField] private MainMenuController _mainMenuScene;
        [SerializeField] private BattleSceneController _battleScene;
        [FormerlySerializedAs("_infoPanelPrefab")]
        [Header("Panels")]
        [SerializeField] private ItemInfoPanel _itemInfoPanelPrefab;
        
        private void Awake()
        {
            CreateSceneManager();
            CreateSaveLoadManager();
            CreatePanelManager();
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
            var manager = obj.AddComponent<SaveLoadManager.SaveLoadManager>();
            manager.Init(jsonSaveLoadService);
            DontDestroyOnLoad(obj);
        }
        
        private void CreatePanelManager()
        {
            var obj = new GameObject("PanelManager");
            var manager = obj.AddComponent<PanelManager.PanelManager>();
            manager.Init(_itemInfoPanelPrefab);
            DontDestroyOnLoad(obj);
        }
    }
}
