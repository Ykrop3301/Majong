using MajongGame.Common.LevelSystem;
using Zenject;

namespace MajongGame.Common.PopupSystem.PopupVariants
{
    public class WinPopup : Popup
    {
        private ILevelsController _levelsController;
        private SceneChanger _sceneChanger;

        [Inject]
        private void Construct(ILevelsController levelsController, SceneChanger sceneChanger)
        {
            _levelsController = levelsController;
            _sceneChanger = sceneChanger;
        }

        public void NextLevel()
        {
            Hide(() => _levelsController.PlayNextLevel());
        }

        public void GoToMainMenu()
        {
            Hide(() => _sceneChanger.LoadScene("MenuScene"));
        }
    }
}