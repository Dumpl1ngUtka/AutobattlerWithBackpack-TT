using System;
using UnityEngine;

namespace BattleScene.Enemy
{
    [CreateAssetMenu(fileName = "LevelPreset", menuName = "Config/LevelPreset")]
    public class LevelPreset : ScriptableObject
    {
        public WavePreset[] Waves;
    }

    [Serializable]
    public class WavePreset
    {
        public EnemyInWave[] Enemies;
    }

    [Serializable]
    public class EnemyInWave
    {
        public Enemy Enemy;
        public int Count;
    }
}