using MajongGame.Common.PopupSystem;
using MajongGame.Configs.Level;
using MajongGame.Gameplay;
using MajongGame.Gameplay.Level;
using MajongGame.Gameplay.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

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
                _coroutineRunner.StartCoroutine(WaitLoadSceneCoroutine(location, id));
            }
            else PrepareGame(location, id);
        }

        private IEnumerator WaitLoadSceneCoroutine(LevelLocationConfig location, int id)
        {
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "GameplayScene");
            PrepareGame(location, id);
        }

        private void PrepareGame(LevelLocationConfig location, int id)
        {
            GlobalVariablesController.OnLevelPreparing = true;

            LevelConfig levelConfig = location.GetLevel(id);

            var bgHolder = GameObject.Instantiate(Resources.Load<BackgroundHolder>("Prefabs/UI/BackgroundCanvas"), null);
            bgHolder.SetBG(location.Background);

            Tile tilePrefab = Resources.Load<Tile>($"Prefabs/Tiles/{PlayerPrefs.GetString("TilePrefabName")}");

            List<Tile> tiles = _tilesSpawner.SpawnLevel(levelConfig, tilePrefab);
            _unselectedTilesHolder.SetTiles(tiles);

            _spriteRandomizer.Randomize(location.TilePictures, tiles);

            _coroutineRunner.StartCoroutine(SmoothShowTilesCoroutine(tiles));
        }

        private IEnumerator SmoothShowTilesCoroutine(List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
            {
                tile.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.02f);
            }

            yield return new WaitForSeconds(tiles.Last().Animator.AnimationDuration);

            GlobalVariablesController.OnLevelPreparing = false;
            CheckTilesActivity(tiles);
        }

        private void CheckTilesActivity(List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
            {
                tile.CheckActive();
            }
        }
    }
}
