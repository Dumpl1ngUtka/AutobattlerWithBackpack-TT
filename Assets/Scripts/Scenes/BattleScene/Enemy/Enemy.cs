using UnityEngine;

namespace BattleScene.Enemy
{ 
    [CreateAssetMenu(fileName = "Enemy", menuName = "Config/Enemy")]
    public class Enemy : ScriptableObject
    {
        public string Name;
        public Sprite Sprite;
        public int Health;
        public int Damage;
        public float Speed;
    }
}