using MajongGame.Common.LevelSystem;
using UnityEngine;
using Zenject;

namespace MajongGame.Common.PopupSystem.PopupVariants
{
    public class WinPopup : Popup
    {
        private ILevelsController _levelsController;

        [Inject]
        private void Construct(ILevelsController levelsController)
        {
            _levelsController = levelsController;
        }

        public void NextLevel()
        {
            Hide(() => _levelsController.PlayNextLevel());
        }

        public void GoToMainMenu()
        {
            Debug.Log("Later.");
        }
    }
}