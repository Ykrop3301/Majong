using System.Collections;
using UnityEngine;

namespace MajongGame.Gameplay.Tiles
{
    public class TileColorChanger
    {
        public bool IsChangingColor { get; private set; } = false;

        private readonly Renderer _renderer;
        private readonly TileDTO _tile;
        private readonly Color _activeColor = Color.white;
        private readonly Color _inactiveColor = Color.gray;
        private readonly MonoBehaviour _coroutineRunner;

        private const float CHANGE_DURATION = 0.2f;

        public TileColorChanger(Renderer renderer, TileDTO tile, MonoBehaviour coroutineRunner)
        {
            _renderer = renderer;
            _tile = tile;

            Subscribe();
            _coroutineRunner = coroutineRunner;
        }

        private void Subscribe()
        {
            _tile.ActiveChanged += OnTileActiveChanged;
            _tile.Dead += Unsubscribe;
        }

        private void Unsubscribe()
        {
            _tile.ActiveChanged -= OnTileActiveChanged;
            _tile.Dead -= Unsubscribe;
        }

        private void OnTileActiveChanged(bool active)
        {
            Color newColor = active ? _activeColor : _inactiveColor;

            _coroutineRunner.StartCoroutine(ChangeColorSmoothTo(newColor));
        }

        private IEnumerator ChangeColorSmoothTo(Color targetColor)
        {
            IsChangingColor = true;
            Color startColor = _renderer.material.color;
            float timeElapsed = 0f;

            while (timeElapsed < CHANGE_DURATION)
            {
                _renderer.material.color = Color.Lerp(startColor, targetColor, timeElapsed / CHANGE_DURATION);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            _renderer.material.color = targetColor;
            IsChangingColor = false;
        }
    }
}
