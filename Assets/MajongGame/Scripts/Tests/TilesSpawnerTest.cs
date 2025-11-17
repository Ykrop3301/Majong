using MajongGame.Gameplay;
using MajongGame.Gameplay.Configs;
using MajongGame.Gameplay.Level;
using System.Collections.Generic;
using UnityEngine;

namespace MajongGame.Tests
{
    public class TilesSpawnerTest : MonoBehaviour
    {
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private Tile _tilePrefab;
        [SerializeField] private List<Sprite> _sprites;
        private List<Tile> _tiles;

        private void Start()
        {
            TilesSpawner tilesSpawner = new TilesSpawner();
            _tiles = tilesSpawner.SpawnLevel(_levelConfig, _tilePrefab);

            TileSpriteRandomizer spriteRandomizer = new TileSpriteRandomizer();

            spriteRandomizer.Randomize(_sprites, _tiles);
        }
    }
}