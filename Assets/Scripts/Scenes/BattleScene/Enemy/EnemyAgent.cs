using System;
using BattleScene.Player;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Enemy
{
    public class EnemyAgent : MonoBehaviour
    {
        private readonly Vector2 _moveDirection = Vector2.down;
        
        [SerializeField] private Image _image;
        [SerializeField] private RectTransform _rectTransform;
        private float _maxHealth;
        private float _currentHealth;
        private float _speed;
        private float _pathProgress;
        private float _damage;
        private float _attackDelay;
        private float _attackTimer;
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private PlayerHealth _playerHealth;
        
        public bool IsActive { get; private set; }
        public Action OnDeath;
        public Action<float, float> OnHealthChanged;

        public void Spawn(Enemy enemy, Vector3 startPosition, Vector3 targetPosition, PlayerHealth player)
        {
            gameObject.SetActive(true);
            _image.sprite = enemy.Sprite;
            _speed = enemy.Speed;
            _maxHealth = enemy.Health;
            _currentHealth = _maxHealth;
            _rectTransform.anchoredPosition = startPosition;
            IsActive = true;
            _startPosition = startPosition;
            _endPosition = targetPosition;
            _damage = enemy.Damage;
            _attackDelay = enemy.AttackDelay;
            _playerHealth = player;
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }

        private void Update()
        {
            if (!IsActive)
                return;
            if (_pathProgress < 1)
            {
                _pathProgress += Time.deltaTime * _speed;
                _rectTransform.anchoredPosition = Vector2.Lerp(_startPosition, _endPosition, _pathProgress);
            }
            else
            {
                _attackTimer += Time.deltaTime;
                if (_attackTimer >= _attackDelay)
                {
                    _playerHealth.ApplyDamage(_damage);
                    _attackTimer = 0;
                }
            }
        }

        public void ApplyDamage(float damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke();
                IsActive = false;
                gameObject.SetActive(false);
                return;
            }
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }
}