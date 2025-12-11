using MajongGame.Common;
using MajongGame.Common.LevelSystem;
using MajongGame.Common.PopupSystem;
using MajongGame.Configs.Level;
using MajongGame.Gameplay.Tiles;
using MajongGame.Gameplay.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MajongGame.Gameplay.Level
{
    public class LevelPreparer
    {
        private readonly CoroutineRunner _coroutineRunner;
        private readonly TilesSpawner _tilesSpawner;
        private readonly TileSpriteRandomizer _spriteRandomizer;
        private readonly UnselectedTilesHolder _unselectedTilesHolder;
        private readonly SceneChanger _sceneChanger;
        private readonly BackgroundHolder _backgroundHolder;
        private readonly Tile _tilePrefab;

        public LevelPreparer(CoroutineRunner coroutineRunner, ILevelsController levelsController, PopupsHolder popupsHolder, SceneChanger sceneChanger)
        {
            _coroutineRunner = coroutineRunner;

            _spriteRandomizer = new TileSpriteRandomizer();
            _tilesSpawner = new TilesSpawner();
            _unselectedTilesHolder = new UnselectedTilesHolder(popupsHolder, levelsController);
            _sceneChanger = sceneChanger;

            _backgroundHolder = GameObject.Instantiate(Resources.Load<BackgroundHolder>("Prefabs/UI/BackgroundCanvas"), null);
            _tilePrefab = Resources.Load<Tile>($"Prefabs/Tiles/{PlayerPrefs.GetString("TilePrefabName")}");
        }

        public void PrepareLevel(LevelLocationConfig location, int id)
        {
            GlobalVariablesController.LevelPreparing = true;

            LevelConfig levelConfig = location.GetLevel(id);

            _backgroundHolder.SetBG(location.Background);

            List<Tile> tiles = _tilesSpawner.SpawnLevel(levelConfig, _tilePrefab);
            _unselectedTilesHolder.SetTiles(tiles);
            _spriteRandomizer.Randomize(location.TilePictures, tiles);
            _coroutineRunner.StartCoroutine(SmoothShowTilesCoroutine(tiles));
        }

        private IEnumerator SmoothShowTilesCoroutine(List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
            {
                tile.Show();
                yield return new WaitForSeconds(0.02f);
            }

            yield return new WaitUntil(() => !tiles.Last().IsPlayingAnimation);

            CheckTilesActivity(tiles);
            GlobalVariablesController.LevelPreparing = false;
        }

        private void CheckTilesActivity(List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
            {
                tile.CheckCoveringTiles();
            }
        }
    }
}
