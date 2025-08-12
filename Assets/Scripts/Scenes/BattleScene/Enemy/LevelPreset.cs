using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleScene.Enemy
{
    [CreateAssetMenu(fileName = "LevelPreset", menuName = "Config/LevelPreset")]
    public class LevelPreset : ScriptableObject
    {
        public int LevelID;
        public string LevelName;
        public Sprite Background;
        public WavePreset[] Waves;

        public int GetMaxEnemiesCount()
        {
            var maxCount = 0;
            foreach (var wave in Waves)
            {
                var count = wave.Enemies.Sum(enemyInWave => enemyInWave.Count);
                if (count > maxCount) 
                    maxCount = count;
            }
            return maxCount;
        }
        
        private static LevelPreset[] _levels;
        public static LevelPreset GetLevelByID(int id)
        {
            _levels ??= Resources.LoadAll<LevelPreset>("Configs/Levels");
            return _levels.First(level => level.LevelID == id);
        }
    }

    [Serializable]
    public class WavePreset
    {
        public EnemyInWave[] Enemies;
        public float SpawnDelay;
    }

    [Serializable]
    public class EnemyInWave
    {
        public Enemy Enemy;
        public int Count;
    }
}