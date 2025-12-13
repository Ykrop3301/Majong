using DG.Tweening;
using UnityEngine;

namespace MajongGame.Common
{
    public class RotationHandler : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _rotationDuration = 30f;

        private Vector3 _originalRotation;

        private void Start()
        {
            _originalRotation = _transform.localRotation.eulerAngles;
            Vector3 newRotation = _originalRotation;
            newRotation.y += 360;

            _transform.DOLocalRotate(newRotation, _rotationDuration, RotateMode.FastBeyond360).SetLoops(-1);
        }

        private void OnDisable()
        {
            _transform.DOKill();
        }
    }
}