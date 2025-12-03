using UnityEngine;

namespace MajongGame.Gameplay
{
    public class TileColorChanger
    {
        public bool IsChangingColor { get; private set; } = false;

        private readonly Renderer _renderer;
        private readonly float _duration;

        public TileColorChanger(Renderer renderer, float duration)
        {
            _renderer = renderer;
            _duration = duration;
        }

        public System.Collections.IEnumerator ChangeColorTo(Color targetColor)
        {
            IsChangingColor = true;
            Color startColor = _renderer.material.color;
            float timeElapsed = 0f;

            while (timeElapsed < _duration)
            {
                _renderer.material.color = Color.Lerp(startColor, targetColor, timeElapsed / _duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            _renderer.material.color = targetColor;
            IsChangingColor = false;
        }
    }
}
