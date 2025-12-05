using DG.Tweening;
using MajongGame.Common;
using MajongGame.Common.PopupSystem;
using MajongGame.Common.PopupSystem.PopupVariants;
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
        private bool _inCoroutine = false;
        private PopupsHolder _popupsHolder;

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

        public bool TryAddTile(Tile tile)
        {
            if (FreePointsCount > 0 & tile.IsActive)
            {
                List<Vector3> sameTiles = _tilesPoints
                    .Where(x => x.Value != null && x.Value.Sprite == tile.Sprite)
                    .Select(x => x.Key)
                    .ToList();

                Vector3 newPoint;

                if (sameTiles.Count > 0)
                {
                    Vector3 rightmostPoint = sameTiles.First();
                    foreach (Vector3 point in sameTiles)
                    {
                        if (point.x > rightmostPoint.x)
                            rightmostPoint = point;
                    }

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
                    throw new System.Exception("Error in calculating a new point");

                FreePointsCount--;
                MoveTile(tile, newPoint);

                return true;
            }
            else
            {
                if (!tile.IsActive)
                {
                    StartCoroutine(tile.Animator.CantTouch());
                }

                return false;
            }
        }

        private void MoveTile(Tile tile, Vector3 point)
        {
            GlobalVariablesController.CanClickTiles = false;

            if (!_tilesPoints.ContainsKey(point))
                throw new System.Exception("Point not contains.");

            if (_tilesPoints[point] != null)
                MoveTile(_tilesPoints[point], new Vector3(point.x + _tileSize.x, point.y, point.z));
            else _inCoroutine = true;
    
            _tilesPoints[point] = tile;
            tile.Transform.DOMove(new Vector3(point.x, point.y, point.z), _tileMovingDuration);
            
            if (!_inCoroutine)
                StartCoroutine(WaitAndCheckEmptyPointsCoroutine());
            else
            {
                StopAllCoroutines();
                StartCoroutine(WaitAndCheckEmptyPointsCoroutine());
            }
        }

        private IEnumerator WaitAndCheckEmptyPointsCoroutine()
        {
            yield return new WaitForSeconds(_tileMovingDuration);
            _inCoroutine = false;

            CheckThreeTiles();
            CheckEndGame();
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

        private void CheckThreeTiles()
        {
            List<Tile> sameTiles = new List<Tile>();

            foreach (Tile tile in _tilesPoints.Values)
            {
                if (tile == null) continue;

                if (sameTiles.Count > 0 && tile.Sprite == sameTiles.Last().Sprite)
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

        private IEnumerator KillTiles(List<Tile> tiles)
        {
            _audioSource.Play();

            Vector3 righterPoint = tiles.Last().Transform.position;
            foreach (Tile tile in tiles)
            {
                _tilesPoints[tile.Transform.position] = null;

                tile.DOKill();
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
                pair.Value.Transform.DOMove(pointToMove, _tileMovingDuration);
            }

            GlobalVariablesController.CanClickTiles = true;
        }
    }
}