using DG.Tweening;
using UnityEngine;

namespace MajongGame.Common
{
    public class PulseHandler : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _sizeKoef = 1.1f;
        [SerializeField] private float _duration = 1f;

        private Vector3 _originalScale;

        private void Start()
        {
            _originalScale = _transform.localScale;
            Vector3 newScale = _originalScale * _sizeKoef;

            _transform.DOScale(newScale, _duration).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            _transform.DOKill();
        }
    }
}