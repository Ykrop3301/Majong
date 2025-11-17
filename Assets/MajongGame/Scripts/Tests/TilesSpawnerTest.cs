using MajongGame.Gameplay.Configs;
using MajongGame.Gameplay.Level;
using System.Collections.Generic;
using UnityEngine;

namespace MajongGame.Tests
{
    public class TilesSpawnerTest : MonoBehaviour
    {
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private GameObject _tilePrefab;
        private List<GameObject> _tiles;

        private void Start()
        {
            TilesSpawner tilesSpawner = new TilesSpawner();
            _tiles = tilesSpawner.SpawnLevel(_levelConfig, _tilePrefab);
        }
    }
}