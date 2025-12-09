using MajongGame.Common.LevelSystem;
using UnityEngine;
using Zenject;

namespace MajongGame.MainMenu
{
    public class CurrentLevelButton : MonoBehaviour
    {
        private ILevelsController _levelsController;

        [Inject]
        private void Construct(ILevelsController levelsController)
        {
            _levelsController = levelsController;
        }

        public void PlayCurrentLevel()
        {
            if (_levelsController != null)
                _levelsController.PlayCurrentLevel();
        }
    }
}