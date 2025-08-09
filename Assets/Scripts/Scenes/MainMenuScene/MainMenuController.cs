using UnityEngine;

namespace MainMenuScene
{
    public class MainMenuController : SceneController
    {
        [SerializeField] private MainMenuView _view;
        private MainMenuModel _model;

        public void OnStartBattleButtonClicked() => _model.StartBattle();

        public override void OnEnter()
        {
            _model = new MainMenuModel();
            _view.Enter();
        }


        public override void OnExit()
        {
        }
    }
}