using DG.Tweening;
using UnityEngine;

namespace Menu
{
    public class RotatingLoopCamera : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _circleDuration = 6f;

        private void Start()
        {
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.y += 360f;

            _transform
                .DORotate(rotation, _circleDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }

    }
}