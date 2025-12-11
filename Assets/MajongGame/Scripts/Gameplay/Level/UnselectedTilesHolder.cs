using MajongGame.Common.LevelSystem;
using MajongGame.Common.PopupSystem;
using MajongGame.Common.PopupSystem.PopupVariants;
using MajongGame.Gameplay.Tiles;
using System.Collections.Generic;

namespace MajongGame.Gameplay.Level
{
    public class UnselectedTilesHolder
    {
        private readonly PopupsHolder _popupsHolder;
        private readonly ILevelsController _levelsController;

        private List<Tile> _unselectedTiles;
        private int _unselectedTilesCount;

        public UnselectedTilesHolder(PopupsHolder popupsHolder, ILevelsController levelsController)
        {
            _popupsHolder = popupsHolder;
            _levelsController = levelsController;
        }

        public void SetTiles(List<Tile> tiles)
        {
            _unselectedTiles = tiles;
            _unselectedTilesCount = _unselectedTiles.Count;

            foreach (Tile tile in _unselectedTiles)
            {
                tile.TileDTO.Dead += OnTileDestroyed;
            }
        }

        private void OnTileDestroyed()
        {
            _unselectedTilesCount--;

            if (_unselectedTilesCount == 0)
            {
                ForgetAllTiles();
                _levelsController.UnlockNextLevel();
                _popupsHolder.GetPopup<WinPopup>().Show();
            }
        }

        private void ForgetAllTiles()
        {
            foreach (Tile tile in _unselectedTiles)
            {
                tile.TileDTO.Taked -= OnTileDestroyed;
            }
        }
    }
}
