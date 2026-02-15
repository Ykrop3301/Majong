using Gameplay.Tiles;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Level
{
    public class SpawningMapGenerator
    {
        private readonly BoxCollider _tileColider;

        public SpawningMapGenerator(Tile tilePrefab)
        {
            _tileColider = tilePrefab.GetComponent<BoxCollider>();
        }

        public List<Vector3> GetMap(string layerPattern, int layer)
        {
            int size = 0;

            foreach (char c in layerPattern)
            {
                if (c != 'P' && c != '#')
                    break;
                size++;
            }

            List<Vector3> tileMap = new List<Vector3>();

            float x, z;

            float startX = (_tileColider.size.x / 2) * (-size + 1);
            float startZ = (_tileColider.size.z / 2) * (size - 1);


            int currentXId = 0, currentZId = 0;

            foreach (char c in layerPattern)
            {
                if (currentXId == size)
                {
                    currentZId++;
                    currentXId = 0;
                    continue;
                }

                if (currentZId == size) break;

                if (c == 'P')
                {
                    x = startX + currentXId * _tileColider.size.x;
                    z = startZ - currentZId * _tileColider.size.z;

                    tileMap.Add(new Vector3(x, _tileColider.size.y * layer, z));
                }
                currentXId++;
            }

            return tileMap;
        }
    }
}
