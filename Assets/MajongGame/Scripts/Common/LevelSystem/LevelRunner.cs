using MajongGame.Configs.Level;
using MajongGame.Gameplay;
using MajongGame.Gameplay.Level;
using MajongGame.LevelSystem;
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

        public LevelRunner(CoroutineRunner coroutineRunner, ILevelsController levelsController)
        {
            _coroutineRunner = coroutineRunner;
            _levelsController = levelsController;
            _spriteRandomizer = new TileSpriteRandomizer();
            _tilesSpawner = new TilesSpawner();
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

            if (!PlayerPrefs.HasKey("TilePrefabName"))
                PlayerPrefs.SetString("TilePrefabName", "DefaultTile");

            Tile tilePrefab = Resources.Load<Tile>($"Prefabs/Tiles/{PlayerPrefs.GetString("TilePrefabName")}");

            List<Tile> tiles = _tilesSpawner.SpawnLevel(levelConfig, tilePrefab);

            _spriteRandomizer.Randomize(_levelsController.CurrentLevel.location.TilePictures, tiles);
        }
    }
}
