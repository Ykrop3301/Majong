using Common.AssetsProvider;
using Common.Save;
using Cysharp.Threading.Tasks;
using Gameplay.Tiles;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace Gameplay.Level
{
    public class TilesPool
    {
        private List<Tile> _spawnedTiles;
        private Tile _tilePrefab;

        public TilesPool(Tile tilePrefab)
        {
            _tilePrefab = tilePrefab;
        }

        public async UniTask<List<Tile>> GetTiles(int count)
        {
            if (count <= 0)
                throw new System.Exception("Invalid count;");

            if (_spawnedTiles == null)
            {
                _spawnedTiles = new List<Tile>();
            }
            List<Tile> newObjects = new List<Tile>();

            int spawnedCount = _spawnedTiles.Count;

            if (spawnedCount >= count)
            {
                for (int i = 0; i < count; i++)
                {
                    _spawnedTiles[i].gameObject.SetActive(true);
                    _spawnedTiles[i].ResetValues();
                    newObjects.Add(_spawnedTiles[i]);
                }

                for (int i = count; i < spawnedCount; i++)
                {
                    _spawnedTiles[i].gameObject.SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < spawnedCount; i++)
                {
                    Tile newObject;
                    newObject = _spawnedTiles[i];
                    newObject.ResetValues();
                    newObject.gameObject.SetActive(true);
                    
                    newObjects.Add(newObject);
                }
                if (count >= spawnedCount)
                {
                    Tile[] instantiatedObjects = await GameObject.InstantiateAsync<Tile>(_tilePrefab, count - spawnedCount);
                    _spawnedTiles.AddRange(instantiatedObjects);
                    newObjects.AddRange(instantiatedObjects);
                }
                
            }

            return newObjects;
        }
    }
}
