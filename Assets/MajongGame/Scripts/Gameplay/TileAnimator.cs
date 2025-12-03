using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace MajongGame.Gameplay
{
    public class TileAnimator
    {
        private readonly Transform _transform;
        public float AnimationDuration { get; } = 0.3f;
        private readonly float _yRotationCantTouch;
        private readonly Vector3 _originalRotation;

        public TileAnimator(Transform transform)
        {
            _transform = transform;
            _originalRotation = _transform.eulerAngles;
            _yRotationCantTouch = _originalRotation.y + 7;
        }

        public IEnumerator ShowTile()
        {
            yield return _transform.DOScale(1.1f, AnimationDuration).From(0f).WaitForCompletion();
            yield return _transform.DOScale(1f, AnimationDuration / 3f).WaitForCompletion();
        }

        public IEnumerator CantTouch()
        {
            yield return _transform.DORotate(new Vector3(_originalRotation.x, _yRotationCantTouch, _originalRotation.z), 0.3f / 2f, RotateMode.LocalAxisAdd)
                .From(_originalRotation)
                .SetEase(Ease.InOutSine)
                .SetLoops(2, LoopType.Yoyo)
                .WaitForCompletion();

            yield return _transform.DORotate(_originalRotation, 0.1f).WaitForCompletion();
        }
    }
}