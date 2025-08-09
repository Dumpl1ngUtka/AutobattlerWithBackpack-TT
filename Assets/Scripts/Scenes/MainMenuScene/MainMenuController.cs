using System;
using UnityEngine;

namespace MainMenuScene
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private MainMenuView _view;
        private MainMenuModel _model;
        
        private void Enter()
        {
            _model = new MainMenuModel();
            _view.Enter();
        }

        public void OnStartBattleButtonClicked() => _model.StartBattle();
    }
}