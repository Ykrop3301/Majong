using MajongGame.Common.LevelSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MajongGame.Common.PopupSystem.PopupVariants
{
    public class WinPopup : Popup
    {
        [SerializeField] private Button _centerMainMenuButton;
        [SerializeField] private Transform _twoButtonsHolder;

        private ILevelsController _levelsController;
        private SceneChanger _sceneChanger;

        [Inject]
        private void Construct(ILevelsController levelsController, SceneChanger sceneChanger)
        {
            _levelsController = levelsController;
            _sceneChanger = sceneChanger;
        }

        public override void Show()
        {
            base.Show();
            if (_levelsController.CurrentLevel.location.LevelsCount - 1 == _levelsController.CurrentLevel.levelId)
                OnLocationEnded();
        }

        public void NextLevel()
        {
            Hide(() => _levelsController.PlayNextLevel());
        }

        public void GoToMainMenu()
        {
            Hide(() => _sceneChanger.LoadScene("MenuScene"));
        }

        private void OnLocationEnded()
        {
            _centerMainMenuButton.gameObject.SetActive(true);
            _twoButtonsHolder.gameObject.SetActive(false);
        }
    }
}