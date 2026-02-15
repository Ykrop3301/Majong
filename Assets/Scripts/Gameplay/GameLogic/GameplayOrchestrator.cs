using Gameplay.Level;
using Gameplay.Tiles;
using Gameplay;
using GameTemplate.Configs;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Gameplay
{
    public class GameplayOrchestrator
    {
        private int _tilesCount;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        private readonly SpriteRandomizer _spriteRandomizer = new SpriteRandomizer();
        private TilesSelector _tilesSelector;
        private SelectedTilesHolder _tilesHolder;

        public GameplayOrchestrator()
        {

        }

        public void StartGame()
        {
            _tilesSelector.CanClick = true;
            Debug.Log("Game started!");
        }

        public void Prepare(List<Tile> tiles, LocationConfig locationConfig)
        {
            SubscripeToTiles(tiles);
            _spriteRandomizer.Randomize(locationConfig.AvaibleTileSprites, tiles);
            _tilesSelector = new TilesSelector(_tilesHolder);
        }

        private void SubscripeToTiles(List<Tile> tiles)
        {
            _tilesCount = tiles.Count;

            foreach (ITakeable tile in tiles)
            {
                tile.Taked
                    .Subscribe(_ => OnTileTaked())
                    .AddTo(_disposables);
            }
        }

        private void OnTileTaked()
        {
            _tilesCount--;
            if (_tilesCount <= 0)
            {
                _disposables.Dispose();
            }
        }
    }
}
