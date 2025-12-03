using DG.Tweening;
using UnityEngine;

namespace MajongGame.Common.PopupSystem
{
    public abstract class Popup: MonoBehaviour
    {
        [SerializeField] private Transform _transform;

        private const float ANIMATION_DURATION = 0.5f;

        public virtual void Show()
        {
            if (GlobalVariablesController.InPopup)
                return;

            GlobalVariablesController.InPopup = true;

            gameObject.SetActive(true);
            _transform.DOScale(1f, ANIMATION_DURATION).From(0f);
        }

        public virtual void Hide(System.Action actionAfterHide)
        {
            if (!GlobalVariablesController.InPopup)
                return;

            _transform.DOScale(0f, ANIMATION_DURATION)
                .From(1f)
                .OnComplete(() =>
                {
                    GlobalVariablesController.InPopup = false;
                    gameObject.SetActive(false);
                    actionAfterHide();
                });
        }

        public virtual void Hide()
        {
            if (!GlobalVariablesController.InPopup)
                return;

            _transform.DOScale(0f, ANIMATION_DURATION)
                .From(1f)
                .OnComplete(() =>
                {
                    GlobalVariablesController.InPopup = false;
                    gameObject.SetActive(false);
                });
        }
    }
}
