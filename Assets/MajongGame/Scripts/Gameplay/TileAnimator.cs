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
        private readonly AudioSource _audioSource;

        public TileAnimator(Transform transform, AudioSource audioSource)
        {
            _transform = transform;
            _originalRotation = _transform.eulerAngles;
            _yRotationCantTouch = _originalRotation.y + 7;
            _audioSource = audioSource;
        }

        public IEnumerator ShowTile()
        {
            _audioSource.pitch = Random.Range(0.5f, 0.7f);
            _audioSource.Play();
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

        public IEnumerator ChangeColor()
        {
            yield return _transform.DOScale(1.1f, AnimationDuration / 1.5f).From(1f).WaitForCompletion();
            yield return _transform.DOScale(1f, AnimationDuration / 1.5f).WaitForCompletion();
        }
    }
}