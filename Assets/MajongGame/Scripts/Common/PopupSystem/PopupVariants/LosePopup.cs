using MajongGame.Common.LevelSystem;
using Zenject;

namespace MajongGame.Common.PopupSystem.PopupVariants
{
    public class LosePopup : Popup
    {
        private ILevelsController _levelsController;
        private SceneChanger _sceneChanger;

        [Inject]
        private void Construct(ILevelsController levelsController, SceneChanger sceneChanger)
        {
            _levelsController = levelsController;
            _sceneChanger = sceneChanger;
        }

        public void ReplayLevel()
        {
            Hide(() => _levelsController.PlayCurrentLevel());
        }

        public void GoToMainMenu()
        {
            Hide(() => _sceneChanger.LoadScene("MenuScene"));
        }
    }
}