using MajongGame.Common.PopupSystem;
using MajongGame.Configs.Level;
using MajongGame.Gameplay;
using MajongGame.Gameplay.Level;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private readonly SceneChanger _sceneChanger;

        public LevelRunner(CoroutineRunner coroutineRunner, ILevelsController levelsController, PopupsHolder popupsHolder, SceneChanger sceneChanger)
        {
            _coroutineRunner = coroutineRunner;
            _levelsController = levelsController;

            _spriteRandomizer = new TileSpriteRandomizer();
            _tilesSpawner = new TilesSpawner();
            _unselectedTilesHolder = new UnselectedTilesHolder(popupsHolder, levelsController);
            _sceneChanger = sceneChanger;
        }

        public void RunLevel(LevelLocationConfig location, int id)
        {
            if (SceneManager.GetActiveScene().name != "GameplayScene")
            {
                _sceneChanger.LoadScene("GameplayScene");
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
            GlobalVariablesController.OnLevelPreparing = true;

            LevelConfig levelConfig = _levelsController.CurrentLevel.location.GetLevel(_levelsController.CurrentLevel.levelId);
            Tile tilePrefab = Resources.Load<Tile>($"Prefabs/Tiles/{PlayerPrefs.GetString("TilePrefabName")}");

            List<Tile> tiles = _tilesSpawner.SpawnLevel(levelConfig, tilePrefab);
            _unselectedTilesHolder.SetTiles(tiles);

            _spriteRandomizer.Randomize(_levelsController.CurrentLevel.location.TilePictures, tiles);

            _coroutineRunner.StartCoroutine(SmoothShowTilesCoroutine(tiles));
        }

        private IEnumerator SmoothShowTilesCoroutine(List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
            {
                tile.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.02f);
            }

            yield return new WaitForSeconds(2 * tiles.Last().Animator.AnimationDuration);
            GlobalVariablesController.OnLevelPreparing = false;
        }
    }
}
