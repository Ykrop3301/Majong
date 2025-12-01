using MajongGame.Common.LevelSystem;
using MajongGame.Common.PopupSystem;
using MajongGame.Configs.Level;
using MajongGame.LevelSystem;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MajongGame.Common.Zenject
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private PopupsHolder _popupsHolder;
        [SerializeField] private List<LevelLocationConfig> _locations;
        [SerializeField] private CoroutineRunner _coroutineRunner;

        public override void InstallBindings()
        {
            Container.Bind<PopupsHolder>().FromInstance(_popupsHolder).AsSingle();

            BindLevelsController();
        }

        private void BindLevelsController()
        {
            LevelsController levelsController = new LevelsController(_locations, _coroutineRunner);
            Container.Bind<ILevelsController>().FromInstance(levelsController).AsSingle();
        }
    }
}