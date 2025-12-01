using MajongGame.Common.LevelSystem;
using MajongGame.Common.PopupSystem;
using MajongGame.Configs.Level;
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
        [SerializeField] private SceneChanger _sceneChanger;

        public override void InstallBindings()
        {
            Container.Bind<PopupsHolder>().FromInstance(_popupsHolder).AsSingle();
            Container.Bind<SceneChanger>().FromInstance(_sceneChanger).AsSingle();
            BindLevelsController();
        }

        private void BindLevelsController()
        {
            LevelsController levelsController = new LevelsController(_locations, _coroutineRunner, _popupsHolder, _sceneChanger);
            Container.Bind<ILevelsController>().FromInstance(levelsController).AsSingle();
        }
    }
}