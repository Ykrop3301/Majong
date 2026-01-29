using DG.Tweening;
using UnityEngine;

namespace Menu
{
    public class ShufflingObjects : MonoBehaviour
    {
        private Transform _transform;

        private void Start()
        {
            _transform ??= GetComponent<Transform>();

            Vector3 rotation = _transform.rotation.eulerAngles;
            rotation.y += Random.Range(1f, 10f);
            float duration = Random.Range(3f, 7f);

            _transform
                .DORotate(rotation, duration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);

            float scale = _transform.localScale.x * Random.Range(1f, 1.2f);
            float scaleDuration = Random.Range(3f, 7f);

            _transform
                .DOScale(scale, scaleDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);

        }
    }
}