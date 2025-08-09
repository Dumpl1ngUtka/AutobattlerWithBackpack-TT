using System;

namespace Managers.SaveLoadManager
{
    [Serializable]
    public class SaveData
    {
        public int CurrentLevelIndex;
        public string[] AvailableItems;
        
        public SaveData(int currentLevelIndex, string[] availableItems)
        {
            CurrentLevelIndex = currentLevelIndex;
            AvailableItems = new string[2]{"pistol", "gun"};
        }

        public SaveData()
        {
            CurrentLevelIndex = 0;
            AvailableItems = new string[2]{"pistol", "gun"};
        }
    }
}