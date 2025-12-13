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
        private Dictionary<Vector3, Tile> _tilesPointsTiles = new Dictionary<Vector3, Tile>();
        public int FreePointsCount { get; private set; }
        private Vector3 _tileSize;
        private PopupsHolder _popupsHolder;
        private int _movingTiles = 0;
        private List<Vector3> _tilesPointsPositions;
        private Vector3 _debugPoint;

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

                if (position.x > -0.1 && position.x < 0.1f)
                    position.x = 0;

                _tilesPointsTiles.Add(position, null);
            }
            _tilesPointsPositions = _tilesPointsTiles.Keys.ToList();
            FreePointsCount = _tilesCount;
        }

        public bool CanAddTile() =>
            FreePointsCount > 0;


        public bool TryAddTile(Tile tile)
        {
            if (FreePointsCount > 0 & tile.TileDTO.IsActive)
            {
                GlobalVariablesController.CanClickTiles = false;

                List<Vector3> sameTilesPositions = _tilesPointsPositions
                    .Where(x => _tilesPointsTiles[x] != null && _tilesPointsTiles[x].TileDTO.Sprite == tile.TileDTO.Sprite)
                    .ToList();

                Vector3 newPoint;

                if (sameTilesPositions.Count > 0)
                {
                    Vector3 rightmostPoint = sameTilesPositions.Last();

                    int pointIndex = _tilesPointsPositions.IndexOf(rightmostPoint);

                    if (pointIndex + 1 > _tilesPointsPositions.Count)
                    {
                        Debug.Log(pointIndex);
                    }

                    newPoint = _tilesPointsTiles.Keys.ToList()[pointIndex + 1];
                }
                else
                {
                    newPoint = _tilesPointsTiles
                        .Where(x => x.Value == null)
                        .Select(x => x.Key)
                        .First();
                }

                _debugPoint = newPoint;
                

                FreePointsCount--;
                MoveTile(tile, newPoint);

                return true;
            }
            return false;
        }

        private void MoveTile(Tile tile, Vector3 point)
        {
            _movingTiles++;

            if (_tilesPointsTiles[point] != null)
            {
                Tile oldTile = _tilesPointsTiles[point];
                _tilesPointsTiles[point] = tile;

                int pointIndex = _tilesPointsPositions.IndexOf(point);
                Vector3 newPoint = _tilesPointsPositions[pointIndex + 1];

                MoveTile(oldTile, newPoint);
            }
            else _tilesPointsTiles[point] = tile;

            _movingTiles--;

            if (_movingTiles == 0)
            {
                foreach (Vector3 tilePoint in _tilesPointsPositions)
                {
                    if (_tilesPointsTiles[tilePoint] == null)
                        continue;

                    _tilesPointsTiles[tilePoint].TileDTO.Transform.DOMove(tilePoint, _tileMovingDuration);
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
            List<Vector3> sameTilesPositions = new List<Vector3>();

            foreach (Vector3 point in _tilesPointsPositions)
            {
                if (_tilesPointsTiles[point] == null)
                    continue;

                if (sameTilesPositions.Count > 0 && _tilesPointsTiles[point].TileDTO.Sprite == _tilesPointsTiles[sameTilesPositions.Last()].TileDTO.Sprite)
                {
                    sameTilesPositions.Add(point);

                    if (sameTilesPositions.Count == 3)
                    {
                        StartCoroutine(KillTiles(sameTilesPositions));
                        return;
                    }
                }
                else
                {
                    sameTilesPositions.Clear();
                    sameTilesPositions.Add(point);
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

        private IEnumerator KillTiles(List<Vector3> tilesPositions)
        {
            _audioSource.Play();

            Vector3 righterPoint = tilesPositions.Last();
            foreach (Vector3 point in tilesPositions)
            {
                var tile = _tilesPointsTiles[point];
                _tilesPointsTiles[point] = null;

                tile.Die();
                FreePointsCount++;
                yield return new WaitForSeconds(0.1f);
            }
            FillEmptyPoints(tilesPositions);
        }

        private void FillEmptyPoints(List<Vector3> emptyPoints)
        {
            List<Tile> tilesToMoveBack = new List<Tile>();

            List<Vector3> righterTilesPositions = _tilesPointsPositions
                .Where(x => x.x > emptyPoints.Last().x && _tilesPointsTiles[x] != null)
                .ToList();

            int tempPointIndex = _tilesPointsPositions.IndexOf(emptyPoints[0]);
            foreach (Vector3 point in righterTilesPositions)
            {
                Tile tile = _tilesPointsTiles[point];
                _tilesPointsTiles[point] = null;

                _tilesPointsTiles[_tilesPointsPositions[tempPointIndex]] = tile;

                tile.TileDTO.Transform.DOMove(_tilesPointsPositions[tempPointIndex], _tileMovingDuration);
                tempPointIndex++;
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