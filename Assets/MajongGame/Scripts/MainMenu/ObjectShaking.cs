using DG.Tweening;
using UnityEngine;

namespace MajongGame.MainMenu
{
    public class ObjectShaking : MonoBehaviour
    {
        [SerializeField] private float _shakingDuration = 2.5f;

        private void Start()
        {
            var transform = GetComponent<Transform>();
            var startRotation = transform.rotation.eulerAngles;

            transform.DORotate(startRotation + new Vector3(0f, 0f, Random.Range(1f, 5f)), _shakingDuration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }
    }
}