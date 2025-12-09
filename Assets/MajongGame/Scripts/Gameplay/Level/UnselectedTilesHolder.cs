using MajongGame.Common.LevelSystem;
using MajongGame.Common.PopupSystem;
using MajongGame.Common.PopupSystem.PopupVariants;
using System.Collections.Generic;
using UnityEngine;

namespace MajongGame.Gameplay.Level
{
    public class UnselectedTilesHolder
    {
        private List<Tile> _tiles;
        private readonly PopupsHolder _popupsHolder;
        private readonly ILevelsController _levelsController;

        public UnselectedTilesHolder(PopupsHolder popupsHolder, ILevelsController levelsController)
        {
            _popupsHolder = popupsHolder;
            _levelsController = levelsController;
        }

        public void SetTiles(List<Tile> tiles)
        {
            _tiles = tiles;

            foreach (Tile tile in _tiles)
            {
                tile.Died += OnTileDestroyed;
            }
        }

        private void OnTileDestroyed(Tile tile)
        {
            _tiles.Remove(tile);

            if (_tiles.Count == 0)
            {
                _levelsController.UnlockNextLevel();
                _popupsHolder.GetPopup<WinPopup>().Show();
            }
        }
    }
}
