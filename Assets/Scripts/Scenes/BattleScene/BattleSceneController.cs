using BattleScene.Enemy;
using BattleScene.Player;
using UnityEngine;

namespace BattleScene
{
    public class BattleSceneController : SceneController
    {
        private const int ItemForSpawnCount = 3;
        
        [SerializeField] private BattleSceneView _view;
        [SerializeField] private PlayerHealth _player;
        private BattleSceneModel _model;
        
        public override void OnEnter()
        {
            _model = new BattleSceneModel(this, _player);
            _view.OnEnter();
            _view.SetBackpackVisible(true);
            _view.RenderAvailableItems(_model.GetItemsForSpawn(ItemForSpawnCount));
            _view.CreateEnemyAgentsPool(_model.MaxEnemiesCount);
            _model.AddAgent(_view.Agents);
        }

        public override void OnExit()
        {
            
        }
        
        public void Reroll()
        {
            _view.RenderAvailableItems(_model.GetItemsForSpawn(ItemForSpawnCount));
        }

        public void StartBattleButtonClick()
        {
            _view.SetBackpackVisible(false);
            _model.GetCurrentWaveData(out var enemies, out var delay);
            _view.RenderItemForBattle();
            _model.StartWave(_view.GetBattleItems());
            _view.SpawnEnemies(enemies, delay);
        }

        public void GoToMainMenu()
        {
            _model.SwitchScene();
        }

        public void OnLose()
        {
            _view.ShowEndGamePanel(false);
            _view.DestroyBattleItems();
        }

        public void OnWin()
        {
            _view.ShowEndGamePanel(true);
            _view.DestroyBattleItems();
        }

        public void OnEndWave()
        {
            _view.SetBackpackVisible(true);
            _view.RenderAvailableItems(_model.GetItemsForSpawn(ItemForSpawnCount));
            _view.DestroyBattleItems();
        }
    }
}