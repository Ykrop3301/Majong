using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MajongGame.Gameplay.Tiles
{
    public class TileCoverChecker
    {
        private readonly TileDTO _tile;
        private readonly Tile _originTile;
        private readonly BoxCollider _boxCollider;

        private List<Tile> _coveringTiles;
        private int _coveringTilesCount;

        public TileCoverChecker(TileDTO tile, Tile originTile)
        {
            _tile = tile;
            _originTile = originTile;
            _boxCollider = _tile.Transform.GetComponent<BoxCollider>();
        }

        public void CheckCoveringTiles()
        {
            _coveringTiles = GetCoveringTiles();

            _coveringTilesCount = _coveringTiles.Count;

            if (_coveringTilesCount > 0)
            {
                foreach (Tile coveringTile in _coveringTiles)
                {
                    coveringTile.TileDTO.Taked += OnCoveringTileTaked;
                }
            }
            else
            {
                _tile.SetActive(true);
            }
        }

        private List<Tile> GetCoveringTiles()
        {
            Vector3 center = _tile.Transform.TransformPoint(_boxCollider.center);
            Vector3 boxCenter = center + Vector3.up * (_boxCollider.size.y / 2);

            List<Tile> hits = Physics.OverlapBox(boxCenter, _boxCollider.size / 2.25f)
                .Where(x => x.TryGetComponent(out Tile tile) && tile != _originTile)
                .Select(x => x.GetComponent<Tile>())
                .ToList();

            return hits;
        }

        private void OnCoveringTileTaked()
        {
            _coveringTilesCount--;

            if (_coveringTilesCount == 0)
            {
                ForgetCoveringTiles();
                _tile.SetActive(true);
            }
        }

        private void ForgetCoveringTiles()
        {
            foreach (Tile tile in _coveringTiles)
            {
                tile.TileDTO.Taked -= OnCoveringTileTaked;
            }
        }
    }
}
