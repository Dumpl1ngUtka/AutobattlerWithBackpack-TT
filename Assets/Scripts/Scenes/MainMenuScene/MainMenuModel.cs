using Managers;

namespace MainMenuScene
{
    public class MainMenuModel
    {
        public MainMenuModel()
        {
            
        }
        
        public void StartBattle()
        {
            SceneManager.Instance.OpenBattleScene();
        }
    }
}