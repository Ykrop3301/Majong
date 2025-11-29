using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MajongGame.Common.PopupSystem
{
    public class PopupsHolder : MonoBehaviour
    {
        [SerializeField] private List<Popup> _popupVariants;
        private Dictionary<System.Type, Popup> _popups = new Dictionary<System.Type, Popup>();

        private void Awake() =>
            DontDestroyOnLoad(this);

        public T GetPopup<T>() where T : Popup
        {
            if (!_popupVariants.Select(x => x.GetType()).Contains(typeof(T)))
                throw new System.Exception("Invalid type");

            if (_popups.ContainsKey(typeof(T)))
            {
                Popup popup = _popups[typeof(T)];
                if (popup != null && popup.gameObject != null)
                    return _popups[typeof(T)] as T;

                else _popups.Remove(typeof(T));
            }

            T popupPrefab = _popupVariants.Where(x => x.GetType() == typeof(T)).First() as T;
            T newPopup = Instantiate(popupPrefab);
            newPopup.gameObject.SetActive(false);
            _popups.Add(typeof(T), newPopup);

            return newPopup;
        }
    }
}