using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace MajongGame.Gameplay.Tiles
{
    public class TileAnimator
    {
        public bool IsPlaying { get; private set; } = false;

        private readonly TileDTO _tile;
        private readonly MonoBehaviour _coroutineCunner;
        private readonly float _yRotationCantTouch;
        private readonly Vector3 _originalRotation;

        private const float ANIMATION_DURATION = 0.3f;

        public TileAnimator(TileDTO tile, MonoBehaviour coroutineRunner)
        {
            _tile = tile;
            _coroutineCunner = coroutineRunner;
            _originalRotation = _tile.Transform.eulerAngles;
            _yRotationCantTouch = _originalRotation.y + 7;

            Subscribe();
        }

        private void Subscribe()
        {
            _tile.Dead += Unsubscribe;
            _tile.Dead += PlayTileDead;
            _tile.Taked += PlayOnTileTaked;
            _tile.ActiveChanged += PlayOnTileActiveChanged;
        }

        private void Unsubscribe()
        {
            _tile.Dead -= Unsubscribe;
            _tile.Dead -= PlayTileDead;
            _tile.Taked -= PlayOnTileTaked;
            _tile.ActiveChanged -= PlayOnTileActiveChanged;
        }

        public void PlayOnTileSpawned()
        {
            _coroutineCunner.StartCoroutine(TileSpawned());
        }

        private IEnumerator TileSpawned()
        {
            IsPlaying = true;
            yield return _tile.Transform.DOScale(1.1f, ANIMATION_DURATION).From(0f).WaitForCompletion();
            yield return _tile.Transform.DOScale(1f, ANIMATION_DURATION / 3f).WaitForCompletion();
            IsPlaying = false;
        }

        public void PlayOnCantTakeTile()
        {
            _coroutineCunner.StartCoroutine(CantTake());
        }

        private IEnumerator CantTake()
        {
            IsPlaying = true;
            yield return _tile.Transform.DORotate(new Vector3(_originalRotation.x, _yRotationCantTouch, _originalRotation.z), 0.3f / 2f, RotateMode.LocalAxisAdd)
                .From(_originalRotation)
                .SetEase(Ease.InOutSine)
                .SetLoops(2, LoopType.Yoyo)
                .WaitForCompletion();

            yield return _tile.Transform.DORotate(_originalRotation, 0.1f).WaitForCompletion();
            IsPlaying = false;
        }

        private void PlayOnTileActiveChanged(bool active)
        {
            _coroutineCunner.StartCoroutine(ActiveChanged());
        }

        private IEnumerator ActiveChanged()
        {
            IsPlaying = true;
            yield return _tile.Transform.DOScale(1.1f, ANIMATION_DURATION / 1.5f).From(1f).WaitForCompletion();
            yield return _tile.Transform.DOScale(1f, ANIMATION_DURATION / 1.5f).WaitForCompletion();
            IsPlaying = false;
        }

        private void PlayOnTileTaked()
        {
            _coroutineCunner.StartCoroutine(TileTaked());
        }

        private IEnumerator TileTaked()
        {
            IsPlaying = true;
            yield return _tile.Transform.DOScale(1.1f, ANIMATION_DURATION / 1.5f).From(1f).WaitForCompletion();
            yield return _tile.Transform.DOScale(1f, ANIMATION_DURATION / 1.5f).WaitForCompletion();
            IsPlaying = false;
        }

        private void PlayTileDead()
        {
            _coroutineCunner.StartCoroutine(TileDead());
        }

        private IEnumerator TileDead()
        {
            yield return _tile.Transform.DOScale(0f, 0.2f).From(1f);
        }
    }
}