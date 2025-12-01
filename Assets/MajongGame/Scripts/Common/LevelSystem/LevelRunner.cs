using MajongGame.Common.PopupSystem;
using MajongGame.Configs.Level;
using MajongGame.Gameplay;
using MajongGame.Gameplay.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MajongGame.Common.LevelSystem
{
    public class LevelRunner
    {
        private readonly ILevelsController _levelsController;
        private readonly CoroutineRunner _coroutineRunner;
        private readonly TilesSpawner _tilesSpawner;
        private readonly TileSpriteRandomizer _spriteRandomizer;
        private readonly UnselectedTilesHolder _unselectedTilesHolder;

        public LevelRunner(CoroutineRunner coroutineRunner, ILevelsController levelsController, PopupsHolder popupsHolder)
        {
            _coroutineRunner = coroutineRunner;
            _levelsController = levelsController;

            _spriteRandomizer = new TileSpriteRandomizer();
            _tilesSpawner = new TilesSpawner();
            _unselectedTilesHolder = new UnselectedTilesHolder(popupsHolder, levelsController);
        }

        public void RunLevel(LevelLocationConfig location, int id)
        {
            if (SceneManager.GetActiveScene().name != "GameplayScene")
            {
                SceneManager.LoadScene("GameplayScene");
                _coroutineRunner.StartCoroutine(WaitLoadSceneCoroutine());
            }
            else PrepareGame();
        }

        private IEnumerator WaitLoadSceneCoroutine()
        {
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "GameplayScene");
            PrepareGame();
        }

        private void PrepareGame()
        {
            LevelConfig levelConfig = _levelsController.CurrentLevel.location.GetLevel(_levelsController.CurrentLevel.levelId);

            

            Tile tilePrefab = Resources.Load<Tile>($"Prefabs/Tiles/{PlayerPrefs.GetString("TilePrefabName")}");

            List<Tile> tiles = _tilesSpawner.SpawnLevel(levelConfig, tilePrefab);
            _unselectedTilesHolder.SetTiles(tiles);

            _spriteRandomizer.Randomize(_levelsController.CurrentLevel.location.TilePictures, tiles);
        }
    }
}
