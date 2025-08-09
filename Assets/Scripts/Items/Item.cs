using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Config/Item")]
    public class Item : ScriptableObject
    {
        [Header("Main Item Settings")]
        public Sprite Sprite;
        public string Key;
        [Min(0)] public int Level = 1;
        [Header("Backpack Settings")] 
        public Item CombinationResult ;
        [Header("BattleSettings")]
        public bool IsAutoUse;
        public ActionType ActionType;
        [Min(0)] public float ReloadTime;
        [Min(0)] public float Damage;

        private static Item[] _items;
        public static List<Item> GetItemsByKey(string key)
        {
            _items ??= Resources.LoadAll<Item>("Configs/Items");
            return _items.Where(item => item.Key == key).ToList();
        }
    }

    public enum ActionType
    {
        Attack,
        Heal,
    }
}
