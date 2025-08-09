using System.IO;
using UnityEngine;

namespace Managers.SaveLoadManager
{
    public class JsonSaveDataManager : ISaveLoadManager<SaveData>
    {
        public void Save(string path, SaveData data)
        {
            var json = JsonUtility.ToJson(data); 
            File.WriteAllText(path, json);    
        }

        public SaveData Load(string path)
        {
            if (!File.Exists(path)) return new SaveData();
            
            var json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }
    }
}