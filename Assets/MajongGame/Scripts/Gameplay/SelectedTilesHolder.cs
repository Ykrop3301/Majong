using DG.Tweening;
using MajongGame.Common;
using MajongGame.Common.PopupSystem;
using MajongGame.Common.PopupSystem.PopupVariants;
using MajongGame.Gameplay.Tiles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace MajongGame.Gameplay
{
    [RequireComponent(typeof(BoxCollider))]
    public class SelectedTilesHolder : MonoBehaviour
    {
        [SerializeField] private int _tilesCount = 7;
        [SerializeField] private float _tileMovingDuration = 0.5f;
        [SerializeField] private AudioSource _audioSource;

        private BoxCollider _boxCollider;
        private Dictionary<Vector3, Tile> _tilesPoints = new Dictionary<Vector3, Tile>();
        public int FreePointsCount { get; private set; }
        private Vector3 _tileSize;
        private PopupsHolder _popupsHolder;
        private int _movingTiles = 0;

        [Inject]
        private void Construct(PopupsHolder popupsHolder)
        {
            _popupsHolder = popupsHolder;
        }

        private void Start()
        {
            _boxCollider = GetComponent<BoxCollider>();
            Tile tilePrefab = Resources.Load<Tile>($"Prefabs/Tiles/{PlayerPrefs.GetString("TilePrefabName")}");
            BoxCollider tileCollider = tilePrefab.GetComponent<BoxCollider>();
            _tileSize = tileCollider.size;

            CreateTilesPoints();
        }

        private void CreateTilesPoints()
        {
            Vector3 startPostion = _boxCollider.bounds.center;
            startPostion.y = _boxCollider.bounds.max.y + (_tileSize.y / 2);
            startPostion.x = (_tileSize.x / 2f) * -(_tilesCount - 1);

            for (int i = 0; i < _tilesCount; i++)
            {
                Vector3 position = startPostion;
                position.x += i * _tileSize.x;

                _tilesPoints.Add(position, null);
            }

            FreePointsCount = _tilesCount;
        }

        public bool CanAddTile() =>
            FreePointsCount > 0;


        public bool TryAddTile(Tile tile)
        {
            if (FreePointsCount > 0 & tile.TileDTO.IsActive)
            {
                GlobalVariablesController.CanClickTiles = false;

                List<Vector3> sameTiles = _tilesPoints
                    .Where(x => x.Value != null && x.Value.TileDTO.Sprite == tile.TileDTO.Sprite)
                    .Select(x => x.Key)
                    .ToList();

                Vector3 newPoint;

                if (sameTiles.Count > 0)
                {
                    Vector3 rightmostPoint = sameTiles.Last();

                    newPoint = rightmostPoint;
                    newPoint.x += _tileSize.x;
                }
                else
                {
                    newPoint = _tilesPoints
                        .Where(x => x.Value == null)
                        .Select(x => x.Key)
                        .First();
                }

                if (!_tilesPoints.ContainsKey(newPoint))
                    throw new System.Exception($"Error in calculating a new point {newPoint}");

                FreePointsCount--;
                MoveTile(tile, newPoint);

                return true;
            }
            return false;
        }

        private void MoveTile(Tile tile, Vector3 point)
        {
            if (!_tilesPoints.ContainsKey(point))
                throw new System.Exception($"Point {point} not contains.");

            _movingTiles++;

            if (_tilesPoints[point] != null)
            {
                Tile oldTile = _tilesPoints[point];
                _tilesPoints[point] = tile;

                MoveTile(oldTile, new Vector3(point.x + _tileSize.x, point.y, point.z));
            }
            else _tilesPoints[point] = tile;

            _movingTiles--;

            if (_movingTiles == 0)
            {
                foreach (Vector3 tilePoint in _tilesPoints.Keys)
                {
                    if (_tilesPoints[tilePoint] == null)
                        continue;

                    _tilesPoints[tilePoint].TileDTO.Transform.DOMove(tilePoint, _tileMovingDuration);
                }
                StartCoroutine(WaitAndCheckEmptyPointsCoroutine());
            }
        }

        private IEnumerator WaitAndCheckEmptyPointsCoroutine()
        {
            yield return new WaitForSeconds(_tileMovingDuration);

            CheckThreeTiles();
            CheckEndGame();
        }

        private void CheckThreeTiles()
        {
            List<Tile> sameTiles = new List<Tile>();

            foreach (Tile tile in _tilesPoints.Values)
            {
                if (tile == null) continue;

                if (sameTiles.Count > 0 && tile.TileDTO.Sprite == sameTiles.Last().TileDTO.Sprite)
                {
                    sameTiles.Add(tile);

                    if (sameTiles.Count == 3)
                    {
                        StartCoroutine(KillTiles(sameTiles));
                        return;
                    }
                }
                else
                {
                    sameTiles.Clear();
                    sameTiles.Add(tile);
                }
            }

            GlobalVariablesController.CanClickTiles = true;
        }

        private void CheckEndGame()
        {
            if (FreePointsCount == 0)
            {
                LosePopup popup = _popupsHolder.GetPopup<LosePopup>();
                FreePointsCount = _tilesCount;
                popup.Show();
            }
        }

        private IEnumerator KillTiles(List<Tile> tiles)
        {
            _audioSource.Play();

            Vector3 righterPoint = tiles.Last().TileDTO.Transform.position;
            foreach (Tile tile in tiles)
            {
                _tilesPoints[tile.TileDTO.Transform.position] = null;

                tile.Die();
                FreePointsCount++;
                yield return new WaitForSeconds(0.1f);
            }
            FillEmptyPoints(righterPoint);
        }

        private void FillEmptyPoints(Vector3 righterPoint)
        {
            List<Tile> tilesToMoveBack = new List<Tile>();

            List<KeyValuePair<Vector3, Tile>> righterTiles = _tilesPoints
                .Where(x => x.Key.x > righterPoint.x && x.Value != null)
                .ToList();

            foreach (KeyValuePair<Vector3, Tile> pair in righterTiles)
            {
                _tilesPoints[pair.Key] = null;

                Vector3 pointToMove = pair.Key;
                pointToMove.x -= 3 * _tileSize.x;

                _tilesPoints[pointToMove] = pair.Value;
                pair.Value.TileDTO.Transform.DOMove(pointToMove, _tileMovingDuration);
            }

            StartCoroutine(WaitTileMovingCoroutine());
        }

        private IEnumerator WaitTileMovingCoroutine()
        {
            yield return new WaitForSeconds(_tileMovingDuration * 1.2f);
            GlobalVariablesController.CanClickTiles = true;
        }
    }
}