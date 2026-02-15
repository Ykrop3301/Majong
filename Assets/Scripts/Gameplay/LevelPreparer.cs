using Cysharp.Threading.Tasks;
using Gameplay.Level;
using Gameplay.Tiles;
using GameTemplate.Configs;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class LevelPreparer
    {
        private readonly TilesPool _tilesPool;
        private readonly SpawningMapGenerator _spawningMapGenerator;
        private readonly GameplayOrchestrator _gameplayOrchestrator;

        public LevelPreparer(Tile tilePrefab)
        {
            _tilesPool = new TilesPool(tilePrefab);
            _spawningMapGenerator = new SpawningMapGenerator(tilePrefab);
            _gameplayOrchestrator = new GameplayOrchestrator();
        }

        public async UniTask<GameplayOrchestrator> Prepare(LocationConfig locationConfig, LevelConfig levelConfig)
        {
            if (levelConfig.TilesCount % 3 != 0)
                throw new System.Exception("Количество тайлов должно быть кратно 3!");

            UniTask<List<Tile>> tilesTask = _tilesPool.GetTiles(levelConfig.TilesCount);
            List<Vector3> map = GetMap(levelConfig);

            List<Tile> tiles = await tilesTask;

            PositionTiles(map, tiles);
            _gameplayOrchestrator.Prepare(tiles, locationConfig);

            return _gameplayOrchestrator;
        }

        private List<Vector3> GetMap(LevelConfig levelConfig)
        {
            List<string> layers = new List<string>()
            {
                levelConfig.FirstLeayerPattern,
                levelConfig.SecondLeayerPattern,
                levelConfig.ThirdLeayerPattern
            };

            List<Vector3> map = new List<Vector3>();

            for (int layerNum = 0; layerNum < 3; layerNum++)
            {
                List<Vector3> newMap = _spawningMapGenerator.GetMap(layers[layerNum], layerNum);

                map.AddRange(newMap);
            }

            return map;
        }

        private void PositionTiles(List<Vector3> map, List<Tile> objects)
        {
            int currentIndex = 0;
            foreach (Vector3 pos in map)
            {
                objects[currentIndex++].Transform.position = pos;
            }
        }

    }
}
