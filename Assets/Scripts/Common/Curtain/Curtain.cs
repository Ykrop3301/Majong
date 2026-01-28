using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Common.Curtain
{
    public class Curtain : MonoBehaviour, ICurtain
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _showDuration = 1f;
        [SerializeField] private Transform _rotatingElement;
        [SerializeField] private float _periodDuration = 1f;

        private void OnEnable()
        {
            StartRotation();
        }

        private async void StartRotation()
        {
            Vector3 endRotation = _rotatingElement.rotation.eulerAngles;
            endRotation.z = -360f;

            await _rotatingElement.DORotate(endRotation, _periodDuration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1).AsyncWaitForKill();
        }

        public async UniTask Show()
        {
            gameObject.SetActive(true);

            await _canvasGroup.DOFade(1f, _showDuration).AsyncWaitForCompletion();
        }

        public async UniTask Hide()
        {
            await _canvasGroup.DOFade(0f, _showDuration).AsyncWaitForCompletion();
            
            gameObject.SetActive(false);
        }


        private void OnDisable()
        {
            _rotatingElement.DOKill();
        }
    }
}