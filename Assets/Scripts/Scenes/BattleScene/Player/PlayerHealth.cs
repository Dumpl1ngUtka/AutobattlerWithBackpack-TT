using System;
using UnityEngine;

namespace BattleScene.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        private float _currentHealth;
        
        public RectTransform RectTransform {get; private set;} 
        public Action OnDeath;
        public Action<float, float> OnHealthChanged;

        private void OnEnable()
        {
            RectTransform = GetComponent<RectTransform>();
            _currentHealth = _maxHealth;
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }

        public void ApplyDamage(float damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke();
                return;
            }
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }

        public void IncreaseHealth(float healValue)
        {
            _currentHealth += healValue;
            _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }
}