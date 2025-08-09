using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Config/Item")]
    public class Item : ScriptableObject
    {
        [Header("Main Item Settings")]
        public Sprite Sprite;
        public string Name;
        [Min(0)] public int Level = 1;
        [Header("Backpack Settings")] 
        public Item CombinationResult ;
        [Header("BattleSettings")]
        public bool IsAutoUse;
        [Min(0)] public float ReloadTime;
        [Min(0)] public float Damage;
        [Min(0)] public float Range;
        public ItemAction Action;
    }

    public abstract class ItemAction
    {
        public string Name;
    }

    public class HealAction : ItemAction
    {
        public int Heal;
    }

    public class AttackAction : ItemAction
    {
        public int DamageToAttack;
    }
}
