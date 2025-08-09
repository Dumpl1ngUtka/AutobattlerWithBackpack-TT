using System;
using UnityEngine;

namespace Managers
{
    public class Bootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            CreateSceneManager();
        }

        private void CreateSceneManager()
        {
            var obj = new GameObject("SceneManager");
            var manager = obj.AddComponent<SceneManager>();
            manager.Init();
            DontDestroyOnLoad(obj);
        }
        
        // private void CreateSaveLoadService()
        // {
        //     var jsonSaveLoadService = new JsonSaveLoadRepository();
        //     var obj = new GameObject("SaveLoadService");
        //     var service = obj.AddComponent<SaveLoadService>();
        //     service.Init(jsonSaveLoadService);
        //     DontDestroyOnLoad(obj);
        // }
    }
}
