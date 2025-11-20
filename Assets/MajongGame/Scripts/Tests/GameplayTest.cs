using MajongGame.Common;
using MajongGame.Common.LevelSystem;
using MajongGame.Configs.Level;
using MajongGame.LevelSystem;
using System.Collections.Generic;
using UnityEngine;

namespace MajongGame.Tests
{
    public class GameplayTest : MonoBehaviour
    {
        [SerializeField] private List<LevelLocationConfig> _locations;
        [SerializeField] private CoroutineRunner _coroutineRunner;
        
        private ILevelsController _levelsController;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _levelsController = new LevelsController(_locations, _coroutineRunner);

            _levelsController.PlayCurrentLevel();
        }
    }
}