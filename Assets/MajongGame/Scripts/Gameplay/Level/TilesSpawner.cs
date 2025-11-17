using MajongGame.Gameplay.Configs;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace MajongGame.Gameplay.Level
{
    public class TilesSpawner
    {
        public List<GameObject> SpawnLevel(LevelConfig levelConfig, GameObject tilePrefab)
        {
            BoxCollider tileColider = tilePrefab.GetComponent<BoxCollider>();

            List<GameObject> newTiles = new List<GameObject>();

            newTiles.AddRange(SpawnTiles(tilePrefab, levelConfig.FirstLeayerPattern, tileColider.size, 0));
            newTiles.AddRange(SpawnTiles(tilePrefab, levelConfig.SecondLeayerPattern, tileColider.size, 1));
            newTiles.AddRange(SpawnTiles(tilePrefab, levelConfig.ThirdLeayerPattern, tileColider.size, 2));

            return newTiles;
        }

        private List<GameObject> SpawnTiles(GameObject tilePrefab, string layerPattern, Vector3 tileSize, int layerNum)
        {
            Transform parent = new GameObject($"Layer {layerNum + 1}").transform;
            List<GameObject> newTiles = new List<GameObject>();

            Vector2[,] tileMap = GetTileMap(layerPattern, tileSize.z, tileSize.x);
            int tileMapSize = tileMap.GetLength(0);

            int collumn = 0, row = 0;
            foreach (char c in layerPattern)
            {
                if (row == tileMapSize) break;

                if (c == 'P')
                {
                    Vector3 position = new Vector3(tileMap[collumn, row].x, tileSize.y * layerNum, tileMap[collumn, row].y);

                    GameObject tile = GameObject.Instantiate(tilePrefab, position, Quaternion.identity);
                    tile.transform.SetParent(parent);

                    Debug.Log((tileMap[collumn, row].x, tileMap[collumn, row].y));

                    newTiles.Add(tile);
                    collumn++;
                }
                else if (c == '#')
                {
                    collumn++;
                }
                else
                {
                    row++;
                    collumn = 0;
                }
            }

            return newTiles;
        }

        private Vector2[,] GetTileMap(string layerPattern, float prefabLength, float prefabWidth)
        {
            int size = 0;

            foreach (char c in layerPattern)
            {
                if (c != 'P' && c != '#') break;
                size++;
            }

            Vector2[,] tileMap = new Vector2[size, size];

            float x, y;

            float startX = (prefabWidth / 2) * -size;
            float startY = (prefabLength / 2) * size;  

            int currentXId = 0, currentYId = 0;

            foreach (char c in layerPattern)
            {
                if (currentXId == size)
                {
                    currentYId++;
                    currentXId = 0;
                }

                if (currentYId == size) break;

                x = startX + currentXId * prefabWidth;
                y = startY - currentYId * prefabLength;

                tileMap[currentXId, currentYId] = new Vector2(x, y);
                
                currentXId++;
            }

            return tileMap;
        }
    }
}
