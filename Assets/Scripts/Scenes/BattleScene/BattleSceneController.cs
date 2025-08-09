using System;
using BattleScene.Backpack;
using UnityEngine;

namespace BattleScene
{
    public class BattleSceneController : SceneController
    {
        private const int ItemForSpawnCount = 3;
        
        [SerializeField] private BattleSceneView _view;  
        private BattleSceneModel _model;

        private Action<DraggableItem> Clicked;
        
        public override void OnEnter()
        {
            _model = new BattleSceneModel();
            _view.OnEnter();
            OpenBackpackPanel();
            _view.RenderAvailableItems(_model.GetItemsForSpawn(ItemForSpawnCount));
            Clicked += _view.GetItems()[0].Clicked;
            foreach (var item in _view.GetItems())
            {
                //item.Clicked += ctx => 
            }

            
        }

        public override void OnExit()
        {
            
        }
        
        public void OpenBackpackPanel()
        {
            _view.SetBackpackVisible(true);
        }

        public void Reroll()
        {
            _view.RenderAvailableItems(_model.GetItemsForSpawn(ItemForSpawnCount));
        }

        public void CloseBackpackPanel()
        {
            _view.SetBackpackVisible(false);
        }

        public void StartBattleButtonClick()
        {
            _view.SetBackpackVisible(false);
            _model.StartWave();
        }
    }
}