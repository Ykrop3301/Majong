using MajongGame.Common.LevelSystem;
using UnityEngine;
using Zenject;

namespace MajongGame.Common.PopupSystem.PopupVariants
{
    public class LosePopup : Popup
    {
        private ILevelsController _levelsController;

        [Inject]
        private void Construct(ILevelsController levelsController)
        {
            _levelsController = levelsController;
        }

        public void ReplayLevel()
        {
            Hide(() => _levelsController.PlayCurrentLevel());
        }

        public void GoToMainMenu()
        {
            Debug.Log("Later.");
        }
    }
}