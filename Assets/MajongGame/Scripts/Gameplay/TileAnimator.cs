using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace MajongGame.Gameplay
{
    public class TileAnimator
    {
        private readonly Transform _transform;
        public float AnimationDuration { get; } = 0.3f;

        public TileAnimator(Transform transform)
        {
            _transform = transform;
        }

        public IEnumerator ShowTile()
        {
            yield return _transform.DOScale(1.1f, AnimationDuration).From(0f).WaitForCompletion();
            yield return _transform.DOScale(1f, AnimationDuration / 3f).WaitForCompletion();
        }
    }
}