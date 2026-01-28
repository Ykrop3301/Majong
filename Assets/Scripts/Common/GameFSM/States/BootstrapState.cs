using Common.Save;
using Common.Settings;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Common.GameFSM
{
    public class BootstrapState : IGameState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISaveService _saveService;
        private readonly ISettingsService _settingsService;

        public BootstrapState(IGameStateMachine gameStateMachine, ISaveService saveService, ISettingsService settingsService)
        {
            _gameStateMachine = gameStateMachine;
            _saveService = saveService;
            _settingsService = settingsService;
        }

        public async UniTask Enter()
        {
            if (!_saveService.HasKey("FirstLoadCompleted"))
                OnFirstLoad();

            await SceneManager.LoadSceneAsync("MenuScene").ToUniTask();

            _gameStateMachine.Enter<MenuState>();
        }

        public async UniTask OnExit()
        {
            await UniTask.Yield();
        }

        private void OnFirstLoad()
        {
            _saveService.SetInt("FirstLoadCompleted", 1);

            _settingsService.SetMusicVolume(1);
            _settingsService.SetSFXVolume(1);

            _saveService.Save();
        }
    }
}
