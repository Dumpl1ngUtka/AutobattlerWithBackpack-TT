using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Enemy;
using BattleScene.Player;
using Items;
using Managers;
using Managers.SaveLoadManager;
using Random = UnityEngine.Random;

namespace BattleScene
{
    public class BattleSceneModel
    {
        private readonly List<Item> _itemsForSpawn;
        private readonly LevelPreset _levelData;
        private BattleSceneController _controller;
        private int _waveCount;
        private int _currentWaveEnemyCount;
        private int _waveKillCount;
        private List<EnemyAgent> _agents;
        private PlayerHealth _playerHealth;
        
        public int MaxEnemiesCount => _levelData.GetMaxEnemiesCount();
        
        public BattleSceneModel(BattleSceneController battleSceneController, PlayerHealth playerHealth)
        {
            _controller = battleSceneController;
            _playerHealth = playerHealth;
            var saveData = SaveLoadManager.Instance.LoadData();
            _itemsForSpawn = GetAvailableForSpawnItems(saveData);
            _levelData = LevelPreset.GetLevelByID(saveData.CurrentLevelIndex);
            playerHealth.OnDeath += Lose;
            _waveCount = 0;
        }

        private void Lose()
        {
            _controller.OnLose();
        }

        private void Win()
        {
            _controller.OnWin();
        }

        private void EndWave()
        {
            _controller.OnEndWave();
        }

        public Item[] GetItemsForSpawn(int count)
        {
            var items = new Item[count];
            for (var i = 0; i < count; i++)
            {
                var randomIndex = Random.Range(0, _itemsForSpawn.Count);
                items[i] = _itemsForSpawn[randomIndex];
            }
            return items;
        }
        
        public void GetCurrentWaveData(out Enemy.Enemy[] enemies, out float delay)
        {
            var enemiesList = new List<Enemy.Enemy>();
            var waveData = _levelData.Waves[_waveCount];
            foreach (var enemyInWave in waveData.Enemies)
            {
                for (var i = 0; i < enemyInWave.Count; i++) 
                    enemiesList.Add(enemyInWave.Enemy);
            }
            _waveCount++;
            _currentWaveEnemyCount = enemiesList.Count;
            enemies = enemiesList.ToArray();
            delay = waveData.SpawnDelay;
        }

        private List<Item> GetAvailableForSpawnItems(SaveData saveData)
        {
            var itemsForSpawn = new List<Item>();
            var itemsKeys = saveData.AvailableItems;
            foreach (var key in itemsKeys)
            {
                var item = Item.GetItemsByKey(key)?.Find(item => item.Level == 1);
                itemsForSpawn.Add(item);
            }
            return itemsForSpawn;
        }

        public void AddAgent(List<EnemyAgent> viewAgents)
        {
            foreach (var agent in viewAgents) 
                agent.OnDeath += IncreaseCounter;
            
            _agents = viewAgents;
        }

        public void StartWave(List<BattleItemCell> itemsInBackpack)
        {
            _waveKillCount = 0;
            foreach (var item in itemsInBackpack)
            {
                item.Used += UseItem;
            }
        }

        private void UseItem(Item item)
        {
            if (item.ActionType == ActionType.Attack)
            {
                var agent = SelectAgent();
                agent?.ApplyDamage(item.Damage);
            }
            else if (item.ActionType == ActionType.Heal)
                _playerHealth.IncreaseHealth(item.Damage);
        }

        private void IncreaseCounter()
        {
            _waveKillCount++;
            
            if (_waveKillCount >= _currentWaveEnemyCount)
            {
                if (_waveCount == _levelData.Waves.Length)
                    Win();
                else
                    EndWave();
            }
        }

        public EnemyAgent SelectAgent()
        {
            return _agents.FirstOrDefault(agent => agent.IsActive);
        }

        public void SwitchScene()
        {
            SceneManager.Instance.OpenMainMenuScene();
        }
    }
}