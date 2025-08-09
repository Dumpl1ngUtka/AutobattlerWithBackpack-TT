using UnityEngine;

namespace BattleScene
{
    public class BattleSceneController : MonoBehaviour, ISceneController
    {
        [SerializeField] private BattleSceneView _view;  
        private BattleSceneModel _model;
        
        public void OnEnter()
        {
            _model = new BattleSceneModel();
            _view.OnEnter();
            OpenBackpackPanel();
        }

        public void OnExit()
        {
            
        }
        
        public void OpenBackpackPanel()
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