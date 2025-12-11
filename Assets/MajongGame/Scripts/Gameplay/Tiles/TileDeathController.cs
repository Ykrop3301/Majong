using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace MajongGame.Gameplay.Tiles
{
    public class TileDeathController
    {
        private readonly TileDTO _tile;
        private readonly ParticleSystem _particleSystem;
        private readonly MonoBehaviour _coroutineRunner;
        private readonly TileAnimator _tileAnimator;

        public TileDeathController(TileDTO tile, ParticleSystem particleSystem, MonoBehaviour coroutineRunner, TileAnimator tileAnimator)
        {
            _tile = tile;
            _particleSystem = particleSystem;
            _coroutineRunner = coroutineRunner;
            _tileAnimator = tileAnimator;

            _tile.Dead += OnTileDead;
        }

        private void OnTileDead()
        {
            _tile.Dead -= OnTileDead;

            _coroutineRunner.StartCoroutine(DieCoroutine());
        }

        private IEnumerator DieCoroutine()
        {
            _particleSystem.transform.parent = null;
            yield return new WaitUntil(() => !_tileAnimator.IsPlaying);

            _particleSystem.Play();
            yield return new WaitUntil(() => _particleSystem.isStopped);
            GameObject.Destroy(_particleSystem.gameObject);

            _tile.Transform.DOKill();
            GameObject.Destroy(_tile.Transform.gameObject);
        }
    }
}
