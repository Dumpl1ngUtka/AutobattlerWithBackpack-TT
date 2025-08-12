using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Backpack;
using BattleScene.Enemy;
using BattleScene.Player;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace BattleScene
{
    public class BattleSceneView : MonoBehaviour
    {
        [Header("Battle")]
        [SerializeField] private RectTransform[] _spawnPoints;
        [SerializeField] private RectTransform _agentContainer;
        [SerializeField] private EnemyAgent _agentPrefab;
        [SerializeField] private PlayerHealth _player;
        [SerializeField] private Image _healthBar;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private RectTransform _battleItemContainer;
        [SerializeField] private BattleItemCell _battleItemCellPrefab;
        [Header("Backpack")]
        [SerializeField] private DraggableItem _draggableItemPrefab; 
        [SerializeField] private RectTransform _backpackPanel;
        [SerializeField] private float _animationDuration;
        [SerializeField] private BackpackContainer _backpackContainer;
        [SerializeField] private AvailableItemsContainer _availableItemsContainer;
        [SerializeField] private RectTransform _itemsContainer;
        [Header("EndGamePanel")] 
        [SerializeField] private RectTransform _winPanel;
        [SerializeField] private RectTransform _losePanel;
        private float _backpackHideVerticalPosition;
        private float _backpackVisibleVerticalPosition;
        private Vector3 _spawnPositionOffset;
        private List<BattleItemCell> _battleItems;
        
        public List<EnemyAgent> Agents { get; private set; }

        private void OnEnable()
        {
            _player.OnHealthChanged += UpdateHealthBar;
        }

        private void OnDisable()
        {
            _player.OnHealthChanged -= UpdateHealthBar;
        }

        public void OnEnter()
        {
            _backpackVisibleVerticalPosition = 0;
            _backpackHideVerticalPosition = -_backpackPanel.rect.height;
            _availableItemsContainer.Reset();
            _spawnPositionOffset = new Vector3(Screen.width, Screen.height) / 2;
            _winPanel.gameObject.SetActive(false);
            _losePanel.gameObject.SetActive(false);
            ClearContainer(_itemsContainer);
            ClearContainer(_agentContainer);
            ClearContainer(_battleItemContainer);
        }

        public void CreateEnemyAgentsPool(int count)
        {
            Agents = new List<EnemyAgent>();
            for (var i = 0; i < count; i++)
            {
                var instance = Instantiate(_agentPrefab, _agentContainer);
                instance.gameObject.SetActive(false);
                Agents.Add(instance);
            }
        }

        public void RenderItemForBattle()
        {
            _battleItems = new List<BattleItemCell>();
            ClearContainer(_battleItemContainer);
            foreach (var item in _backpackContainer.GetItems())
            {
                var instantiate = Instantiate(_battleItemCellPrefab, _battleItemContainer);
                instantiate.Init(item);
                _battleItems.Add(instantiate);
            }
        }

        public void SpawnEnemies(Enemy.Enemy[] enemies, float delay)
        {
            StartCoroutine(SpawnEnemiesCoroutine(enemies, delay));
        }

        public void RenderAvailableItems(Item[] items)
        {
            _availableItemsContainer.Reset();
            
            var draggableItems = new List<DraggableItem>();
            foreach (var item in items)
            {
                var draggableItem = Instantiate(_draggableItemPrefab, _itemsContainer);
                draggableItem.Init(item, _availableItemsContainer);
                draggableItems.Add(draggableItem);
            }
            
            _availableItemsContainer.AddRange(draggableItems);
        }

        public List<BattleItemCell> GetBattleItems() => _battleItems;

        public void SetBackpackVisible(bool isVisible)
        {
            var toPos = new Vector2(0, isVisible? _backpackVisibleVerticalPosition : _backpackHideVerticalPosition);
            _availableItemsContainer.Reset();
            StartCoroutine(MoveBackpack(_backpackPanel.anchoredPosition, toPos));
        }
        
        public void ShowEndGamePanel(bool isWin)
        {
            if (isWin)
                _winPanel.gameObject.SetActive(true);
            else 
                _losePanel.gameObject.SetActive(true);
        }

        public void ClearContainer(RectTransform container)
        {
            foreach(RectTransform child in container.transform) 
                Destroy(child.gameObject);
        }

        private IEnumerator MoveBackpack(Vector2 from, Vector2 to)
        {
            var timer = 0f;
            while (timer <= _animationDuration)
            {
                _backpackPanel.anchoredPosition = Vector2.Lerp(from, to, timer / _animationDuration);
                timer += Time.deltaTime;
                _availableItemsContainer.UpdatePositionsInContainer();
                yield return null;
            }
            _backpackPanel.anchoredPosition = to;
        }

        private IEnumerator SpawnEnemiesCoroutine(Enemy.Enemy[] enemies, float delay)
        {
            var spawnCount = 0;
            var delayInstruction = new WaitForSeconds(delay);
            while (spawnCount < enemies.Length)
            {
                foreach (var enemy in Agents.Where(enemy => !enemy.IsActive))
                {
                    var spawnPoint = GetRandomSpawnPoint();
                    var finishPoint = new Vector2(spawnPoint.x, 
                        (_player.RectTransform.position - _spawnPositionOffset).y);
                    enemy.Spawn(enemies[spawnCount++], spawnPoint, finishPoint, _player);
                    break;
                }

                yield return delayInstruction;
            }
        }

        private Vector3 GetRandomSpawnPoint()
        {
            return _spawnPoints[Random.Range(0, _spawnPoints.Length)].position - _spawnPositionOffset;
        }
        
        private void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            _healthBar.fillAmount = currentHealth / maxHealth;
            _healthText.text = currentHealth + "/" + maxHealth;
        }

        public void DestroyBattleItems()
        {
            ClearContainer(_battleItemContainer);
        }
    }
}