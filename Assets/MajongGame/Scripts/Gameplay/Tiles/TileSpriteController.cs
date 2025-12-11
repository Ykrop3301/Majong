using UnityEngine;

namespace MajongGame.Gameplay.Tiles
{
    public class TileSpriteController
    {
        private readonly SpriteRenderer _spriteRenderer;
        private readonly TileDTO _tile;

        public TileSpriteController(SpriteRenderer spriteRenderer, TileDTO tile)
        {
            _spriteRenderer = spriteRenderer;
            _tile = tile;
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
            _tile.Sprite = sprite;
        }
    }
}
