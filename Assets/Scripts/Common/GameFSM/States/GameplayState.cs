using Common.AssetsProvider;
using Common.Curtain;
using Common.Save;
using Cysharp.Threading.Tasks;
using Gameplay;
using Gameplay.Tiles;
using GameTemplate.Configs;
using UnityEngine;

namespace Common.GameFSM
{
    public class GameplayState : IGameState
    {
        private readonly ICurtain _curtain;
        private readonly IAssetsProvider _assetsProvider;
        private readonly ISaveService _saveService;


        public GameplayState(ICurtain curtain, IAssetsProvider assetsProvider, ISaveService saveService)
        {
            _curtain = curtain;
            _assetsProvider = assetsProvider;
            _saveService = saveService;
        }
        public async UniTask Enter()
        {
            GameplayOrchestrator gameplayOrchestrator = await Prepare();

            await _curtain.Hide();

            gameplayOrchestrator.StartGame();
        }

        private async UniTask<GameplayOrchestrator> Prepare()
        {
            string currentLocationId = _saveService.GetString("CurrentLocation");
            string tilePrefabName = _saveService.GetString("CurrentTilePrefabName");

            UniTask<GameObject> tileTask = _assetsProvider.LoadAsync<GameObject>(tilePrefabName);
            LocationConfig location = await _assetsProvider.LoadAsync<LocationConfig>(currentLocationId);
         
            if (!_saveService.HasKey($"{location.Id}.Current"))
                _saveService.SetInt($"{location.Id}.Current", 0);

            LevelConfig level = location.Levels[_saveService.GetInt($"{location.Id}.Current")];

            GameObject tilePrefab = await tileTask;
            LevelPreparer levelPreparer = new LevelPreparer(tilePrefab.GetComponent<Tile>());

            return await levelPreparer.Prepare(location, level);
        }

        public async UniTask OnExit()
        {
            await _curtain.Show();
        }
    }
}
