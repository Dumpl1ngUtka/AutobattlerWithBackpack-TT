using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
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
        public Item CombinationResult;
        public bool3x3 Slots;
        [Header("BattleSettings")]
        public bool IsAutoUse;
        public ActionType ActionType;
        public TargetType TargetType;
        [Min(0)] public float ReloadTime;
        [Min(0)] public float Damage;

        private static Item[] _items;
        public static List<Item> GetItemsByKey(string key)
        {
            _items ??= Resources.LoadAll<Item>("Configs/Items");
            return _items.Where(item => item.Key == key).ToList();
        }
    }
}
