using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Enemy
{
    [RequireComponent(typeof(EnemyAgent))]
    public class EnemyAgentRenderer : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private TMP_Text _healthText;
        private EnemyAgent _agent;
        
        private void OnEnable()
        {
            if (_agent == null)
                _agent = GetComponent<EnemyAgent>();
            
            _agent.OnHealthChanged += OnHealthChanged;
            _agent.OnDeath += OnDeath;
        }

        private void OnHealthChanged(float currentHealth, float maxHealth)
        {
            _healthBar.fillAmount = currentHealth / maxHealth;
            _healthText.text = currentHealth + "/" + maxHealth;
        }

        private void OnDisable()
        {
            _agent.OnHealthChanged -= OnHealthChanged;
            _agent.OnDeath -= OnDeath;
        }

        private void OnDeath()
        {
        }
    }
}