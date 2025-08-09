using System.Collections.Generic;
using System.Linq;
using Items;
using Managers.SaveLoadManager;
using UnityEngine;

namespace BattleScene
{
    public class BattleSceneModel
    {
        private readonly List<Item> _itemsForSpawn;
        
        public BattleSceneModel()
        {
            _itemsForSpawn = new List<Item>();
            var itemsKeys = SaveLoadManager.Instance.LoadData().AvailableItems;
            foreach (var key in itemsKeys)
            {
                var item = Item.GetItemsByKey(key)?.Find(item => item.Level == 1);
                _itemsForSpawn.Add(item);
            }
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
        
        public void StartWave()
        {
            
        }
    }
}