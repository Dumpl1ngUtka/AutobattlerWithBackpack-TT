using System.IO;
using UnityEngine;

namespace Managers.SaveLoadManager
{
    public class SaveLoadManager : MonoBehaviour
    {
        private static SaveLoadManager _instance;
        private ISaveLoadManager<SaveData> _levelLoader;
        private string _filePath;

        public static SaveLoadManager Instance => 
            _instance ?? FindAnyObjectByType(typeof(SaveLoadManager)) as SaveLoadManager;
        
        public void Init(ISaveLoadManager<SaveData> levelLoader)
        {
            _levelLoader = levelLoader;
            var saveDirectory = Path.Combine(Application.persistentDataPath, "Saves");
            
            if (!Directory.Exists(saveDirectory))
                Directory.CreateDirectory(saveDirectory);
            
            _filePath = Path.Combine(saveDirectory, "Save.json");
        }
        
        public void SaveData(SaveData data) => _levelLoader.Save(_filePath, data);
        
        public SaveData LoadData() => _levelLoader.Load(_filePath); 
    }
}