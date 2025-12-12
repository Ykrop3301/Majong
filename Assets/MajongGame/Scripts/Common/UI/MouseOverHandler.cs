using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MajongGame.Common.UI
{
    public class MouseOverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Transform _transform;

        private const float SIZE_KOEF = 1.1f;
        private const float SCALING_DURATION = 0.5f;

        private Vector3 _originalScale;
        private Vector3 _scaledScale;

        private void Start()
        {
            _originalScale = _transform.localScale;
            _scaledScale = _originalScale * SIZE_KOEF;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _transform.DOScale(_scaledScale, SCALING_DURATION);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _transform.DOScale(_originalScale, SCALING_DURATION);
        }
    }
}